using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using static JavaNet.JavaInstruction;
using FieldAttributes = Mono.Cecil.FieldAttributes;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using MethodBody = Mono.Cecil.Cil.MethodBody;
using TypeAttributes = Mono.Cecil.TypeAttributes;

namespace JavaNet
{
    public class JavaAssemblyBuilder
    {
        private readonly Dictionary<string, TypeReference> _typePlugs = new Dictionary<string, TypeReference>();
        private readonly Dictionary<string, TypeReference> _typeReferences = new Dictionary<string, TypeReference>();
        private readonly HashSet<string> _annotations = new HashSet<string>();

        private AssemblyDefinition _asm;

        public void CreatePlugs(AssemblyDefinition asm)
        {
            _typePlugs["java/lang/Object"] = asm.MainModule.TypeSystem.Object;
            _typePlugs["java/lang/String"] = asm.MainModule.TypeSystem.String;
            _typePlugs["java/lang/Throwable"] = asm.MainModule.ImportReference(typeof(Exception));
        }
        
        private TypeReference ResolveTypeReference(string name)
        {
            if (_typePlugs.TryGetValue(name, out var value)) return value;
            if (_typeReferences.TryGetValue(name, out value)) return value;

            throw new Exception("Unknown type " + name);
        }

        public void RegisterReferenceAssembly(string filename)
        {
            var md = Assembly.LoadFile(filename);
            RegisterReferenceAssembly(md);
        }

        public void RegisterReferenceAssembly(Assembly mod)
        {
            foreach (var type in mod.ExportedTypes)
            {
                _typeReferences[type.FullName.Replace('.', '/')] = _asm.MainModule.ImportReference(type);
            }
        }
        
        public AssemblyDefinition BuildAssembly(string name, Version version, JarFile jar)
        {
            var an = new AssemblyNameDefinition(name, version);
            _asm = AssemblyDefinition.CreateAssembly(an, name + ".dll", ModuleKind.Dll);
            
            CreatePlugs(_asm);
            
            foreach (var classFile in jar.ClassFiles)
            {
                var cn = classFile.ThisClass.Name.Split('/');
                _typeReferences[classFile.ThisClass.Name] = new TypeReference(
                    string.Join('.', cn.SkipLast(1)),
                    cn.Last(),
                    _asm.MainModule,
                    _asm.MainModule);
                if (classFile.AccessFlags.HasFlag(ClassFile.Flags.Annotation))
                {
                    _annotations.Add(classFile.ThisClass.Name);
                }
            }

            jar.ClassFiles.TryForeach<ClassFile, Exception>(classFile =>
            {
                var typeDefinition = BuildClass(classFile);
                if (typeDefinition != null)
                    _asm.MainModule.Types.Add(typeDefinition);
            });

            return _asm;
        }

        public TypeDefinition BuildClass(ClassFile cf)
        {
            if (_typePlugs.ContainsKey(cf.ThisClass.Name))
            {
                Console.WriteLine("Skipping plugget type {0}", cf.ThisClass.Name);
                return null;
            }
            Console.WriteLine("Building type {0}", cf.ThisClass.Name);
            var cp = cf.ConstantPool;
            var className = cf.ThisClass.Name.Split('/');
            var attrs = (TypeAttributes) 0;

            var isAnnot = cf.AccessFlags.HasFlag(ClassFile.Flags.Annotation);
            
            if (cf.AccessFlags.HasFlag(ClassFile.Flags.Public))
                attrs |= TypeAttributes.Public;
            if (cf.AccessFlags.HasFlag(ClassFile.Flags.Final))
                attrs |= TypeAttributes.Sealed;
            if (cf.AccessFlags.HasFlag(ClassFile.Flags.Interface) && !isAnnot)
                attrs |= TypeAttributes.Interface;
            if (cf.AccessFlags.HasFlag(ClassFile.Flags.Abstract))
                attrs |= TypeAttributes.Abstract;
            
            var td = new TypeDefinition(string.Join('.', className.SkipLast(1)), className.Last(), attrs);

            td.BaseType = isAnnot
                ? _asm.MainModule.ImportReference(typeof(Attribute))
                : ResolveTypeReference(cf.SuperClass.Name);

            foreach (var iface in cf.Interfaces)
            {
                if (iface.Name != "java/lang/annotation/Annotation" && isAnnot)
                    td.Interfaces.Add(new InterfaceImplementation(ResolveTypeReference(iface.Name)));
            }

            foreach (var fi in cf.Fields)
            {
                td.Fields.Add(BuildField(fi, cp));
            }

            cf.Methods.TryForeach<JavaMethodInfo, Exception>(mi =>
            {
                if (isAnnot)
                {
                    // methods become public fields
                    var (rt, _) = ResolveMethodDescriptor(mi.Descriptor);
                    var anotFld = new FieldDefinition(mi.Name, FieldAttributes.Public, rt);
                    Console.WriteLine("  Built annotation field {0}", anotFld);
                    td.Fields.Add(anotFld);
                }
                else
                {
                    td.Methods.Add(BuildMethod(mi, cp));
                }
            });
            
            //Console.WriteLine("Build {0}", td);
            
            return td;
        }

