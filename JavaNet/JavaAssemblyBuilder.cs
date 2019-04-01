using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using JavaNet.Runtime.Plugs;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using FieldAttributes = Mono.Cecil.FieldAttributes;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using MethodBody = Mono.Cecil.Cil.MethodBody;
using TypeAttributes = Mono.Cecil.TypeAttributes;

namespace JavaNet
{
    public class JavaAssemblyBuilder
    {
        private readonly Dictionary<string, TypeReference> _typePlugs = new Dictionary<string, TypeReference>();
        public Dictionary<string, MethodReference> CastPlugs { get; } = new Dictionary<string, MethodReference>();
        public Dictionary<string, MethodReference> InstanceOfPlugs { get; } = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, TypeReference> _typeReferences = new Dictionary<string, TypeReference>();
        private readonly Dictionary<string, MethodReference> _methodReferences = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, FieldReference> _fieldReferences = new Dictionary<string, FieldReference>();
        private readonly HashSet<string> _annotations = new HashSet<string>();

        private AssemblyDefinition _asm;

        public TypeReference Import(Type t)
        {
            return _asm.MainModule.ImportReference(t);
        }

        public MethodReference Import(MethodBase m) => _asm.MainModule.ImportReference(m);

        public static JavaAssemblyBuilder Instance;

        public JavaAssemblyBuilder()
        {
            Instance = this;
        }

        public void CreatePlugs(AssemblyDefinition asm)
        {
            _typePlugs["java/lang/Object"] = asm.MainModule.TypeSystem.Object;
            _typePlugs["java/lang/String"] = asm.MainModule.TypeSystem.String;
            _typePlugs["java/lang/Throwable"] = asm.MainModule.ImportReference(typeof(Exception));

            PlugAssembly(asm, typeof(StringPlugs).Assembly);
        }

        private void PlugAssembly(AssemblyDefinition asm, Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                if (type.GetCustomAttribute<TypePlugAttribute>() is TypePlugAttribute tpa)
                {
                    _typePlugs[tpa.Name.Replace('.', '/')] = asm.MainModule.ImportReference(type);
                }

                foreach (var method in type.GetMethods())
                {
                    if (method.GetCustomAttribute<MethodPlugAttribute>() is MethodPlugAttribute mpa)
                    {
                        _methodReferences[mpa.Name] = asm.MainModule.ImportReference(method);
                    }

                    if (method.GetCustomAttribute<CastPlugAttribute>() is CastPlugAttribute cpa)
                    {
                        CastPlugs[cpa.TargetType.FullName] = asm.MainModule.ImportReference(method);
                    }

                    if (method.GetCustomAttribute<InstanceOfPlugAttribute>() is InstanceOfPlugAttribute iopa)
                    {
                        CastPlugs[iopa.TargetType.FullName] = asm.MainModule.ImportReference(method);
                    }
                }
            }
        }

        public TypeReference ResolveTypeReference(string name)
        {
            if (name.StartsWith('['))
                return ResolveTypeReference(name.Substring(1)).MakeArrayType();
            if (name.EndsWith(';') && name.StartsWith('L'))
                name = name.Substring(1, name.Length - 2);
            if (name.Length == 1)
                return ResolveFieldDescriptor(name);

            if (_typePlugs.TryGetValue(name, out var value)) return value;
            if (_typeReferences.TryGetValue(name, out value)) return value;

            throw new JavaNetException(JavaNetException.ReasonType.ClassLoad,"Unknown type " + name);
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
                var typeName = type.FullName.Replace('.', '/');
                _typeReferences[typeName] = _asm.MainModule.ImportReference(type);
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
            var typeThings = new List<(TypeDefinition, ClassFile, List<(JavaMethodInfo, MethodDefinition)>)>();

            jar.ClassFiles.TryForeach<ClassFile, Exception>(classFile =>
            {
                var (typeDefinition, methods) = BuildClass(classFile);
                if (typeDefinition != null)
                {
                    typeThings.Add((typeDefinition, classFile, methods));
                    _asm.MainModule.Types.Add(typeDefinition);
                }

            });

            foreach (var (a, b, c) in typeThings)
            {
                BuildClassPart2(a, b, c);
            }

            return _asm;
        }

