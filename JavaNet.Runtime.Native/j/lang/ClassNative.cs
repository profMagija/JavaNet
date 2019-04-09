using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using java.lang;
using java.lang.reflect;
using JavaNet.Runtime.Plugs;
using Type = System.Type;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class ClassNative
    {
        public const string TypeName = "java.lang.Class";

        private static MethodRef<Func<string, bool, Class>> _loadClass;
        private static FieldRef<Type> _nativeData;

        [JniExport]
        public static void registerNatives(Type clazz)
        {
            _loadClass = new MethodRef<Func<string, bool, Class>>(typeof(ClassLoader), "loadClass");
            _nativeData = new FieldRef<Type>(typeof(Class), "__nativeData");
        }

        [JniExport]
        public static Class forName0(Type clazz, string classname, bool initialize, ClassLoader loader, Class caller)
        {
            Class found;

            if (loader != null)
            {
                found = _loadClass[loader](classname, initialize);
            }
            else
            {
                if (classname == null) throw new NullReferenceException(nameof(classname));

                if (classname.StartsWith("["))
                {
                    classname = classname.TrimStart('[');
                    initialize = false;
                }

                if (classname.StartsWith("L") && classname.EndsWith(";"))
                    classname = classname.Substring(1, classname.Length - 2);

                Type t = null;

                foreach (var type in typeof(Class).Assembly.GetTypes())
                {
                    if (type.GetCustomAttribute<JavaNameAttribute>()?.Name == classname)
                    {
                        t = type;
                    }
                }

                if (t == null)
                    throw new ClassNotFoundException(classname);

                if (initialize)
                    RuntimeHelpers.RunClassConstructor(t.TypeHandle);

                found = (Class) ReflectionBridge.GetClass(t);
            }

            return found;
        }

        [JniExport]
        public static Class getPrimitiveClass(Type clazz, string name)
        {
            switch (name)
            {
                case "void": return (Class) ReflectionBridge.GetClass(typeof(void));
                case "byte": return (Class) ReflectionBridge.GetClass(typeof(sbyte));
                case "boolean": return (Class) ReflectionBridge.GetClass(typeof(bool));
                case "char": return (Class) ReflectionBridge.GetClass(typeof(char));
                case "short": return (Class) ReflectionBridge.GetClass(typeof(short));
                case "int": return (Class) ReflectionBridge.GetClass(typeof(int));
                case "float": return (Class) ReflectionBridge.GetClass(typeof(float));
                case "long": return (Class) ReflectionBridge.GetClass(typeof(long));
                case "double": return (Class) ReflectionBridge.GetClass(typeof(double));
                default: return null;
            }
        }

        [JniExport]
        public static bool desiredAssertionStatus0(Type clazz, Class target)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        [JniExport]
        public static Field[] getDeclaredFields0(Class @this, bool publicOnly)
        {
            var flags = publicOnly ? BindingFlags.Public : BindingFlags.Public | BindingFlags.NonPublic;
            flags |= BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static;
            var fields = _nativeData[@this].GetFields(flags);

            return fields.Select(CreateField).ToArray();
        }

        [JniExport]
        public static Method[] getDeclaredMethods(Class @this, bool publicOnly)
        {
            var flags = publicOnly ? BindingFlags.Public : BindingFlags.Public | BindingFlags.NonPublic;
            flags |= BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static;
            var methods = _nativeData[@this].GetMethods(flags);

            return methods
                .Select(CreateMethod)
                .ToArray();
        }

        [JniExport]
        public static int getModifiers(Class @this)
        {
            var t = _nativeData[@this];

            var rv = 0;

            if (t.IsPublic)
                rv |= Modifier.PUBLIC;

            if (t.IsInterface)
                rv |= Modifier.INTERFACE;

            if (t.IsAbstract)
                rv |= Modifier.ABSTRACT;

            if (t.IsEnum || t.BaseType.FullName == "java.lang.Enum")
                rv |= 16384; // enum

            if (t.IsSealed)
                rv |= Modifier.FINAL;

            return rv;
        }

        [JniExport]
        public static string getName0(Class @this)
        {
            var t = _nativeData[@this];
            return t.FullName;
        }

        internal static Field CreateField(FieldInfo f) =>
            new Field(
                (Class) ReflectionBridge.GetClass(f.DeclaringType),
                string.Intern(f.Name),
                (Class) ReflectionBridge.GetClass(f.FieldType),
                0,
                0,
                string.Empty,
                new sbyte[0]) {__nativeData = f};

        internal static FieldInfo GetField(Field f)
        {
            if (f.__nativeData is FieldInfo fi)
                return fi;
            var c = GetClass(f.getDeclaringClass());
            fi = c.GetField(f.getName(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            f.__nativeData = fi;
            return fi;
        }

        internal static Type GetClass(Class clazz)
        {
            return (Type) clazz.__nativeData ?? throw new NotImplementedException("hmm indeed");
        }

        internal static Method CreateMethod(MethodInfo m) => new Method(
            (Class) ReflectionBridge.GetClass(m.DeclaringType),
            string.Intern(m.Name),
            m.GetParameters().Select(p => (Class) ReflectionBridge.GetClass(p.ParameterType)).ToArray(),
            (Class) ReflectionBridge.GetClass(m.ReturnType),
            new Class[0],
            0,
            0,
            "",
            new sbyte[0], new sbyte[0], new sbyte[0]
        ) {__nativeData = m};
    }
}
