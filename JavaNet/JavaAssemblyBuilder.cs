using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using JavaNet.Runtime.Plugs;
using JavaNet.Runtime.Plugs.NativeImpl;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
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
        private readonly Dictionary<string, TypeReference> _typePlugs = new Dictionary<string, TypeReference>();
        public Dictionary<string, MethodReference> CastPlugs { get; } = new Dictionary<string, MethodReference>();
        public Dictionary<string, MethodReference> InstanceOfPlugs { get; } = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, TypeReference> _typeReferences = new Dictionary<string, TypeReference>();
        private readonly Dictionary<string, TypeDefinition> _typeDefinitions = new Dictionary<string, TypeDefinition>();
        private readonly Dictionary<string, MethodReference> _methodReferences = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, MethodReference> _methodPlugs = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, MethodReference> _methodImpl = new Dictionary<string, MethodReference>();
        private readonly Dictionary<string, FieldReference> _fieldReferences = new Dictionary<string, FieldReference>();
        private readonly HashSet<string> _annotations = new HashSet<string>();

        private AssemblyDefinition _asm;

        public TypeReference Import(Type t)
        {
            return _asm.MainModule.ImportReference(t);
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
            _typePlugs["java/lang/Object"] = asm.MainModule.TypeSystem.Object;
            _typePlugs["java/lang/String"] = asm.MainModule.TypeSystem.String;
            _typePlugs["java/lang/Throwable"] = SystemImport(typeof(Exception));

            _typePlugs["java/lang/Class"] = SystemImport(typeof(Type));
            _typePlugs["java/lang/annotation/Annotation"] = SystemImport(typeof(Attribute));
            _typePlugs["java/lang/reflect/AccessibleObject"] = SystemImport(typeof(MemberInfo));
            _typePlugs["java/lang/reflect/Constructor"] = SystemImport(typeof(ConstructorInfo));
            _typePlugs["java/lang/reflect/Field"] = SystemImport(typeof(FieldInfo));
            _typePlugs["java/lang/reflect/Method"] = SystemImport(typeof(MethodInfo));

            _typePlugs["java/lang/ArgumentException"] = SystemImport(typeof(ArgumentException));
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
                    Console.WriteLine("plugging {0}", tpa.Name ?? type.FullName);
                    _typePlugs[(tpa.Name ?? type.FullName).Replace('.', '/')] = asm.MainModule.ImportReference(type);
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
                        _methodPlugs[signature] = asm.MainModule.ImportReference(method);
                        _methodReferences[signature] = asm.MainModule.ImportReference(method);
                    }

                    foreach (var mpa in method.GetCustomAttributes<NativeImplAttribute>())
                    {
                        var signature = CreateMethodSignature(
                            mpa.IsStatic,
                            mpa.ReturnType ?? method.ReturnType.FullName,
                            mpa.DeclaringType,
                            mpa.MethodName,
                            mpa.ArgTypes);
                        _methodImpl[signature] = asm.MainModule.ImportReference(method);
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
                _typeReferences[typeName] = _asm.MainModule.ImportReference(type);
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
                BuildClass(classFile);
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
                    }
                }
            }

            foreach (var (a, b, c) in _typeThings)
            {
                BuildClassPart2(a, b, c);
            }

            return _asm;
        }

        public TypeReference GetOrDefineType(string name)
        {
            if (_typePlugs.TryGetValue(name, out var tr)) return tr;
            if (_typeDefinitions.TryGetValue(name, out var td)) return td.Module != _asm.MainModule ? _asm.MainModule.ImportReference(td) : td;

            if (_jar.ClassFiles.FirstOrDefault(x => x.ThisClass.Name == name) is var cf && cf != null)
            {
                BuildClass(cf);
                return _typeDefinitions.GetValueOrDefault(name);
            }

            return null;
        }

        public void BuildClass(ClassFile cf)
        {
            if (_typePlugs.ContainsKey(cf.ThisClass.Name))
            {
                //Console.WriteLine("Skipping plugget type {0}", cf.ThisClass.Name);
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

            td.Scope = _asm.MainModule;
            _asm.MainModule.Types.Add(td);

            _typeDefinitions[cf.ThisClass.Name] = td;

            if (isAnnot)
                td.BaseType = _asm.MainModule.ImportReference(typeof(Attribute));
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

            // this thing will contain all the javaMethod-dotnetMethod pairs, for later definition
            var methodPairs  = new List<(JavaMethodInfo, MethodDefinition)>();

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
                        var md = BuildMethod(td, mi, cp, methodPairs);
                    }
                    catch (JavaNetException ex)
                    {
                        Console.WriteLine("Failed to build method {0}::{1}: {2}", td.FullName, mi.Name, ex.Message);
                    }
                }
            }

            if (td.IsClass)
            {
                foreach (var tdInterface in td.Interfaces)
                {
                    foreach (var ifMethod in tdInterface.InterfaceType.Resolve().Methods.Where(x => !x.IsStatic))
                    {
                        var hasMatch = ResolveMethodReference(false, ifMethod.ReturnType, td, ifMethod.Name, ifMethod.Parameters.Select(x => x.ParameterType).ToArray());

                        if (hasMatch != null) continue;

                        var sign = CreateMethodSignature(false, ifMethod.ReturnType.FullName, ifMethod.DeclaringType.FullName, ifMethod.Name, ifMethod.Parameters.Select(x => x.ParameterType.FullName));
                        if (!_methodReferences.TryGetValue(sign + "$default", out var impl) || impl == null)
                        {
                            if (!td.IsAbstract)
                                throw new Exception($"Unimplemented non-default interface method {ifMethod.FullName} in class {td.FullName}");
                        }

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

        public void BuildClassPart2(TypeDefinition td, ClassFile cf, List<(JavaMethodInfo, MethodDefinition)> methodPairs)
        {

            foreach (var (mi, md) in methodPairs)
            {
                BuildMethodBody(md, td, mi, cf.ConstantPool);
            }

            //Console.WriteLine("Build {0}", td);

        }

        private MethodDefinition BuildMethod(TypeDefinition definingClass, JavaMethodInfo mi, CpInfo[] cp, List<(JavaMethodInfo, MethodDefinition)> methodPairs)
        {
            //Console.WriteLine("  Building method {0} {1}", mi.Name, mi.Descriptor);

            var isInterface = definingClass.IsInterface;

            var attrs = MethodAttributes.HideBySig;
            if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Public))
                attrs |= MethodAttributes.Public;
            else if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Protected))
                attrs |= MethodAttributes.Family;
            else if (mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Private))
                attrs |= MethodAttributes.Private;

            var isStatic = mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Static);
            if (isStatic)
                attrs |= MethodAttributes.Static;

            if (isInterface
                || !mi.AccessFlags.HasFlag(JavaMethodInfo.Flags.Final)
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

            var myName = TranslateMethodName(mi.Name);
            
            var md = new MethodDefinition(myName, attrs, retType)
            {
                DeclaringType = definingClass
            };

            foreach (var param in paramType)
            {
                md.Parameters.Add(new ParameterDefinition(param));
            }

            var methodSignature = CreateMethodSignature(isStatic, retType.FullName, definingClass.FullName, mi.Name, paramType.Select(x => x.FullName));
            _methodReferences[methodSignature] = md;


            if (!isStatic && isInterface && mi.Attributes.OfType<CodeAttribute>().Any())
            {
                // this is a default-implemented interface method (* fuck Java and their creators *)
                // we need to create a new method that will implement it
                // AND later wire up all implementing classes to it (that didn't implement it explicitly)

                CreateInterfaceDefaultImplementation(definingClass, mi, md, cp, methodPairs, methodSignature);
                md.Attributes |= MethodAttributes.Abstract;
            }

            definingClass.Methods.Add(md);
            methodPairs.Add((mi, md));

            return md;
        }

        private void CreateInterfaceDefaultImplementation(
            TypeDefinition definingClass, 
            JavaMethodInfo mi, 
            MethodDefinition md, 
            CpInfo[] cp, 
            List<(JavaMethodInfo, MethodDefinition)> methodPairs, 
            string methodSignature)
        {
            var defMethod = new MethodDefinition(md.Name + "_defaultImpl", (md.Attributes | MethodAttributes.Static) & ~MethodAttributes.Virtual, md.ReturnType);
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
            if (_methodImpl.TryGetValue(signature, out var impl))
            {
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
                                    .MakeGenericInstanceType(new[] {targetMethod.ReturnType}.Concat(targetMethod.Parameters.Select(x => x.ParameterType)).ToArray())
                                    .Resolve();

                                processor.Append(Instruction.Create(OpCodes.Newobj, _asm.MainModule.ImportReference(funcType.GetConstructors().Single())));
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
                processor.Append(Instruction.Create(OpCodes.Ret));
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
                            processor.Append(Instruction.Create(OpCodes.Ret));
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
            return $"{(isStatic ? 's' : 'i')}:{type}.{name}(" + string.Join(',', paramTypes) + "):" + returnType;
        }

        public MethodReference ResolveMethodReference(bool isStatic, TypeReference retType, TypeDefinition type, string name, TypeReference[] paramTypes)
        {
            var signature = CreateMethodSignature(isStatic, retType.FullName, type.FullName, name, paramTypes.Select(x => x.FullName));
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
                            && x.Parameters.Zip(paramTypes, (param, typeDef) => param.ParameterType.FullName == typeDef.FullName).All(b => b))
                        .ToList();


                    resolvedMethod = resolvedMethods.SingleOrDefault();

                    if (resolvedMethod != null)
                        resolvedMethod = _asm.MainModule.ImportReference(resolvedMethod);

                    if (resolvedMethod == null && type.BaseType != null)
                        resolvedMethod = ResolveMethodReference(isStatic, retType, type.BaseType.Resolve(), name, paramTypes);

                    if (type.IsInterface)
                    {
                        foreach (var sInterface in type.Interfaces)
                        {
                            if (resolvedMethod == null)
                                resolvedMethod = ResolveMethodReference(isStatic, retType, sInterface.InterfaceType.Resolve(), name, paramTypes);
                        }
                    }
                }
            }

            return resolvedMethod;
        }

        public MethodReference ResolveMethodReference(bool isStatic, FieldOrMethodrefInfo fmi)
        {
            var declType = ResolveTypeReference(fmi.Class.Name).Resolve();
            var (retType, paramTypes) = ResolveMethodDescriptor(fmi.NameAndType.Descriptor);
            var translatedMethodName = TranslateMethodName(fmi.NameAndType.Name);
            var resolvedMethod = ResolveMethodReference(isStatic, retType, declType, translatedMethodName, paramTypes);
            var signature = CreateMethodSignature(isStatic, retType.FullName, declType.FullName, translatedMethodName, paramTypes.Select(x => x.FullName));
            _methodReferences[signature] = resolvedMethod;

            //Debug.Assert(resolvedMethod != null);

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

        public TypeReference Import(TypeReference type)
        {
            return _asm.MainModule.ImportReference(type);
        }
    }
}