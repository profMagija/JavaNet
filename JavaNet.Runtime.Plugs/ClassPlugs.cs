using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JavaNet.Runtime.Plugs
{
    public static class ClassPlugs
    {
        [MethodPlug(typeof(Type), "forName", typeof(string), IsStatic = true)]
        public static Type ForName(string name)
        {
            if (name.StartsWith("["))
                return ForName(name.Substring(1)).MakeArrayType();

            if (name.StartsWith("L") && name.EndsWith(";"))
                name = name.Substring(1, name.Length - 2);
            var type = Type.GetType(name, Assembly.Load, TypeResolver);

            if (type == null)
                throw PlugHelpers.ThrowForName("java.lang.ClassNotFoundException, JavaNet.Runtime");

            return type;
        }

        private static Type TypeResolver(Assembly assembly, string name, bool ignoreCase)
        {
            var stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            assembly = assembly ?? Assembly.Load("JavaNet.Runtime") ?? throw new Exception();

            foreach (var type in assembly.GetTypes())
            {
                if (type.FullName == null)
                    continue;

                if (type.FullName.Equals(name, stringComparison))
                    return type;

                if (type.GetCustomAttributes<JavaNameAttribute>().Any(jna => jna.Name.Equals(name, stringComparison)))
                    return type;
            }

            return null;
        }

        [MethodPlug("System.Type", "System.Type", "forName", "System.String", "System.Boolean", "java.lang.ClassLoader", IsStatic = true)]
        public static Type ForName(string name, bool initialize, [ActualType("java.lang.ClassLoader")] object loader)
        {
            if (loader != null)
                throw new ArgumentException("Can't use a non-bootstrap loader", nameof(loader));

            return ForName(name);
        }

        [MethodPlug(typeof(Type), "getPrimitiveClass", typeof(string), IsStatic = true)]
        public static Type GetPrimitiveClass(string name)
        {
            switch (name)
            {
                case "int": return typeof(int);
                case "long": return typeof(long);
                case "float": return typeof(float);
                case "double": return typeof(double);
                case "void": return typeof(void);
                case "char": return typeof(char);
                case "short": return typeof(short);
                case "byte": return typeof(byte);
                case "boolean": return typeof(bool);
                default:
                    throw new ArgumentOutOfRangeException(nameof(name));
            }
        }

        [MethodPlug(typeof(Type), "newInstance")]
        public static object NewInstance(Type t)
        {
            return Activator.CreateInstance(t);
        }

        [MethodPlug(typeof(Type), "isInstance", typeof(object))]
        public static object IsInstance(Type t, object obj)
        {
            return t.IsInstanceOfType(obj);
        }

        [MethodPlug(typeof(Type), "isAssignableFrom", typeof(Type))]
        public static bool IsAssignableFrom(Type t, Type other)
        {
            return t.IsAssignableFrom(other);
        }

        [MethodPlug(typeof(Type), "isInterface")]
        public static bool IsInterface(Type t)
        {
            return t.IsInterface || t.IsSubclassOf(typeof(Attribute));
        }

        [MethodPlug(typeof(Type), "isArray")]
        public static bool IsArray(Type t)
        {
            return t.IsArray;
        }

        [MethodPlug(typeof(Type), "isPrimitive")]
        public static bool IsPrimitive(Type t)
        {
            return t.IsPrimitive;
        }

        [MethodPlug(typeof(Type), "isAnnotation")]
        public static bool IsAnnotation(Type t)
        {
            return t.IsSubclassOf(typeof(Attribute));
        }

        [MethodPlug(typeof(Type), "isSynthetic")]
        public static bool IsSynthetic(Type t)
        {
            return t.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }

        [MethodPlug(typeof(Type), "getName")]
        public static string GetName(Type t)
        {
            if (t == typeof(byte))   return "byte";
            if (t == typeof(bool))   return "boolean";
            if (t == typeof(char))   return "char";
            if (t == typeof(int))    return "int";
            if (t == typeof(float))  return "float";
            if (t == typeof(double)) return "double";
            if (t == typeof(long))   return "long";
            if (t == typeof(void))   return "void";
            if (t == typeof(short))  return "short";
            if (t.IsArray) return "[" + GetInternalName(t);
            return t.FullName;
        }

        private static string GetInternalName(Type t)
        {
            if (t == typeof(byte))   return "B";
            if (t == typeof(bool))   return "Z";
            if (t == typeof(char))   return "C";
            if (t == typeof(int))    return "I";
            if (t == typeof(float))  return "F";
            if (t == typeof(double)) return "D";
            if (t == typeof(long))   return "J";
            if (t == typeof(short))  return "S";
            if (t.IsArray) return "[" + GetInternalName(t);
            return "L" + t.FullName + ";";
        }

        [MethodPlug("java.lang.ClassLoader", "System.Type", "getClassLoader")]
        [MethodPlug("java.lang.ClassLoader", "System.Type", "getClassLoader0")]
        [return: ActualType("java.lang.ClassLoader")]
        public static object GetClassLoader(Type t) => null;

        [MethodPlug(typeof(Type), "getSuperclass")]
        public static Type GetSuperclass(Type t) => t.BaseType;
        
        [MethodPlug(typeof(Type), "getInterfaces")]
        public static Type[] GetInterfaces(Type t) => t.GetInterfaces();

        [MethodPlug(typeof(Type), "getComponentType")]
        public static Type GetComponentType(Type t) => t.GetElementType();

        [MethodPlug("java.lang.reflect.Type[]", "System.Type", "getGenericInterfaces")]
        public static Type[] GetGenericInterfaces(Type t) => GetInterfaces(t);

        [MethodPlug(typeof(Type), "getModifiers")]
        public static int GetModifiers(Type t)
        {
            var rt = (JavaModifiers) 0;
            if (t.IsPublic)
                rt |= JavaModifiers.PUBLIC;
            else if (t.IsNestedFamORAssem)
                rt |= JavaModifiers.PROTECTED;
            else
                rt |= JavaModifiers.PRIVATE;

            if (t.IsSealed)
                rt |= JavaModifiers.FINAL;

            if (t.IsAbstract && t.IsSealed)
                rt |= JavaModifiers.STATIC;

            if (t.IsAbstract)
                rt |= JavaModifiers.ABSTRACT;

            if (t.IsInterface)
                rt |= JavaModifiers.INTERFACE;

            return (int) rt;
        }

        [MethodPlug(typeof(Type), "getSigners")]
        public static object[] GetSigners(Type t) => null;

        [MethodPlug(typeof(Type), "getEnclosingMethod")]
        public static MethodInfo GetEnclosingMethod(Type t) => t.DeclaringMethod as MethodInfo;

        [MethodPlug(typeof(Type), "getEnclosingConstructor")]
        public static ConstructorInfo GetEnclosingConstructor(Type t) => t.DeclaringMethod as ConstructorInfo;

        [MethodPlug(typeof(Type), "getDeclaringClass")]
        public static Type GetDeclaringClass(Type t) =>
            t.DeclaringMethod == null && !IsSynthetic(t) ? t.DeclaringType : null;

        [MethodPlug(typeof(Type), "getEnclosingClass")]
        public static Type GetEnclosingClass(Type t) => t.DeclaringType;

        [MethodPlug(typeof(Type), "getSimpleName")]
        public static string GetSimpleName(Type t)
        {
            if (t.IsArray)
                return GetSimpleName(t.GetElementType()) + "[]";
            if (t.IsPointer)
                return GetSimpleName(t.GetElementType()) + "*";
            if (IsSynthetic(t))
                return "";
            return t.Name;
        }

        [MethodPlug(typeof(Type), "getCanonicalName")]
        public static string GetCanonicalName(Type t) => IsSynthetic(t) ? null : t.FullName;

        [MethodPlug(typeof(Type), "isAnonymousClass")]
        public static bool IsAnonymousClass(Type t) => IsSynthetic(t);

        [MethodPlug(typeof(Type), "isLocalClass")]
        public static bool IsLocalClass(Type t) => t.DeclaringMethod != null;

        [MethodPlug(typeof(Type), "isMemberClass")]
        public static bool IsMemberClass(Type t) => t.DeclaringType != null && !IsLocalClass(t);

        [MethodPlug(typeof(Type), "getClasses")]
        public static Type[] GetClasses(Type t)
        {
            var l = new List<Type>();
            while (t != null)
            {
                l.AddRange(t.GetNestedTypes());
                t = t.BaseType;
            }

            return l.ToArray();
        }

        [MethodPlug(typeof(Type), "getFields")]
        public static FieldInfo[] GetFields(Type t)
        {
            var l = new List<FieldInfo>();
            while (t != null)
            {
                l.AddRange(t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance));
                t = t.BaseType;
            }

            return l.ToArray();
        }

        [MethodPlug(typeof(Type), "getMethods")]
        public static MethodInfo[] GetMethods(Type t)
        {
            var l = new List<MethodInfo>();
            while (t != null)
            {
                l.AddRange(t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance));
                t = t.BaseType;
            }

            return l.ToArray();
        }

        [MethodPlug(typeof(Type), "getConstructors")]
        public static ConstructorInfo[] GetConstructors(Type t)
        {
            return t.GetConstructors();
        }

        [MethodPlug(typeof(Type), "getField", typeof(string))]
        public static FieldInfo GetField(Type t, string name)
        {
            return GetFieldInternal(t, name) ?? throw new MissingFieldException(t.FullName, name);
        }

        private static FieldInfo GetFieldInternal(Type t, string name)
        {
            var fld = t.GetField(name);
            if (fld != null)
            {
                return fld;
            }

            foreach (var superInterface in t.GetInterfaces())
            {
                fld = GetFieldInternal(superInterface, name);
                if (fld != null)
                {
                    return fld;
                }
            }

            if (t.BaseType != null)
                fld = GetFieldInternal(t.BaseType, name);

            return fld;
        }

        [MethodPlug(typeof(Type), "getMethod", typeof(string), typeof(Type[]))]
        public static MethodInfo GetMethod(Type t, string name, Type[] parameterTypes) =>
            GetMethodInternal(t, name, parameterTypes) ?? throw new MissingMethodException(t.FullName, name);

        private static MethodInfo GetMethodInternal(Type t, string name, Type[] parameterTypes)
        {
            if (name == "<init>" || name == "<clinit>")
                return null;

            var m = t.GetMethod(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, parameterTypes, null);

            if (m != null) return m;

            if (t.BaseType != null)
                m = GetMethodInternal(t.BaseType, name, parameterTypes);

            if (m != null) return m;

            foreach (var superInterface in t.GetInterfaces())
            {
                m = GetMethodInternal(superInterface, name, parameterTypes);
                if (m != null) return m;
            }

            return null;
        }

        [MethodPlug(typeof(Type), "getConstructor", typeof(Type[]))]
        public static ConstructorInfo GetConstructor(Type t, Type[] parameterTypes)
        {
            return t.GetConstructor(BindingFlags.Public, null, parameterTypes, null);
        }

        [MethodPlug(typeof(Type), "getDeclaredClasses")]
        public static Type[] GetDeclaredClasses(Type t) => t.GetNestedTypes();

        [MethodPlug(typeof(Type), "getDeclaredFields")]
        public static FieldInfo[] GetDeclaredFields(Type t) => t.GetFields();

        [MethodPlug(typeof(Type), "getDeclaredMethods")]
        public static MethodInfo[] GetDeclaredMethods(Type t) => t.GetMethods();

        [MethodPlug(typeof(Type), "getDeclaredConstructors")]
        public static ConstructorInfo[] GetDeclaredConstructors(Type t) => t.GetConstructors();

        [MethodPlug(typeof(Type), "getDeclaredField", typeof(string))]
        public static FieldInfo GetDeclaredField(Type t, string name)
        {
            Console.WriteLine("searching for {0}.{1}", t, name);
            return t.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        }

        [MethodPlug(typeof(Type), "getDeclaredMethod", typeof(string), typeof(Type[]))]
        public static MethodInfo GetDeclaredField(Type t, string name, Type[] paramTypes) => t.GetMethod(name, paramTypes);

        [MethodPlug(typeof(Type), "getDeclaredConstructor", typeof(Type[]))]
        public static ConstructorInfo GetDeclaredConstructor(Type t, Type[] paramTypes) => t.GetConstructor(paramTypes);

        [MethodPlug(typeof(Type), "getResourceAsStream", typeof(string))]
        [return: ActualType("java.io.InputStream")]
        public static object GetResourceAsStream(Type t, string name) => null;

        [MethodPlug(typeof(Type), "getResource", typeof(string))]
        [return: ActualType("java.net.URL")]
        public static object GetResource(Type t, string name) => null;

        [MethodPlug(typeof(Type), "desiredAssertionStatus")]
        public static bool DesiredAssertionStatus(Type t) => false;

        [MethodPlug(typeof(Type), "isEnum")]
        public static bool IsEnum(Type t) => t.IsEnum || t.BaseType?.FullName == "java.lang.Enum";

        [MethodPlug(typeof(Type), "getEnumConstants")]
        public static object[] GetEnumConstants(Type t)
        {
            if (t.IsEnum)
                return t.GetEnumValues().Cast<object>().ToArray();

            if (t.BaseType?.FullName == "java.lang.Enum")
                return (object[]) t.GetMethod("values")?.Invoke(null, new object[0]);

            return null;
        }

        [MethodPlug(typeof(Type), "cast", typeof(object))]
        public static object Cast(Type t, object o)
        {
            if (o == null || t.IsInstanceOfType(o)) return o;
            throw new InvalidCastException($"Can not cast instance of type {o.GetType().FullName} to {t.FullName}");
        }

        [MethodPlug(typeof(Type), "asSubclass", typeof(Type))]
        public static Type AsSubclass(Type t, Type other)
        {
            if (t.IsSubclassOf(other)) return t;
            throw new InvalidCastException($"Type {t.FullName} is not a subclass of {other.FullName}");
        }
    }

    [Flags]
    internal enum JavaModifiers
    {
        PUBLIC = 1,
        PRIVATE = 2,
        PROTECTED = 4,
        STATIC = 8,
        FINAL = 16,
        SYNCHRONIZED = 32,
        VOLATILE = 64,
        TRANSIENT = 128,
        NATIVE = 256,
        INTERFACE = 512,
        ABSTRACT = 1024,
        STRICT = 2048,
        BRIDGE = 64,
        VARARGS = 128,
        SYNTHETIC = 4096,
        ANNOTATION = 8192,
        ENUM = 16384,
        MANDATED = 32768,
        CLASS_MODIFIERS = 3103,
        INTERFACE_MODIFIERS = 3087,
        CONSTRUCTOR_MODIFIERS = 7,
        METHOD_MODIFIERS = 3391,
        FIELD_MODIFIERS = 223,
        PARAMETER_MODIFIERS = 16,
        ACCESS_MODIFIERS = 7,
    }

    internal static class JavaModifiersExtensions
    {
        public static bool HasFlagFast(this JavaModifiers value, JavaModifiers flag)
        {
            return (value & flag) != 0;
        }
    }
}
