using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using JavaNet.Runtime.Plugs;
using JavaNet.Runtime.Plugs.NativeImpl;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;
using FieldAttributes = Mono.Cecil.FieldAttributes;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using MethodBody = Mono.Cecil.Cil.MethodBody;
using MethodImplAttributes = Mono.Cecil.MethodImplAttributes;
using ParameterAttributes = Mono.Cecil.ParameterAttributes;
using TypeAttributes = Mono.Cecil.TypeAttributes;

namespace JavaNet
{
    public class JavaAssemblyBuilder
    {
        private readonly Dictionary<string, TypeReference> _typeReferences = new Dictionary<string, TypeReference>();
        private readonly Dictionary<string, TypeDefinition> _typeDefinitions = new Dictionary<string, TypeDefinition>();
        private readonly Dictionary<string, TypeReference> _typePlugs = new Dictionary<string, TypeReference>();

        public Dictionary<string, MethodReference> CastPlugs { get; } = new Dictionary<string, MethodReference>();
        public Dictionary<string, MethodReference> InstanceOfPlugs { get; } = new Dictionary<string, MethodReference>();

        private readonly Dictionary<string, MethodReference> _methodReferences = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, MethodReference> _methodPlugs = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, MethodReference> _nativeMethodImpl = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, FieldReference> _fieldReferences = new Dictionary<string, FieldReference>();
        private readonly Dictionary<string, TypeReference> _nativeDataTypes = new Dictionary<string, TypeReference>();
        private readonly HashSet<string> _annotations = new HashSet<string>();

        private AssemblyDefinition _asm;

        public TypeReference Import(Type t)
        {
            return t.Assembly.FullName == "System.Private.CoreLib" ? SystemImport(t) : _asm.MainModule.ImportReference(t);
        }

        public MethodReference Import(MethodBase m) => _asm.MainModule.ImportReference(m);

        public static JavaAssemblyBuilder Instance;

        /// <summary>
        /// Contains a list of the following: <br/>
        /// - TypeDefinition (.NET representation of a type) <br/>
        /// - ClassFile (Java representation of the same type) <br/>
        /// - A list, with an element for each method of the type, with: <br/>
        ///   - JavaMethodInfo (Java representation of the method) <br/>
        ///   - MethodDefinition (.NET representation of the same method) <br/>
        /// </summary>
        private List<(TypeDefinition, ClassFile, List<(JavaMethodInfo, MethodDefinition)>)> _typeThings;
        private JarFile _jar;

        public JavaAssemblyBuilder()
        {
            Instance = this;
        }

        private TypeReference SystemImport(Type type)
        {
            return _asm.MainModule.ImportReference(type);
        }

        public void CreatePlugs(AssemblyDefinition asm)
        {
            _typePlugs["java/lang/Object"] = SystemImport(typeof(object));
            _typePlugs["java/lang/String"] = SystemImport(typeof(string));
            _typePlugs["java/lang/Throwable"] = SystemImport(typeof(Exception));

            _typePlugs["java/lang/Class"] = SystemImport(typeof(Type));
            _typePlugs["java/lang/annotation/Annotation"] = SystemImport(typeof(Attribute));
            _typePlugs["java/lang/reflect/AccessibleObject"] = SystemImport(typeof(MemberInfo));
            _typePlugs["java/lang/reflect/Constructor"] = SystemImport(typeof(ConstructorInfo));
            _typePlugs["java/lang/reflect/Field"] = SystemImport(typeof(FieldInfo));
            _typePlugs["java/lang/reflect/Method"] = SystemImport(typeof(MethodInfo));

            _typePlugs["java/lang/NoSuchFieldException"] = SystemImport(typeof(MissingFieldException));
            _typePlugs["java/lang/NoSuchMethodException"] = SystemImport(typeof(MissingMethodException));
            _typePlugs["java/lang/NullPointerException"] = SystemImport(typeof(NullReferenceException));
            _typePlugs["java/lang/ClassCastException"] = SystemImport(typeof(InvalidCastException));
            _typePlugs["java/lang/IllegalMonitorStateException"] = SystemImport(typeof(SynchronizationLockException));
            _typePlugs["java/lang/InterruptedException"] = SystemImport(typeof(ThreadInterruptedException));

            PlugAssembly(asm, typeof(StringPlugs).Assembly);
        }