        public (TypeDefinition td, List<(JavaMethodInfo, MethodDefinition)> maaaaa) BuildClass(ClassFile cf)
        {
            if (_typePlugs.ContainsKey(cf.ThisClass.Name))
            {
                Console.WriteLine("Skipping plugget type {0}", cf.ThisClass.Name);
                return (null, null);
            }

            //Console.WriteLine("Building type {0}", cf.ThisClass.Name);
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
                try
                {
                    var fd = BuildField(fi, cp);
                    td.Fields.Add(fd);
                    _fieldReferences[cf.ThisClass.Name.Replace('/', '.') + "." + fi.Name + ":" + fi.Descriptor] = fd;
                }
                catch (JavaNetException ex) when (ex.Reason == JavaNetException.ReasonType.ClassLoad)
                {
                    Console.WriteLine("Failed to build field {0}::{1}", td.FullName, fi.Name);
                }
            }

            var maaaaa  = new List<(JavaMethodInfo, MethodDefinition)>();

            foreach (var mi in cf.Methods)
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
                    try
                    {
                        var md = BuildMethod(td, mi, cp);
                        td.Methods.Add(md);
                        _methodReferences[cf.ThisClass.Name.Replace('/', '.') + "." + mi.Name + ":" + mi.Descriptor] = md;
                        maaaaa.Add((mi, md));
                    }
                    catch (JavaNetException ex)
                    {
                        Console.WriteLine("Failed to build method {0}::{1}", td.FullName, mi.Name);
                    }
                }
            }

            return (td, maaaaa);

        }

        public void BuildClassPart2(TypeDefinition td, ClassFile cf, List<(JavaMethodInfo, MethodDefinition)> maaaaa)
        {

            foreach (var (mi, md) in maaaaa)
            {
                BuildMethodBody(md, td, mi, cf.ConstantPool);
            }

            //Console.WriteLine("Build {0}", td);

        }

        private MethodDefinition BuildMethod(TypeDefinition definingClass, JavaMethodInfo mi, CpInfo[] cp)
        {
            //Console.WriteLine("  Building method {0} {1}", mi.Name, mi.Descriptor);
            var attrs = (MethodAttributes) 0;
            if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Public))
                attrs |= MethodAttributes.Public;
            else if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Protected))
                attrs |= MethodAttributes.Family;
            else if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Private))
                attrs |= MethodAttributes.Private;

            if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Static) || mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Native))
                attrs |= MethodAttributes.Static;

            if (!mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Final)
                && !mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Static)
                && !mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Native)
                && mi.Name != "<init>")
                attrs |= MethodAttributes.Virtual;
            if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Abstract))
                attrs |= MethodAttributes.Abstract;
            if (mi.Name == "<init>" || mi.Name == "<clinit>")
                attrs |= MethodAttributes.RTSpecialName | MethodAttributes.SpecialName;

            attrs |= MethodAttributes.HideBySig;

            var (retType, paramType) = ResolveMethodDescriptor(mi.Descriptor);

            var md = new MethodDefinition(TranslateMethodName(mi.Name), attrs, retType)
            {
                DeclaringType = definingClass
            };


            foreach (var param in paramType)
            {
                md.Parameters.Add(new ParameterDefinition(param));
            }

            return md;
        }

        private void BuildMethodBody(MethodDefinition md, TypeDefinition definingClass, JavaMethodInfo mi, CpInfo[] cp)
        {

            foreach (var attributeInfo in mi.Attributes)
            {
                switch (attributeInfo)
                {
                    case CodeAttribute code:
                        try
                        {
                            md.Body = MethodGenerator.GenerateMethod(md, mi, code, cp) ?? throw new Exception("NULL AAAAAAAAA");
                        }
                        catch (JavaNetException ex) when (ex.Reason == JavaNetException.ReasonType.ClassLoad)
                        {
                            Console.WriteLine(ex.Message);
                            md.Body = new MethodBody(md);
                            var processor = md.Body.GetILProcessor();
                            processor.Append(Instruction.Create(OpCodes.Ldstr, ex.Message));
                            processor.Append(Instruction.Create(OpCodes.Newobj, _asm.MainModule.ImportReference(typeof(TypeLoadException).GetConstructor(new[] {typeof(string)}))));
                            processor.Append(Instruction.Create(OpCodes.Throw));
                        }

                        break;
                }
            }
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

        public static IEnumerable<FieldReference> GetAllFields(TypeDefinition td)
        {
            return td == null ? Enumerable.Empty<FieldReference>() : td.Fields.Concat(GetAllFields(td.BaseType?.Resolve()));
        }

        public FieldReference ResolveFieldReference(FieldOrMethodrefInfo fmi)
        {
            if (_fieldReferences.TryGetValue(fmi.Represent(), out var f))
                return f ?? throw new JavaNetException(JavaNetException.ReasonType.ClassLoad, "Could not resolve field " + fmi.Represent());
            var declType = ResolveTypeReference(fmi.Class.Name);
            var field = GetAllFields(declType.Resolve()).FirstOrDefault(x => x.Name == fmi.NameAndType.Name);
            _fieldReferences[fmi.Represent()] = field;
            return field ?? throw new JavaNetException(JavaNetException.ReasonType.ClassLoad, "Could not resolve field " + fmi.Represent());
        }

        //public static IEnumerable<MethodReference> GetAllMethods(TypeDefinition td)
        //{
        //    return td == null ? Enumerable.Empty<MethodReference>() : td.GetMethods().Concat(GetAllMethods(td.BaseType?.Resolve()));
        //}

        public static string CreateMethodSignature(TypeDefinition type, string name, TypeReference[] paramTypes)
        {
            return $"{type.FullName}.{name}(" + string.Join(',', paramTypes.Select(x => x.FullName)) + ")";
        }

        public MethodReference ResolveMethodReference(TypeDefinition type, string name, TypeReference[] paramTypes)
        {
            if (!_methodReferences.TryGetValue(CreateMethodSignature(type, name, paramTypes), out var resolvedMethod))
            {

                if (name == ".cctor")
                    resolvedMethod = type.GetStaticConstructor();
                else
                {

                    resolvedMethod =
                        (name == ".ctor" ? type.GetConstructors() : type.GetMethods())
                        .FirstOrDefault(x =>
                            x.Name == name
                            && x.Parameters.Count == paramTypes.Length
                            && x.Parameters.Zip(paramTypes, (param, typeDef) => param.ParameterType.FullName == typeDef.FullName).All(b => b));

                    if (resolvedMethod == null)
                        resolvedMethod = ResolveMethodReference(type.BaseType.Resolve(), name, paramTypes);

                    _methodReferences[CreateMethodSignature(type, name, paramTypes)] = resolvedMethod;
                }
            }

            return resolvedMethod;
        }

        public MethodReference ResolveMethodReference(FieldOrMethodrefInfo fmi)
        {
            var declType = ResolveTypeReference(fmi.Class.Name).Resolve();
            var paramTypes = ResolveMethodDescriptor(fmi.NameAndType.Descriptor).paramType.ToArray();
            var resolvedMethod = ResolveMethodReference(declType, TranslateMethodName(fmi.NameAndType.Name), paramTypes);

            return resolvedMethod
                   ?? throw new JavaNetException(JavaNetException.ReasonType.ClassLoad, "Could not resolve method " + fmi.Represent());
        }

        private string TranslateMethodName(string name)
        {
            switch (name)
            {
                case "<init>":
                    return ".ctor";
                case "<clinit>":
                    return ".cctor";
                case "toString":
                    return "ToString";
                case "equals":
                    return "Equals";
                case "hashCode":
                    return "GetHashCode";
                default:
                    return name;
            }
        }

        internal (TypeReference retType, TypeReference[] paramType) ResolveMethodDescriptor(string desc)
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

        public  TypeReference ResolveFieldDescriptor(string desc)
        {
            var length = 0;
            return ResolveFieldDescriptor(desc, ref length);
        }

        public TypeSystem TypeSystem => _asm.MainModule.TypeSystem;

        public TypeReference ResolveFieldDescriptor(string desc, ref int pos)
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