        private MethodDefinition BuildMethod(JavaMethodInfo mi, CpInfo[] cp)
        {
            Console.WriteLine("  Building method {0} {1}", mi.Name, mi.Descriptor);
            var attrs = (MethodAttributes) 0;
            if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Public))
                attrs |= MethodAttributes.Public;
            else if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Protected))
                attrs |= MethodAttributes.Family;
            else if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Private))
                attrs |= MethodAttributes.Private;
            if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Static))
                attrs |= MethodAttributes.Static;
            if (!mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Final) && mi.Name != "<init>")
                attrs |= MethodAttributes.Virtual;
            if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Abstract))
                attrs |= MethodAttributes.Abstract;
            var (retType, paramType) = ResolveMethodDescriptor(mi.Descriptor);
            
            var md = new MethodDefinition(TranslateMethodName(mi.Name), attrs, retType);
            
            foreach (var param in paramType)
            {
                md.Parameters.Add(new ParameterDefinition(param));
            }
            
            foreach (var attributeInfo in mi.Attributes)
            {
                switch (attributeInfo)
                {
                    case CodeAttribute code:
                        md.Body = MethodGenerator.GenerateMethod(md, mi, code, cp);
                        break;
                }
            }
            
            //Console.WriteLine("  Built {0}", md);

            return md;
        }

        private MethodBody GenerateMethodBody(MethodDefinition md, JavaMethodInfo mi, CodeAttribute ca, CpInfo[] cp)
        {
            var mb = new MethodBody(md);
            var codeProc = mb.GetILProcessor();
            var code = ca.Code;
            var jumps = new List<(Instruction, int)>();
            var switches = new List<(Instruction, int[])>();
            var instrs = new Instruction[code.Length];
            var pc = 0;
            var ilLoc = 0;

            while (pc < code.Length)
            {
                var begin = pc;
                Instruction[] res = null;
                switch ((JavaInstruction) code[pc++])
                {
                    case nop:
                        res = new[] {codeProc.Create(OpCodes.Nop)};
                        break;

                    #region constant loading

                    case aconst_null:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_0)};
                        break;
                    case iconst_m1:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_M1)};
                        break;
                    case iconst_0:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_0)};
                        break;
                    case iconst_1:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_1)};
                        break;
                    case iconst_2:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_2)};
                        break;
                    case iconst_3:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_3)};
                        break;
                    case iconst_4:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_4)};
                        break;
                    case iconst_5:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_5)};
                        break;
                    case lconst_0:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I8, 0L)};
                        break;
                    case lconst_1:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I8, 1L)};
                        break;
                    case fconst_0:
                        res = new[] {codeProc.Create(OpCodes.Ldc_R4, 0.0f)};
                        break;
                    case fconst_1:
                        res = new[] {codeProc.Create(OpCodes.Ldc_R4, 1.0f)};
                        break;
                    case fconst_2:
                        res = new[] {codeProc.Create(OpCodes.Ldc_R4, 2.0f)};
                        break;
                    case dconst_0:
                        res = new[] {codeProc.Create(OpCodes.Ldc_R8, 0.0)};
                        break;
                    case dconst_1:
                        res = new[] {codeProc.Create(OpCodes.Ldc_R8, 1.0)};
                        break;
                    case bipush:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_S, (sbyte) code[pc++])};
                        break;
                    case sipush:
                        res = new[] {codeProc.Create(OpCodes.Ldc_I4_S, (code[pc++] << 8) | code[pc++])};
                        break;
                    case ldc:
                        res = PushCp(codeProc, cp[code[pc++]]);
                        break;                    
                    case ldc_w:
                    case ldc2_w:
                        res = PushCp(codeProc, cp[(code[pc++] << 8) | code[pc++]]);
                        break;

                    #endregion

                    #region local loading

                    case iload:
                    case lload:
                    case fload:
                    case dload:
                    case aload:
                        res = new[] {LocalMangling(codeProc, mi, code[pc++], true)};
                        break;
                    case iload_0:
                    case lload_0:
                    case fload_0:
                    case dload_0:
                    case aload_0:
                        res = new[] {LocalMangling(codeProc, mi, 0, true)};
                        break;
                    case iload_1:
                    case lload_1:
                    case fload_1:
                    case dload_1:
                    case aload_1:
                        res = new[] {LocalMangling(codeProc, mi, 1, true)};
                        break;
                    case iload_2:
                    case lload_2:
                    case fload_2:
                    case dload_2:
                    case aload_2:
                        res = new[] {LocalMangling(codeProc, mi, 2, true)};
                        break;
                    case iload_3:
                    case lload_3:
                    case fload_3:
                    case dload_3:
                    case aload_3:
                        res = new[] {LocalMangling(codeProc, mi, 3, true)};
                        break;

                    #endregion

                    #region array loading

                    case iaload:
                        res = new[] {codeProc.Create(OpCodes.Ldelem_I4)};
                        break;
                    case laload:
                        res = new[] {codeProc.Create(OpCodes.Ldelem_I8)};
                        break;
                    case faload:
                        res = new[] {codeProc.Create(OpCodes.Ldelem_R4)};
                        break;
                    case daload:
                        res = new[] {codeProc.Create(OpCodes.Ldelem_R8)};
                        break;
                    case aaload:
                        res = new[] {codeProc.Create(OpCodes.Ldelem_Ref)};
                        break;

                    #endregion
                    
                    #region local storing
                    
                    case istore:
                    case lstore:
                    case fstore:
                    case dstore:
                    case astore:
                        res = new[] {LocalMangling(codeProc, mi, code[pc++], false)};
                        break;
                    case istore_0:
                    case lstore_0:
                    case fstore_0:
                    case dstore_0:
                    case astore_0:
                        res = new[] {LocalMangling(codeProc, mi, 0, false)};
                        break;
                    case istore_1:
                    case lstore_1:
                    case fstore_1:
                    case dstore_1:
                    case astore_1:
                        res = new[] {LocalMangling(codeProc, mi, 1, false)};
                        break;
                    case istore_2:
                    case lstore_2:
                    case fstore_2:
                    case dstore_2:
                    case astore_2:
                        res = new[] {LocalMangling(codeProc, mi, 2, false)};
                        break;
                    case istore_3:
                    case lstore_3:
                    case fstore_3:
                    case dstore_3:
                    case astore_3:
                        res = new[] {LocalMangling(codeProc, mi, 3, false)};
                        break;
                    
                    #endregion

                    #region array storing
                    
                    case iastore:
                        res = new[] {codeProc.Create(OpCodes.Stelem_I4)};
                        break;
                    case lastore:
                        res = new[] {codeProc.Create(OpCodes.Stelem_I8)};
                        break;
                    case fastore:
                        res = new[] {codeProc.Create(OpCodes.Stelem_R4)};
                        break;
                    case dastore:
                        res = new[] {codeProc.Create(OpCodes.Stelem_R8)};
                        break;
                    case aastore:
                        res = new[] {codeProc.Create(OpCodes.Stelem_Ref)};
                        break;

                    #endregion
                    
                    case pop:
                        res = new[] {codeProc.Create(OpCodes.Pop)};
                        break;
                    
                    case dup:
                        res = new[] {codeProc.Create(OpCodes.Dup)};
                        break;

                    #region arithmetic

                    case iadd:
                    case ladd:
                    case fadd:
                    case dadd:
                        res = new[] {codeProc.Create(OpCodes.Add)};
                        break;
                    case isub:
                    case lsub:
                    case fsub:
                    case dsub:
                        res = new[] {codeProc.Create(OpCodes.Sub)};
                        break;
                    case imul:
                    case lmul:
                    case fmul:
                    case dmul:
                        res = new[] {codeProc.Create(OpCodes.Mul)};
                        break;
                    case idiv:
                    case ldiv:
                    case fdiv:
                    case ddiv:
                        res = new[] {codeProc.Create(OpCodes.Div)};
                        break;
                    case irem:
                    case lrem:
                    case frem:
                    case drem:
                        res = new[] {codeProc.Create(OpCodes.Rem)};
                        break;
                    case ineg:
                    case lneg:
                    case fneg:
                    case dneg:
                        res = new[] {codeProc.Create(OpCodes.Neg)};
                        break;
                    #endregion
                    
                    #region logical
                    
                    case ishl:
                    case lshl:
                        res = new[] {codeProc.Create(OpCodes.Shl)};
                        break;
                    case ishr:
                    case lshr:
                        res = new[] {codeProc.Create(OpCodes.Shr)};
                        break;
                    case iushr:
                    case lushr:
                        res = new[] {codeProc.Create(OpCodes.Shr_Un)};
                        break;
                    case iand:
                    case land:
                        res = new[] {codeProc.Create(OpCodes.And)};
                        break;
                    case ior:
                    case lor:
                        res = new[] {codeProc.Create(OpCodes.Or)};
                        break;
                    case ixor:
                    case lxor:
                        res = new[] {codeProc.Create(OpCodes.Xor)};
                        break;
                    #endregion

                    case iinc:
                    {
                        var index = code[pc++];
                        var dif = (sbyte) code[pc++];
                        res = new[]
                        {
                            codeProc.Create(OpCodes.Ldloc_S, index),
                            codeProc.Create(OpCodes.Ldc_I4_S, dif),
                            codeProc.Create(OpCodes.Add),
                            codeProc.Create(OpCodes.Stloc_S, index), 
                        };
                        break;
                    }

                    #region conversions

                    case l2i:
                    case f2i:
                    case d2i:
                        res = new[] {codeProc.Create(OpCodes.Conv_I4)};
                        break;
                    case i2l:
                    case f2l:
                    case d2l:
                        res = new[] {codeProc.Create(OpCodes.Conv_I8)};
                        break;
                    case i2f:
                    case l2f:
                    case d2f:
                        res = new[] {codeProc.Create(OpCodes.Conv_R4)};
                        break;
                    case i2d:
                    case l2d:
                    case f2d:
                        res = new[] {codeProc.Create(OpCodes.Conv_R8)};
                        break;
                    case i2b:
                        res = new[] {codeProc.Create(OpCodes.Conv_I1)};
                        break;
                    case i2s:
                    case i2c:
                        res = new[] {codeProc.Create(OpCodes.Conv_I2)};
                        break;
                    #endregion

                    #region branches

                    case ifeq:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Brfalse, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }
                    case ifne:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Brtrue, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }
                    case iflt:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[]
                        {
                            codeProc.Create(OpCodes.Ldc_I4_0),
                            codeProc.Create(OpCodes.Blt, codeProc.Create(OpCodes.Nop))
                        };
                        jumps.Add((res[1], target));
                        break;
                    }
                    case ifge:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[]
                        {
                            codeProc.Create(OpCodes.Ldc_I4_0),
                            codeProc.Create(OpCodes.Bge, codeProc.Create(OpCodes.Nop))
                        };
                        jumps.Add((res[1], target));
                        break;
                    }
                    case ifgt:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[]
                        {
                            codeProc.Create(OpCodes.Ldc_I4_0),
                            codeProc.Create(OpCodes.Bgt, codeProc.Create(OpCodes.Nop))
                        };
                        jumps.Add((res[1], target));
                        break;
                    }
                    case ifle:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[]
                        {
                            codeProc.Create(OpCodes.Ldc_I4_0),
                            codeProc.Create(OpCodes.Ble, codeProc.Create(OpCodes.Nop))
                        };
                        jumps.Add((res[1], target));
                        break;
                    }
                    
                    case @goto:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Br, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }
                    case goto_w:
                    {
                        var target = begin + I4(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Br, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }

                    #endregion

                    #region compare and branch

                    case if_icmpeq:
                    case if_acmpeq:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Beq, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }
                    case if_icmpne:
                    case if_acmpne:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Bne_Un, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }
                    
                    case if_icmplt:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Blt, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }
                    case if_icmpgt:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Bgt, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }
                    case if_icmpge:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Bge, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }
                    case if_icmple:
                    {
                        var target = begin + I2(code, ref pc);
                        res = new[] {codeProc.Create(OpCodes.Ble, codeProc.Create(OpCodes.Nop))};
                        jumps.Add((res[0], target));
                        break;
                    }

                    #endregion

                    #region switches

                    case tableswitch:
                    {
                        while (pc % 4 != 0) pc++;
                        var defaultTarget = begin + I4(code, ref pc);
                        var low = I4(code, ref pc);
                        var high = I4(code, ref pc);
                        var targets = new int[high - low + 1];
                        for (var i = 0; i < high - low + 1; i++)
                        {
                            targets[i] = begin + I4(code, ref pc);
                        }

                        res = new[]
                        {
                            codeProc.Create(OpCodes.Ldc_I4, low),
                            codeProc.Create(OpCodes.Sub),
                            codeProc.Create(OpCodes.Switch, new Instruction[high - low + 1]),
                            codeProc.Create(OpCodes.Br, codeProc.Create(OpCodes.Nop)),
                        };

                        switches.Add((res[2], targets));
                        jumps.Add((res[3], defaultTarget));
                        break;
                    }

                    case lookupswitch:
                    {
                        while (pc % 4 != 0) pc++;
                        var defaultTarget = begin + I4(code, ref pc);
                        var npairs = I4(code, ref pc);
                        var resList = new List<Instruction>();
                        for (var i = 0; i < npairs; i++)
                        {
                            var m = I4(code, ref pc);
                            var t = begin + I4(code, ref pc);
                            if (i < npairs - 1)
                                resList.Add(codeProc.Create(OpCodes.Dup));
                            resList.Add(codeProc.Create(OpCodes.Ldc_I4, m));
                            var br = codeProc.Create(OpCodes.Beq, codeProc.Create(OpCodes.Nop));
                            jumps.Add((br, t));
                            resList.Add(br);
                        }
                        var defBr = codeProc.Create(OpCodes.Br, codeProc.Create(OpCodes.Nop));
                        jumps.Add((defBr, defaultTarget));
                        resList.Add(defBr);
                        res = resList.ToArray();
                        break;
                    }

                    #endregion
                    
                    case ireturn:
                    case lreturn:
                    case freturn:
                    case dreturn:
                    case areturn:
                    case @return:
                        res = new[] {codeProc.Create(OpCodes.Ret)};
                        break;
                    
                    case getstatic:
                        res = new[] {codeProc.Create(OpCodes.Ldsfld, ResolveFieldReference((FieldOrMethodrefInfo)cp[I2(code, ref pc)]))};
                        break;
                    case putstatic:
                        res = new[] {codeProc.Create(OpCodes.Stsfld, ResolveFieldReference((FieldOrMethodrefInfo)cp[I2(code, ref pc)]))};
                        break;
                    case getfield:
                        res = new[] {codeProc.Create(OpCodes.Ldfld, ResolveFieldReference((FieldOrMethodrefInfo)cp[I2(code, ref pc)]))};
                        break;
                    case putfield:
                        res = new[] {codeProc.Create(OpCodes.Stfld, ResolveFieldReference((FieldOrMethodrefInfo)cp[I2(code, ref pc)]))};
                        break;
                    
                    case invokevirtual:    
                        res = new[] {codeProc.Create(OpCodes.Callvirt, ResolveMethodReference((FieldOrMethodrefInfo) cp[I2(code, ref pc)], true))};
                        break;                    
                    case invokespecial:
                        res = new[] {codeProc.Create(OpCodes.Call, ResolveMethodReference((FieldOrMethodrefInfo)cp[I2(code, ref pc)], true))};
                        break;
                    case invokestatic:
                        res = new[] {codeProc.Create(OpCodes.Call, ResolveMethodReference((FieldOrMethodrefInfo)cp[I2(code, ref pc)], false))};
                        break;

                    case invokeinterface:
                    {
                        var fmr = (FieldOrMethodrefInfo) cp[I2(code, ref pc)];
                        if (_annotations.Contains(fmr.Class.Name))
                        {
                            var (rt, _) = ResolveMethodDescriptor(fmr.NameAndType.Descriptor);
                            res = new[]
                            {
                                codeProc.Create(
                                    OpCodes.Ldfld,
                                    new FieldReference(fmr.NameAndType.Name, rt, ResolveTypeReference(fmr.Class.Name)))
                            };
                        }
                        else
                        {
                            res = new[] {codeProc.Create(OpCodes.Callvirt, ResolveMethodReference(fmr, true))};
                        }
                        break;
                    }
                        
                    
                    case anewarray:
                    {
                        var index = I2(code, ref pc);
                        Console.WriteLine("newarr " + cp[index]);
//                        res = new[]
//                        {
//                            codeProc.Create(OpCodes.Newarr,
//                                ResolveFieldDescriptor(((ClassInfo) cp[index]).Name))
//                        };
                        throw new NotImplementedException();
                    }
                    case newarray:
                    {
                        var atype = I2(code, ref pc);
                        switch (atype)
                        {
                            case 4:
                                res = new[] {codeProc.Create(OpCodes.Newarr, _asm.MainModule.TypeSystem.Boolean)};
                                break;

                            case 5:
                                res = new[] {codeProc.Create(OpCodes.Newarr, _asm.MainModule.TypeSystem.Char)};
                                break;

                            case 6:
                                res = new[] {codeProc.Create(OpCodes.Newarr, _asm.MainModule.TypeSystem.Single)};
                                break;

                            case 7:
                                res = new[] {codeProc.Create(OpCodes.Newarr, _asm.MainModule.TypeSystem.Double)};
                                break;

                            case 8:
                                res = new[] {codeProc.Create(OpCodes.Newarr, _asm.MainModule.TypeSystem.SByte)};
                                break;

                            case 9:
                                res = new[] {codeProc.Create(OpCodes.Newarr, _asm.MainModule.TypeSystem.Int16)};
                                break;

                            case 10:
                                res = new[] {codeProc.Create(OpCodes.Newarr, _asm.MainModule.TypeSystem.Int32)};
                                break;

                            case 11:
                                res = new[] {codeProc.Create(OpCodes.Newarr, _asm.MainModule.TypeSystem.Int64)};
                                break;
                        }

                        break;
                    }

                    case @new:
                    {
                        var index = I2(code, ref pc);
                        Console.WriteLine("new -> {0} -> {1}", (JavaInstruction) code[pc],
                            (JavaInstruction) code[pc + 1]);
                        break;
                    }
                    
                    case arraylength:
                        res = new[] {codeProc.Create(OpCodes.Ldlen)};
                        break;
                    case athrow:
                        res = new[] {codeProc.Create(OpCodes.Throw)};
                        break;
                    case baload:
                        res = new[] {codeProc.Create(OpCodes.Ldelem_I1)};
                        break;
                    case bastore:
                        res = new[] {codeProc.Create(OpCodes.Stelem_I1)};
                        break;
                    case caload:
                        res = new[] {codeProc.Create(OpCodes.Ldelem_I2)};
                        break;
                    case castore:
                        res = new[] {codeProc.Create(OpCodes.Stelem_I2)};
                        break;
                    case checkcast:
                    {
                        var index = (code[pc++] << 8) | code[pc++];
                        Console.WriteLine("checkcast " + cp[index]);
//                        res = new[]
//                        {
//                            codeProc.Create(OpCodes.Newarr,
//                                ResolveFieldDescriptor(((ClassInfo) cp[index]).Name))
//                        };
                        throw new NotImplementedException();
                    }
                    default:
                        throw new NotImplementedException("No implementation for " + (JavaInstruction) code[pc - 1]);
                }

                instrs[begin] = res[0];

                //Console.WriteLine("    // {0} {1}", (JavaInstruction) code[begin], GitEm(code, begin + 1, pc));
                
                foreach (var instruction in res)
                {
                    instruction.Offset = ilLoc;
                    ilLoc += instruction.OpCode.Size +
                             OperandLength(instruction.OpCode.OperandType, instruction.Operand);
                    // Console.WriteLine("    {0}", instruction);
                    codeProc.Append(instruction);
                }
            }
            
            foreach (var (instruction, target) in jumps)
            {
                instruction.Operand = instrs[target];
            }

            foreach (var (instruction, targets) in switches)
            {
                instruction.Operand = targets.Select(x => instrs[x]).ToArray();
            }

            return mb;
        }

        private string GitEm(byte[] a, int from, int to)
        {
            if (from == to)
                return "";
            
            var res = 0L;
            for (var i = from; i < to; i++)
            {
                res = (res << 8) | a[i];
            }

            return res.ToString("X" + (to - from));
        }

        private (bool isArg, int index) LocalIndex(JavaMethodInfo mi, int index)
        {
            var counter = 0;
            var argCount = 0;

            if (!mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Static))
            {
                if (counter == index) 
                    return (true, argCount);
                counter++;
                argCount++;
            }

            var (_, paramTypes) = ResolveMethodDescriptor(mi.Descriptor);

            foreach (var type in paramTypes)
            {
                if (counter == index) 
                    return (true, argCount);
                counter++;
                argCount++;

                if (type.FullName == "System.Double" || type.FullName == "System.Long")
                    counter++;
            }

            return (false, index - counter);
        }

        private Instruction LocalMangling(ILProcessor codeProc, JavaMethodInfo mi, byte local, bool load)
        {
            var (isArg, index) = LocalIndex(mi, local);
            var toUse = load
                ? (isArg
                    ? new[]
                    {
                        OpCodes.Ldarg_0, OpCodes.Ldarg_1, OpCodes.Ldarg_2, OpCodes.Ldarg_3, OpCodes.Ldarg_S,
                        OpCodes.Ldarg
                    }
                    : new[]
                    {
                        OpCodes.Ldloc_0, OpCodes.Ldloc_1, OpCodes.Ldloc_2, OpCodes.Ldloc_3, OpCodes.Ldloc_S,
                        OpCodes.Ldloc
                    })
                : (isArg
                    ? new[]
                    {
                        OpCodes.Starg_S, OpCodes.Starg_S, OpCodes.Starg_S, OpCodes.Starg_S, OpCodes.Starg_S, OpCodes.Starg
                    }
                    : new[]
                    {
                        OpCodes.Stloc_0, OpCodes.Stloc_1, OpCodes.Stloc_2, OpCodes.Stloc_3, OpCodes.Stloc_S,
                        OpCodes.Stloc
                    });
            if (load || !isArg)
                switch (index)
                {
                    case 0: return codeProc.Create(toUse[0]);
                    case 1: return codeProc.Create(toUse[1]);
                    case 2: return codeProc.Create(toUse[2]);
                    case 3: return codeProc.Create(toUse[3]);
                }

            if (index <= byte.MaxValue)
            {
                return codeProc.Create(toUse[4], (byte) index);
            }

            return codeProc.Create(toUse[5], (ushort) index);
        }

        private int OperandLength(OperandType opCodeOperandType, object operand)
        {
            switch (opCodeOperandType)
            {
                case OperandType.InlineBrTarget:
                case OperandType.InlineField:
                case OperandType.InlineI:
                case OperandType.InlineI8:
                case OperandType.ShortInlineR:
                case OperandType.InlineSig:
                case OperandType.InlineString:
                case OperandType.InlineTok:
                case OperandType.InlineType:
                case OperandType.InlineMethod: 
                    return 4;
                case OperandType.InlineNone: 
                    return 0;
                case OperandType.InlineSwitch: 
                    return ((object[]) operand).Length * 4;
                case OperandType.InlineVar:
                case OperandType.InlineArg:
                case OperandType.ShortInlineBrTarget:
                case OperandType.ShortInlineI:
                    return 2;
                case OperandType.InlineR:
                    return 8;
                case OperandType.ShortInlineVar:
                case OperandType.ShortInlineArg:
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(opCodeOperandType), opCodeOperandType, null);
            }
        }

        private FieldReference ResolveFieldReference(FieldOrMethodrefInfo fmi)
        {
            var declType = ResolveTypeReference(fmi.Class.Name);
            var fldType = ResolveFieldDescriptor(fmi.NameAndType.Descriptor);
            return new FieldReference(fmi.NameAndType.Name, fldType, declType);
        }

        private MethodReference ResolveMethodReference(FieldOrMethodrefInfo fmi, bool instance)
        {
            var declType = ResolveTypeReference(fmi.Class.Name);
            var (retType, paramType) = ResolveMethodDescriptor(fmi.NameAndType.Descriptor);
            var mr = new MethodReference(TranslateMethodName(fmi.NameAndType.Name), retType, declType);

            mr.HasThis = instance;

            foreach (var param in paramType)
            {
                mr.Parameters.Add(new ParameterDefinition(param));
            }
            
            return mr;
        }

        private string TranslateMethodName(string name)
        {
            switch (name)
            {
                case "<init>":
                    return ".ctor";
                case "<cinit>":
                    return ".cctor";
                case "toString":
                    return "ToString";
                case "hashCode":
                    return "GetHashCode";
                default:
                    return name;
            }
        }

        private static short I2(byte[] code, ref int pc)
        {
            return (short) ((code[pc++] << 8) | code[pc++]);
        }

        private static int I4(byte[] code, ref int pc)
        {
            return (code[pc++] << 24) | (code[pc++] << 16) | (code[pc++] << 8) | code[pc++];
        }

        private Instruction[] PushCp(ILProcessor codeProc, CpInfo cpInfo)
        {
            switch (cpInfo)
            {
                case StringInfo si:
                    return new[] {codeProc.Create(OpCodes.Ldstr, si.String)};
                case IntegerInfo ii:
                    return new[] {codeProc.Create(OpCodes.Ldc_I4, ii.Value)};
                case FloatInfo fi:
                    return new[] {codeProc.Create(OpCodes.Ldc_R4, fi.Value)};
                case ClassInfo ci:
                    return new[] {codeProc.Create(OpCodes.Ldtoken, ResolveTypeReference(ci.Name))};
                case DoubleInfo di:
                    return new[] {codeProc.Create(OpCodes.Ldc_R8, di.Value)};
                case LongInfo li:
                    return new[] {codeProc.Create(OpCodes.Ldc_I8, li.Value)};
                default:
                    throw new ArgumentException("Unknown constant type " + cpInfo.Tag, nameof(cpInfo));
            }
        }

        private (TypeReference retType, TypeReference[] paramType) ResolveMethodDescriptor(string desc)
        {
            var pos = 1;
            var parTypes = new List<TypeReference>();
            while (desc[pos] != ')')
            {
                parTypes.Add(ResolveFieldDescriptor(desc, ref pos));
            }

            pos++;

            return (ResolveFieldDescriptor(desc, ref pos), parTypes.ToArray());
        }

        private FieldDefinition BuildField(JavaFieldInfo fi, CpInfo[] cp)
        {
            var attrs = (FieldAttributes) 0;

            if (fi.AccessFlags.HasFlag(JavaFieldInfo.Flags.Private))
                attrs |= FieldAttributes.Private;
            else if (fi.AccessFlags.HasFlag(JavaFieldInfo.Flags.Public))
                attrs |= FieldAttributes.Public;
            else if (fi.AccessFlags.HasFlag(JavaFieldInfo.Flags.Protected))
                attrs |= FieldAttributes.Family;
            
            var fieldType = ResolveFieldDescriptor(fi.Descriptor);

            if (fi.AccessFlags.HasFlag(JavaFieldInfo.Flags.Static))
                attrs |= FieldAttributes.Static;
            if (fi.AccessFlags.HasFlag(JavaFieldInfo.Flags.Final))
                attrs |= FieldAttributes.InitOnly;

            var fd = new FieldDefinition(fi.Name, attrs, fieldType);

            foreach (var attributeInfo in fi.Attributes)
            {
                switch (attributeInfo)
                {
                    case ConstantValueAttribute cva:
                        fd.Attributes |= FieldAttributes.Literal;
                        fd.Attributes &= ~FieldAttributes.InitOnly;
                        switch (fi.Descriptor)
                        {
                            case "J":
                                fd.Constant = ((LongInfo) cva.ConstantValue).Value;
                                break;
                            case "F":
                                fd.Constant = ((FloatInfo) cva.ConstantValue).Value;
                                break;
                            case "D":
                                fd.Constant = ((DoubleInfo) cva.ConstantValue).Value;
                                break;
                            case "I":
                                fd.Constant = ((IntegerInfo) cva.ConstantValue).Value;
                                break;
                            case "S":
                                fd.Constant = (short) ((IntegerInfo) cva.ConstantValue).Value;
                                break;
                            case "B":
                                fd.Constant = (byte) ((IntegerInfo) cva.ConstantValue).Value;
                                break;
                            case "C":
                                fd.Constant = (char) ((IntegerInfo) cva.ConstantValue).Value;
                                break;
                            case "Z":
                                fd.Constant = ((IntegerInfo) cva.ConstantValue).Value != 0;
                                break;
                            case "Ljava/lang/String;":
                                fd.Constant = ((StringInfo) cva.ConstantValue).String;
                                break;
                        }

                        break;
                }
            }

            return fd;
        }

        private TypeReference ResolveFieldDescriptor(string desc)
        {
            var length = 0;
            return ResolveFieldDescriptor(desc, ref length);
        }

        private TypeReference ResolveFieldDescriptor(string desc, ref int pos)
        {
            var typeSystem = _asm.MainModule.TypeSystem;
            switch (desc[pos++])
            {
                case 'B': return typeSystem.SByte;
                case 'C': return typeSystem.Char;
                case 'D': return typeSystem.Double;
                case 'F': return typeSystem.Single;
                case 'I': return typeSystem.Int32;
                case 'J': return typeSystem.Int64;
                case 'S': return typeSystem.Int16;
                case 'Z': return typeSystem.Boolean;
                case 'V': return typeSystem.Void;
                case 'L':
                    var semi = desc.IndexOf(';', pos);
                    var cn = desc.Substring(pos, semi - pos);
                    pos = semi + 1;
                    return ResolveTypeReference(cn);
                case '[':
                    return ResolveFieldDescriptor(desc, ref pos).MakeArrayType();
                default:
                    throw new ArgumentException($"Unknown type descriptor '{desc[pos - 1]}'", nameof(desc));
            }
        }
    }
}