        private void PlugAssembly(AssemblyDefinition asm, Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                if (type.GetCustomAttribute<TypePlugAttribute>() is TypePlugAttribute tpa)
                {
                    _typePlugs[(tpa.Name ?? type.FullName).Replace('.', '/')] = Import(type);
                }

                foreach (var nda in type.GetCustomAttributes<NativeDataAttribute>())
                {
                    _nativeDataTypes[nda.TargetClass.Replace('.', '/')] = Import(type);
                }

                foreach (var method in type.GetMethods())
                {
                    foreach (var mpa in method.GetCustomAttributes<MethodPlugAttribute>())
                    {
                        var signature = CreateMethodSignature(
                            mpa.IsStatic,
                            mpa.ReturnType ?? method.ReturnType.FullName,
                            mpa.DeclaringType,
                            mpa.MethodName,
                            mpa.ParamTypes);
                        _methodPlugs[signature] = Import(method);
                        _methodReferences[signature] = Import(method);
                    }

                    foreach (var mpa in method.GetCustomAttributes<NativeImplAttribute>())
                    {
                        var isStatic = mpa.IsStatic;
                        var returnType = mpa.ReturnType
                                         ?? method.ReturnTypeCustomAttributes.GetCustomAttributes(false).OfType<ActualTypeAttribute>().FirstOrDefault()?.TypeName
                                         ?? method.ReturnType.FullName;
                        var declType = mpa.DeclaringType ?? (string) type.GetField("TypeName", BindingFlags.Static | BindingFlags.Public).GetValue(null);
                        var methodName = mpa.MethodName ?? method.Name;
                        var argTypes = mpa.ArgTypes ?? method.GetParameters().Select(ActualTypeName).Skip(isStatic ? 0 : 1).ToArray();
                        var signature = CreateMethodSignature(isStatic, returnType, declType, methodName, argTypes);
                        _nativeMethodImpl[signature] = Import(method);
                    }

                    if (method.GetCustomAttribute<CastPlugAttribute>() is CastPlugAttribute cpa)
                    {
                        CastPlugs[cpa.TargetType.FullName] = Import(method);
                    }

                    foreach (var iopa in method.GetCustomAttributes<InstanceOfPlugAttribute>())
                    {
                        InstanceOfPlugs[iopa.TargetType.FullName] = Import(method);
                    }
                }
            }
        }

        private string ActualTypeName(ParameterInfo arg)
        {
            return arg.GetCustomAttribute<ActualTypeAttribute>()?.TypeName ?? arg.ParameterType.FullName;
        }

