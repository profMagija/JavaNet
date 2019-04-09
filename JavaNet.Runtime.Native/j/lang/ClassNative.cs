using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using java.lang;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class ClassNative
    {
        public const string TypeName = "java.lang.Class";

        private static MethodRef<Func<string, bool, Class>> _loadClass;

        [JniExport]
        public static void registerNatives(Type clazz)
        {
            _loadClass = new MethodRef<Func<string, bool, Class>>(typeof(ClassLoader), "loadClass");
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
    }
}