        public TypeReference ResolveTypeReference(string name)
        {
            if (name.StartsWith('['))
                return ResolveTypeReference(name.Substring(1)).MakeArrayType();
            if (name.EndsWith(';') && name.StartsWith('L'))
                name = name.Substring(1, name.Length - 2);
            if (name.Length == 1)
                return ResolveFieldDescriptor(name);

            name = name.Replace('.', '/');

            if (_typePlugs.TryGetValue(name, out var value)) return value;

            var td = GetOrDefineType(name);
            if (td != null) return td;

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
                _typeReferences[typeName] = Import(type);
            }
        }
        
        public AssemblyDefinition BuildAssembly(string name, Version version, JarFile jar)
        {
            var an = new AssemblyNameDefinition(name, version);
            _jar = jar;
            _asm = AssemblyDefinition.CreateAssembly(an, name + ".dll", ModuleKind.Dll);
            
            CreatePlugs(_asm);

            var innerTypes = new Dictionary<string, (string, string)>();

            var lastAnonName = 0;

            foreach (var cf in jar.ClassFiles)
            {
                foreach (var ica in cf.Attributes.OfType<InnerClassesAttribute>().SelectMany(x => x.Classes).Where(x => x.InnerClass.Name == cf.ThisClass.Name))
                {
                    var innerName = ica.InnerClass.Name;
                    Debug.Assert(innerName != null, nameof(innerName) + " != null");
                    Debug.Assert(innerName.Contains('$'));
                    var outerName = ica.OuterClass?.Name ?? innerName.Substring(0, innerName.LastIndexOf('$'));
                    var newInnerName = ica.InnerClassName?.Data ?? "<>anon_" + (lastAnonName++);
                    innerTypes[innerName] = (outerName, newInnerName);
                }
            }

            foreach (var classFile in jar.ClassFiles)
            {
                var cn = classFile.ThisClass.Name.Split('/');
                //_typeReferences[classFile.ThisClass.Name] = new TypeReference(
                //    string.Join('.', cn.SkipLast(1)),
                //    cn.Last(),
                //    _asm.MainModule,
                //    _asm.MainModule);
                if (classFile.AccessFlags.HasFlag(ClassFile.Flags.Annotation))
                {
                    _annotations.Add(classFile.ThisClass.Name);
                }
            }
            _typeThings = new List<(TypeDefinition, ClassFile, List<(JavaMethodInfo, MethodDefinition)>)>();

            foreach (var classFile in jar.ClassFiles)
            {
                BuildClassDefinition(classFile);
            }

            foreach (var classFile in jar.ClassFiles)
            {
                BuildClassFieldsAndMethods(jar, classFile);
            }

            foreach (var classFile in jar.ClassFiles)
            {
                var key = classFile.ThisClass.Name;
                if (!_typeDefinitions.TryGetValue(key, out var typeDefinition))
                    continue;
                if (innerTypes.TryGetValue(key, out var info))
                {
                    var (enclosingClass, innerName) = info;
                    if (_typeDefinitions.TryGetValue(enclosingClass, out var outerType))
                    {
                        typeDefinition.Name = innerName;
                        _asm.MainModule.Types.Remove(typeDefinition);
                        outerType.NestedTypes.Add(typeDefinition);
                        typeDefinition.DeclaringType = outerType;
                        typeDefinition.IsNestedPublic = true;
                    }
                }
            }

            foreach (var (a, b, c) in _typeThings)
            {
                BuildClassMethodBodies(a, b, c);
            }

            foreach (var reference in _asm.MainModule.AssemblyReferences)
            {
                if (reference.Name != "System.Private.CoreLib") continue;
                reference.Name = "mscorlib";
                reference.PublicKey = null;
                reference.PublicKeyToken = null;
                reference.HasPublicKey = false;
                reference.Culture = null;
            }

            Debug.Assert(_nativeMethodImpl.Count == 0);

            return _asm;
        }

        private HashSet<string> _builtTypes = new HashSet<string>();

        private void BuildClassFieldsAndMethods(JarFile jar, ClassFile classFile)
        {
            if (_builtTypes.Contains(classFile.ThisClass.Name))
                return;

            var td = classFile.TypeDefinition;
            if (td == null)
                return;

            if (td.BaseType != null)
                BuildClassFieldsAndMethodsFromType(jar, td.BaseType);

            foreach (var ifType in td.Interfaces.Select(i => i.InterfaceType))
            {
                BuildClassFieldsAndMethodsFromType(jar, ifType);
            }

            Debug.Assert(!_builtTypes.Contains(classFile.ThisClass.Name));

            BuildClassFieldsAndMethodsInternal(td, classFile, classFile.ConstantPool);

            Debug.Assert(!_builtTypes.Contains(classFile.ThisClass.Name));
            _builtTypes.Add(classFile.ThisClass.Name);
        }

        private void BuildClassFieldsAndMethodsFromType(JarFile jar, TypeReference ifType)
        {
            var ccf = jar.ClassFiles.FirstOrDefault(c => c.TypeDefinition?.FullName == ifType.FullName);
            if (ccf != null)
                BuildClassFieldsAndMethods(jar, ccf);
        }

        public TypeReference GetOrDefineType(string name)
        {
            if (_typePlugs.TryGetValue(name, out var tr)) return tr;
            if (_typeDefinitions.TryGetValue(name, out var td)) return Import(td);

            if (_jar.ClassFiles.FirstOrDefault(x => x.ThisClass.Name == name) is var cf && cf != null)
            {
                BuildClassDefinition(cf);
                return _typeDefinitions.GetValueOrDefault(name);
            }

            return null;
        }

        public void BuildClassDefinition(ClassFile cf)
        {
            if (_typePlugs.TryGetValue(cf.ThisClass.Name, out var _))
            {
                return;
            }

            if (_typeDefinitions.ContainsKey(cf.ThisClass.Name))
                return;

            //Console.WriteLine("Building type {0}", cf.ThisClass.Name);
            var cp = cf.ConstantPool;
            var className = cf.ThisClass.Name.Split('/');
            var attrs = (TypeAttributes) 0;

            var isAnnot = cf.AccessFlags.HasFlag(ClassFile.Flags.Annotation);
            var isInterface = cf.AccessFlags.HasFlag(ClassFile.Flags.Interface) && !isAnnot;

            if (cf.AccessFlags.HasFlag(ClassFile.Flags.Public))
                attrs |= TypeAttributes.Public;
            if (cf.AccessFlags.HasFlag(ClassFile.Flags.Final))
                attrs |= TypeAttributes.Sealed;
            if (isInterface)
                attrs |= TypeAttributes.Interface;
            if (cf.AccessFlags.HasFlag(ClassFile.Flags.Abstract))
                attrs |= TypeAttributes.Abstract;

            var td = new TypeDefinition(string.Join('.', className.SkipLast(1)), className.Last(), attrs);


            AddJavaNameAttribute(cf.ThisClass.Name, td.CustomAttributes);

            td.Scope = _asm.MainModule;
            _asm.MainModule.Types.Add(td);
            cf.TypeDefinition = td;
            _typeDefinitions[cf.ThisClass.Name] = td;

            if (isAnnot)
                td.BaseType = Import(typeof(Attribute));
            else if (isInterface)
                td.BaseType = null;
            else
                td.BaseType = GetOrDefineType(cf.SuperClass.Name) ?? ResolveTypeReference(cf.SuperClass.Name);

            foreach (var info in cf.Interfaces)
            {
                if (info.Name == "java/lang/annotation/Annotation" && isAnnot) continue;

                var interfaceType = GetOrDefineType(info.Name) ?? ResolveTypeReference(info.Name);
                td.Interfaces.Add(new InterfaceImplementation(interfaceType));
            }

            if (td.IsClass)
            {
                // all java fields are sequential (we need this for some Unsafe operations)
                var baseType = td.BaseType.Resolve();
                if (baseType.FullName == "System.Object" || baseType.IsSequentialLayout || baseType.IsExplicitLayout)
                {
                    // we can't apply 'sequential' if base type is auto-layout (unless it's the Object class)
                    td.IsSequentialLayout = true;
                }
            }
        }

        private void AddJavaNameAttribute(string name, Collection<CustomAttribute> attributes)
        {
            var nameEnc = Encoding.UTF8.GetBytes(name);
            var lenEnc = BitConverter.GetBytes(name.Length);
            var lenBytes = nameEnc.Length <= 127
                ? new byte[] {1, 0, lenEnc[0]}
                : nameEnc.Length <= 0x3fff
                    ? new byte[] {1, 0, (byte) (0x80 | lenEnc[1]), lenEnc[0]}
                    : new byte[] {1, 0, (byte) (0xc0 | lenEnc[3]), lenEnc[2], lenEnc[1], lenEnc[0]};
            var named = new byte[] {0, 0};

            attributes.Add(new CustomAttribute(Import(JavaNameAttribute.Ctor), lenBytes.Concat(nameEnc).Concat(named).ToArray()));
        }


        public void BuildClassFieldsAndMethodsInternal(TypeDefinition td, ClassFile cf, CpInfo[] cp)
        {

            foreach (var fi in cf.Fields)
            {
                try
                {
                    var fd = BuildField(td, fi, cp);
                    td.Fields.Add(fd);
                    _fieldReferences[cf.ThisClass.Name.Replace('/', '.') + "." + fi.Name + ":" + fi.Descriptor] = fd;
                }
                catch (JavaNetException ex) when (ex.Reason == JavaNetException.ReasonType.ClassLoad)
                {
                    Console.WriteLine("Failed to build field {0}::{1}", td.FullName, fi.Name);
                }
            }

            if (_nativeDataTypes.TryGetValue(cf.ThisClass.Name, out var nativeData))
            {
                td.Fields.Add(new FieldDefinition("__nativeData", FieldAttributes.CompilerControlled, nativeData));
            }

            // this thing will contain all the javaMethod-dotnetMethod pairs, for later definition
            var methodPairs  = new List<(JavaMethodInfo, MethodDefinition)>();

            var isAnnot = td.BaseType?.FullName == typeof(Attribute).FullName;



            foreach (var mi in cf.Methods)
            {

                if (isAnnot)
                {
                    // methods become public fields
                    var (rt, _) = ResolveMethodDescriptor(mi.Descriptor);
                    var anotFld = new FieldDefinition(mi.Name, FieldAttributes.Public, rt);
                    td.Fields.Add(anotFld);
                }
                else
                {
                    try
                    {
                        BuildMethod(td, mi, cp, methodPairs);
                    }
                    catch (JavaNetException ex)
                    {
                        Console.WriteLine("Failed to build method {0}::{1}: {2}", td.FullName, mi.Name, ex.Message);
                    }
                }
            }


            if (td.IsClass)
            {
                var interfaces = AllInterfaces(td);
                foreach (var tdInterface in interfaces)
                {
                    foreach (var ifMethod in tdInterface.InterfaceType.Resolve().Methods.Where(x => !x.IsStatic))
                    {
                        var hasMatch = ResolveMethodReference(false,
                            ifMethod.ReturnType,
                            td,
                            ifMethod.Name,
                            ifMethod.Parameters.Select(x => x.ParameterType).ToArray(),
                            false);

                        if (hasMatch != null && !hasMatch.Resolve().IsAbstract)
                            continue;

                        if (hasMatch != null && hasMatch.DeclaringType == td)
                            continue;

                        var impl = CheckMatchingMethodInInterface(td, ifMethod.ReturnType,
                            new TypeReference[] {null}.Concat(ifMethod.Parameters.Select(x => x.ParameterType)).ToArray(),
                            ifMethod.Name + "$default", false);
                        if (impl == null)
                        {
                            if (!td.IsAbstract)
                                throw new Exception($"Unimplemented non-default interface method {ifMethod.FullName} in class {td.FullName}");
                        }

                        if (hasMatch != null && impl == null)
                            continue; // we have a abstract match, and would create an abstract implementation - no need to do that


                        var md = new MethodDefinition(ifMethod.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, ifMethod.ReturnType);
                        md.DeclaringType = td;
                        td.Methods.Add(md);

                        foreach (var parameter in ifMethod.Parameters)
                        {
                            md.Parameters.Add(new ParameterDefinition(parameter.Name, parameter.Attributes, parameter.ParameterType));
                        }

                        if (impl != null)
                        {
                            md.Body = new MethodBody(md);
                            var il = md.Body.GetILProcessor();

                            il.Append(Instruction.Create(OpCodes.Ldarg, md.Body.ThisParameter));

                            foreach (var parameter in md.Parameters)
                            {
                                il.Append(Instruction.Create(OpCodes.Ldarg, parameter));
                            }

                            il.Append(Instruction.Create(OpCodes.Call, impl));
                            il.Append(Instruction.Create(OpCodes.Ret));
                            md.Body.Optimize();
                        }
                        else
                        {
                            md.IsAbstract = true;
                        }

                    }
                }
            }

            _typeThings.Add((td, cf, methodPairs));
        }

        public IEnumerable<InterfaceImplementation> AllInterfaces(TypeDefinition tr)
        {

            return tr?.Interfaces.SelectMany(ii => AllInterfaces(ii.InterfaceType.Resolve()).Concat(new[] {ii})).Concat(AllInterfaces(tr.BaseType?.Resolve())).Distinct(new ByNameInterfaceComparer())
                   ?? new InterfaceImplementation[0];
        }

        private class ByNameInterfaceComparer : IEqualityComparer<InterfaceImplementation>
        {
            public bool Equals(InterfaceImplementation x, InterfaceImplementation y) => x.InterfaceType.FullName == y.InterfaceType.FullName;
            public int GetHashCode(InterfaceImplementation obj) => obj.InterfaceType.FullName.GetHashCode();
        }

        public void BuildClassMethodBodies(TypeDefinition td, ClassFile cf, List<(JavaMethodInfo, MethodDefinition)> methodPairs)
        {

            foreach (var (mi, md) in methodPairs)
            {
                BuildMethodBody(md, td, mi, cf.ConstantPool);
            }

            //Console.WriteLine("Build {0}", td);

        }

        private void BuildMethod(TypeDefinition definingClass, JavaMethodInfo mi, CpInfo[] cp, List<(JavaMethodInfo, MethodDefinition)> methodPairs)
        {
            //Console.WriteLine("  Building method {0} {1}", mi.Name, mi.Descriptor);

            var isInterface = definingClass.IsInterface;
            var isStatic = mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Static);


            var attrs = (MethodAttributes) 0;
            if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Public) || isInterface)
                attrs |= MethodAttributes.Public;
            else if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Protected))
                attrs |= MethodAttributes.FamORAssem;
            else if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Private))
                attrs |= MethodAttributes.Private;
            else
                attrs |= MethodAttributes.FamORAssem;

            if (isStatic)
                attrs |= MethodAttributes.Static;
            else
            {
                // interface methods are not marked virtual in JVM
                // but we need to in CLI

                if (mi.Name != "<init>")
                {
                    attrs |= MethodAttributes.Virtual;
                }


                //if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Final))
                //{
                //    attrs |= MethodAttributes.Final;
                //}

                if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Abstract))
                    attrs |= MethodAttributes.Abstract;
            }

            //if (isInterface)
            //    attrs |= MethodAttributes.NewSlot;

            if (mi.Name == "<init>" || mi.Name == "<clinit>")
                attrs |= MethodAttributes.RTSpecialName | MethodAttributes.SpecialName;

            var (retType, paramType) = ResolveMethodDescriptor(mi.Descriptor);

            var myName = TranslateMethodName(mi.Name, false);

            //if (CheckMatchingMethodInInterface(definingClass, retType, paramType, myName, true) != null)
            //{
            //    // this is an override
            //    attrs |= MethodAttributes.Virtual;
            //}

            if (isInterface && (myName == "GetHashCode" && paramType.Length == 0 
                            || myName == "Equals" && paramType.Length == 1 && paramType[0].FullName == "System.Object"
                            || myName == "ToString" && paramType.Length == 0))
                // this is an interface "override" of an object.Equals, object.GHC or object.ToString method, we don't want those
                return;

            var md = new MethodDefinition(myName, attrs, retType)
            {
                DeclaringType = definingClass
            };

            if (myName != mi.Name) 
                AddJavaNameAttribute(mi.Name, md.CustomAttributes);

            foreach (var param in paramType)
            {
                // TODO: parse parameter names for prettier methods
                md.Parameters.Add(new ParameterDefinition(param));
            }

            var methodSignature = CreateMethodSignature(isStatic, retType.FullName, definingClass.FullName, mi.Name, paramType.Select(x => x.FullName));

            if (!isStatic && isInterface && mi.Attributes.OfType<CodeAttribute>().Any())
            {
                // this is a default-implemented interface method (* fuck Java and their creators *)
                // we need to create a new method that will implement it
                // AND later wire up all implementing classes to it (that didn't implement it explicitly)

                CreateInterfaceDefaultImplementation(definingClass, mi, md, cp, methodPairs, methodSignature);
                md.Attributes |= MethodAttributes.Abstract;
            }


            if (isInterface && !isStatic && definingClass.Interfaces.Any(ii => CheckMatchingMethodInInterface(ii.InterfaceType.Resolve(), mi) != null))
            {
                // this method would hide a inherited one
                // we don't want that

                // HOWEVER, if this is a default implementation of an inherited method WE KEEP
                // THE IMPLEMENTATION, but DISCARD THIS
                return;
            }

            // add the method to all indices, we are doing this

            if (_methodReferences.ContainsKey(methodSignature))
            {
                // plugged method, we just drop it
                return;
            }

            Debug.Assert(!definingClass.Methods.Any(x => x.FullName == md.FullName));
            _methodReferences[methodSignature] = md;
            definingClass.Methods.Add(md);
            methodPairs.Add((mi, md));

        }

        private static void BreakWhen(bool cond)
        {
            Debug.Assert(!cond);
        }

        private MethodDefinition CheckMatchingMethodInInterface(TypeDefinition type, JavaMethodInfo mi)
        {
            var (retType, paramTypes) = ResolveMethodDescriptor(mi.Descriptor);
            return CheckMatchingMethodInInterface(type, retType, paramTypes, TranslateMethodName(mi.Name, false), false);
        }

        private MethodDefinition CheckMatchingMethodInInterface(TypeDefinition type, TypeReference retType, TypeReference[] paramTypes, string name, bool classHierarchy)
        {
            var resolved = type.Methods.FirstOrDefault(md =>
                md.Name == name
                && md.ReturnType.FullName == retType.FullName
                && md.Parameters.Count == paramTypes.Length
                && md.Parameters.Zip(paramTypes, (p1, p2) => p1.ParameterType.FullName == (p2?.FullName ?? type.FullName)).All(b => b));

            if (classHierarchy && type.BaseType?.Resolve() != null)
                resolved = resolved ?? CheckMatchingMethodInInterface(type.BaseType.Resolve(), retType, paramTypes, name, true);

            return resolved ?? type.Interfaces.Select(ii => CheckMatchingMethodInInterface(ii.InterfaceType.Resolve(), retType, paramTypes, name, classHierarchy)).FirstOrDefault(m => m != null);
        }

        private void CreateInterfaceDefaultImplementation(
            TypeDefinition definingClass, 
            JavaMethodInfo mi, 
            MethodDefinition md, 
            CpInfo[] cp, 
            List<(JavaMethodInfo, MethodDefinition)> methodPairs, 
            string methodSignature)
        {
            var defMethod = new MethodDefinition(md.Name + "$default", (md.Attributes | MethodAttributes.Static) & ~MethodAttributes.Virtual, md.ReturnType);
            defMethod.Parameters.Add(new ParameterDefinition("this", ParameterAttributes.None, definingClass));
            foreach (var parameter in md.Parameters)
            {
                defMethod.Parameters.Add(new ParameterDefinition(parameter.Name, parameter.Attributes, parameter.ParameterType));
            }

            _methodReferences[methodSignature + "$default"] = defMethod;

            definingClass.Methods.Add(defMethod);
            methodPairs.Add((mi, defMethod));
        }

        private void BuildMethodBody(MethodDefinition md, TypeDefinition definingClass, JavaMethodInfo mi, CpInfo[] cp)
        {
            var signature = CreateMethodSignature(md.IsStatic, md.ReturnType.FullName, md.DeclaringType.FullName, md.Name, md.Parameters.Select(x => x.ParameterType.FullName));
            if (_nativeMethodImpl.TryGetValue(signature, out var impl))
            {
                _nativeMethodImpl.Remove(signature);
                var resolvedImpl = impl.Resolve();
                md.Body = new MethodBody(md);
                var processor = md.Body.GetILProcessor();

                var i = 0;

                if (md.HasThis)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg, md.Body.ThisParameter));
                    i++;
                }

                foreach (var parameter in md.Parameters)
                {
                    processor.Append(Instruction.Create(OpCodes.Ldarg, parameter));
                    i++;
                }

                foreach (var param in resolvedImpl.Parameters.Skip(i))
                {
                    switch (param.CustomAttributes.FirstOrDefault())
                    {
                        case null: throw new Exception("Unattributed extra parameter: " + param.Name);
                        case var nativeDataParam when nativeDataParam.AttributeType.FullName == typeof(NativeDataParamAttribute).FullName:
                        {
                            if (md.HasThis)
                            {
                                processor.Append(Instruction.Create(OpCodes.Ldarg, md.Body.ThisParameter));
                                processor.Append(Instruction.Create(OpCodes.Ldflda, definingClass.Fields.Single(x => x.Name == "__nativeData")));
                            }
                            else
                            {
                                processor.Append(Instruction.Create(OpCodes.Ldsflda, definingClass.Fields.Single(x => x.Name == "__staticNativeData")));
                            }

                            break;
                        }
                        case var typeHandle when typeHandle.AttributeType.FullName == typeof(TypeHandleAttribute).FullName:
                        {
                            var s = (string) typeHandle.ConstructorArguments[0].Value;
                            processor.Append(Instruction.Create(OpCodes.Ldtoken, ResolveTypeReference(s)));
                            processor.Append(Instruction.Create(OpCodes.Call, Import(typeof(Type).GetMethod("GetTypeFromHandle"))));
                            break;
                        }
                        case var methodPtr when methodPtr.AttributeType.FullName == typeof(MethodPtrAttribute).FullName:
                        {
                            var isStatic = (bool) methodPtr.ConstructorArguments[0].Value;
                            var retType = (string) methodPtr.ConstructorArguments[1].Value;
                            var name = (string) methodPtr.ConstructorArguments[2].Value;
                            var argTypes = ((CustomAttributeArgument[]) methodPtr.ConstructorArguments[3].Value).Select(x => (string)x.Value).ToArray();

                            if (!isStatic && !md.HasThis)
                                throw new Exception("Can't handle instance method reference in static method");

                            processor.Append(md.HasThis && !isStatic ? Instruction.Create(OpCodes.Ldarg, md.Body.ThisParameter) : Instruction.Create(OpCodes.Ldnull));
                            var targetMethod = definingClass.Methods.Single(x =>
                                x.IsStatic == isStatic
                                && x.ReturnType.FullName == retType
                                && x.Name == name
                                && x.Parameters.Count == argTypes.Length
                                && x.Parameters.Zip(argTypes, (definition,  s) => definition.ParameterType.FullName == s).All(b => b));
                            processor.Append(Instruction.Create(OpCodes.Ldftn, targetMethod));

                            if (targetMethod.ReturnType.FullName == "System.Void")
                            {
                                var numParams = targetMethod.Parameters.Count;
                                TypeDefinition actionType;
                                if (numParams == 0)
                                {
                                    actionType = SystemImport(typeof(Action)).Resolve();
                                }
                                else
                                {
                                    actionType = SystemImport(Type.GetType($"System.Action`{numParams}"))
                                        .MakeGenericInstanceType(targetMethod.Parameters.Select(x => x.ParameterType).ToArray())
                                        .Resolve();
                                }

                                processor.Append(Instruction.Create(OpCodes.Newobj, _asm.MainModule.ImportReference(actionType.GetConstructors().Single())));
                            }
                            else
                            {
                                var numParams = targetMethod.Parameters.Count;
                                var funcType = SystemImport(Type.GetType($"System.Func`{numParams + 1}"))
                                    .MakeGenericInstanceType(targetMethod.Parameters.Select(x => x.ParameterType).Concat(new[] {targetMethod.ReturnType}).ToArray())
                                    .Resolve();

                                processor.Append(Instruction.Create(OpCodes.Newobj, _asm.MainModule.ImportReference(funcType.GetConstructors().Single())));
                            }
                            break;
                        }
                        case var fieldPtr when fieldPtr.AttributeType.FullName == typeof(FieldPtrAttribute).FullName:
                        {
                            var name = (string) fieldPtr.ConstructorArguments[0].Value;
                            var isStatic = (bool) fieldPtr.ConstructorArguments[1].Value;
                            var tgtField = definingClass.Fields.Single(f => f.IsStatic == isStatic && f.Name == name);
                            if (isStatic)
                            {

                                processor.Append(Instruction.Create(OpCodes.Ldsflda, tgtField));
                            }
                            else
                            {
                                if (!md.HasThis)
                                    throw new Exception("Can't handle instance field reference in static method");
                                processor.Append(Instruction.Create(OpCodes.Ldarg, md.Body.ThisParameter));
                                processor.Append(Instruction.Create(OpCodes.Ldflda, tgtField));
                            }
                            break;
                        }
                        case var unknown:
                            throw new Exception("Unknown attribute " + unknown.AttributeType.FullName);
                    }
                }

                processor.Append(Instruction.Create(OpCodes.Call, impl));
                processor.Append(Instruction.Create(OpCodes.Ret));
                return;
            }


            if ((mi.AccessFlags & JavaMethodInfo.Flags.Native) != 0)
            {
                md.Body = new MethodBody(md);
                var processor = md.Body.GetILProcessor();
                processor.Append(Instruction.Create(OpCodes.Ldstr, "Native method stub: " + md.FullName));
                processor.Append(Instruction.Create(OpCodes.Newobj, _asm.MainModule.ImportReference(typeof(TypeLoadException).GetConstructor(new[] { typeof(string) }))));
                processor.Append(Instruction.Create(OpCodes.Throw));
                return;
            }

            foreach (var attributeInfo in mi.Attributes)
            {
                switch (attributeInfo)
                {
                    case CodeAttribute code:
                        try
                        {
                            md.Body = MethodGenerator.GenerateMethod(md, mi, code, cp);
                            Debug.Assert(md.Body != null);
                        }
                        catch (JavaNetException ex)
                        {
                            Console.WriteLine(ex.Message);
                            md.Body = new MethodBody(md);
                            var processor = md.Body.GetILProcessor();
                            processor.Append(Instruction.Create(OpCodes.Ldstr, ex.Reason + " " + ex.Message));
                            processor.Append(Instruction.Create(OpCodes.Newobj, _asm.MainModule.ImportReference(typeof(TypeLoadException).GetConstructor(new[] {typeof(string)}))));
                            processor.Append(Instruction.Create(OpCodes.Throw));
                        }

                        break;
                }
            }
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
            if (field != null)
                field = _asm.MainModule.ImportReference(field);
            _fieldReferences[fmi.Represent()] = field;
            return field ?? throw new JavaNetException(JavaNetException.ReasonType.ClassLoad, "Could not resolve field " + fmi.Represent());
        }

        //public static IEnumerable<MethodReference> GetAllMethods(TypeDefinition td)
        //{
        //    return td == null ? Enumerable.Empty<MethodReference>() : td.GetMethods().Concat(GetAllMethods(td.BaseType?.Resolve()));
        //}

        public static string CreateMethodSignature(bool isStatic, string returnType, string type, string name, IEnumerable<string> paramTypes)
        {
            return $"{type}.{name}(" + string.Join(',', paramTypes) + "):" + returnType;
        }

        public MethodReference ResolveMethodReference(bool isStatic, TypeReference retType, TypeDefinition type, string name, TypeReference[] paramTypes, bool goIntoInterfaces = true)
        {
            var signature = CreateMethodSignature(isStatic, retType.FullName, type.FullName, name, paramTypes.Select(x => x.FullName));
            if (_methodPlugs.TryGetValue(signature, out var plug)) return plug;
            if (!_methodReferences.TryGetValue(signature, out var resolvedMethod))
            {

                if (name == ".cctor")
                    resolvedMethod = type.GetStaticConstructor();
                else
                {
                    var normalizedName = char.ToUpper(name[0]) + name.Substring(1);

                    var resolvedMethods = (name == ".ctor" ? type.GetConstructors() : type.Methods)
                        .Where(x =>
                            (x.Name == name || x.Name == normalizedName)
                            && x.IsStatic == isStatic
                            && x.ReturnType.FullName == retType.FullName
                            && x.Parameters.Count == paramTypes.Length
                            && x.Parameters.Zip(paramTypes,
                                (param, typeDef) =>
                                    ((string) param.CustomAttributes.FirstOrDefault(ca => ca.AttributeType.Name == "ActualTypeAttribute")?.ConstructorArguments[0].Value
                                     ?? param.ParameterType.FullName)
                                    == typeDef.FullName).All(b => b))
                        .ToList();


                    resolvedMethod = resolvedMethods.SingleOrDefault();

                    if (resolvedMethod != null)
                        resolvedMethod = _asm.MainModule.ImportReference(resolvedMethod);

                    if (resolvedMethod == null && type.BaseType != null)
                        resolvedMethod = ResolveMethodReference(isStatic, retType, type.BaseType.Resolve(), name, paramTypes, goIntoInterfaces);

                    if (resolvedMethod == null && type.IsInterface) // interfaces don't have a base type, but we still need to check in the Object class
                        resolvedMethod = ResolveMethodReference(isStatic, retType, _asm.MainModule.TypeSystem.Object.Resolve(), name, paramTypes, goIntoInterfaces);

                    if (goIntoInterfaces)
                    {
                        foreach (var sInterface in type.Interfaces)
                        {
                            if (resolvedMethod == null)
                                resolvedMethod = ResolveMethodReference(isStatic, retType, sInterface.InterfaceType.Resolve(), name, paramTypes, goIntoInterfaces);
                        }
                    }
                }
            }

            Debug.Assert(name != "getKey" || resolvedMethod != null);

            return resolvedMethod;
        }

        public MethodReference ResolveMethodReference(bool isStatic, FieldOrMethodrefInfo fmi, bool goIntoInterfaces)
        {
            var declType = ResolveTypeReference(fmi.Class.Name).Resolve();
            var (retType, paramTypes) = ResolveMethodDescriptor(fmi.NameAndType.Descriptor);
            var translatedMethodName = TranslateMethodName(fmi.NameAndType.Name, true);
            var resolvedMethod = ResolveMethodReference(isStatic, retType, declType, translatedMethodName, paramTypes, goIntoInterfaces);
            var signature = CreateMethodSignature(isStatic, retType.FullName, declType.FullName, translatedMethodName, paramTypes.Select(x => x.FullName));
            _methodReferences[signature] = resolvedMethod;

            //Debug.Assert(resolvedMethod != null);

            return resolvedMethod;
        }

        public  string TranslateMethodName(string name, bool calling)
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
                case "hashCode" when !calling: // overriders etc need to override GetHashCode, but callers stil call hashCode()
                    return "GetHashCode";
                case "finalize":
                    return "Finalize";
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

        private FieldDefinition BuildField(TypeDefinition declaringClass, JavaFieldInfo fi, CpInfo[] cp)
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


            if ((fi.AccessFlags & JavaFieldInfo.Flags.Volatile) != 0)
            {
                fieldType = fieldType.MakeRequiredModifierType(Import(typeof(IsVolatile)));
            }

            var fd = new FieldDefinition(fi.Name, attrs, fieldType);
            fd.DeclaringType = declaringClass;

            if ((fi.AccessFlags & JavaFieldInfo.Flags.Synthetic) != 0)
            {
                fd.CustomAttributes.Add(new CustomAttribute(Import(typeof(CompilerGeneratedAttribute).GetConstructor(new Type[0]))));
            }



            foreach (var attributeInfo in fi.Attributes)
            {
                switch (attributeInfo)
                {
                    case ConstantValueAttribute cva:
                        fd.Attributes |= FieldAttributes.Literal;
                        fd.Attributes &= ~FieldAttributes.InitOnly;
                        fd.HasConstant = true;
                        switch (fi.Descriptor)
                        {
                            case "J":
                                fd.Constant = ((LongInfo) cva.Value).Value;
                                break;
                            case "F":
                                fd.Constant = ((FloatInfo) cva.Value).Value;
                                break;
                            case "D":
                                fd.Constant = ((DoubleInfo) cva.Value).Value;
                                break;
                            case "I":
                                fd.Constant = ((IntegerInfo) cva.Value).Value;
                                break;
                            case "S":
                                fd.Constant = (short) ((IntegerInfo) cva.Value).Value;
                                break;
                            case "B":
                                fd.Constant = (byte) ((IntegerInfo) cva.Value).Value;
                                break;
                            case "C":
                                fd.Constant = (char) ((IntegerInfo) cva.Value).Value;
                                break;
                            case "Z":
                                fd.Constant = ((IntegerInfo) cva.Value).Value != 0;
                                break;
                            case "Ljava/lang/String;":
                                fd.Constant = ((StringInfo) cva.Value).String;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(fi.Descriptor), fi.Descriptor, "Unknown constant");
                        }

                        Debug.Assert(fd.Constant != null);

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

        public TypeReference Import(TypeReference type)
        {
            return _asm.MainModule.ImportReference(type);
        }
    }
}