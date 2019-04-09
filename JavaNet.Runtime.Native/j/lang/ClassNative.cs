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

            JNI.RegisterNativeMethod(clazz, nameof(forName0), (Func<Type, string, bool, ClassLoader, Class, Class>) forName0);
            JNI.RegisterNativeMethod(clazz, nameof(getPrimitiveClass), (Func<Type, string, Class>) getPrimitiveClass);
            JNI.RegisterNativeMethod(clazz, nameof(desiredAssertionStatus0), (Func<Type, Class, bool>) desiredAssertionStatus0);
            JNI.RegisterNativeMethod(clazz, nameof(getDeclaredFields0), (Func<Class, bool, Field[]>) getDeclaredFields0);
            JNI.RegisterNativeMethod(clazz, nameof(getDeclaredMethods0), (Func<Class, bool, Method[]>) getDeclaredMethods0);
            JNI.RegisterNativeMethod(clazz, nameof(getDeclaredConstructors0), (Func<Class, bool, Constructor[]>) getDeclaredConstructors0);
            JNI.RegisterNativeMethod(clazz, nameof(getModifiers), (Func<Class, int>) getModifiers);
            JNI.RegisterNativeMethod(clazz, nameof(getName0), (Func<Class, string>) getName0);
            JNI.RegisterNativeMethod(clazz, nameof(getSuperclass), (Func<Class, Class>) getSuperclass);
            JNI.RegisterNativeMethod(clazz, nameof(isPrimitive), (Func<Class, bool>) isPrimitive);
            JNI.RegisterNativeMethod(clazz, nameof(isInterface), (Func<Class, bool>) isInterface);
            JNI.RegisterNativeMethod(clazz, nameof(isArray), (Func<Class, bool>) isArray);
            JNI.RegisterNativeMethod(clazz, nameof(isInstance), (Func<Class, object, bool>) isInstance);

        }

        [JniExport]
        public static Class forName0(Type clazz, string classname, bool initialize, ClassLoader loader, Class caller)
        {
            Class found;

            Console.WriteLine(" searching for {0}", classname);

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
                    if (type.FullName == classname)
                        t = type;
                    if (type.GetCustomAttribute<JavaNameAttribute>()?.Name.Replace('/', '.') == classname)
                        t = type;
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
        public static Method[] getDeclaredMethods0(Class @this, bool publicOnly)
        {
            var flags = publicOnly ? BindingFlags.Public : BindingFlags.Public | BindingFlags.NonPublic;
            flags |= BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static;
            var methods = _nativeData[@this].GetMethods(flags);

            return methods
                .Select(CreateMethod)
                .ToArray();
        }

        [JniExport]
        public static Constructor[] getDeclaredConstructors0(Class @this, bool publicOnly)
        {
            var flags = publicOnly ? BindingFlags.Public : BindingFlags.Public | BindingFlags.NonPublic;
            flags |= BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static;
            var methods = _nativeData[@this].GetConstructors(flags);

            return methods
                .Select(CreateConstructor)
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

        [JniExport]
        public static Class getSuperclass(Class @this)
        {
            var bt = _nativeData[@this].BaseType;
            if (bt == null)
                return null;
            return (Class) ReflectionBridge.GetClass(bt);
        }

        [JniExport]
        public static bool isPrimitive(Class @this)
        {
            return _nativeData[@this].IsPrimitive;
        }

        [JniExport]
        public static bool isInterface(Class @this)
        {
            return _nativeData[@this].IsInterface;
        }

        [JniExport]
        public static bool isArray(Class @this)
        {
            return _nativeData[@this].IsArray;
        }

        [JniExport]
        public static bool isInstance(Class @this, object o)
        {
            return _nativeData[@this].IsInstanceOfType(o);
        }



        internal static Field CreateField(FieldInfo f)
        {
            var modifiers = 0;
            if (f.IsStatic)
                modifiers |= Modifier.STATIC;

            if (f.IsPublic)
                modifiers |= Modifier.PUBLIC;
            else if (f.IsPrivate)
                modifiers |= Modifier.PRIVATE;
            else if (f.IsFamilyOrAssembly)
                modifiers |= Modifier.PROTECTED;

            if (f.GetRequiredCustomModifiers().Any(t => t == typeof(IsVolatile)))
                modifiers |= Modifier.VOLATILE;

            return new Field(
                (Class) ReflectionBridge.GetClass(f.DeclaringType),
                string.Intern(f.Name),
                (Class) ReflectionBridge.GetClass(f.FieldType),
                modifiers,
                0,
                string.Empty,
                new sbyte[0]) {__nativeData = f};
        }

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

        internal static Method CreateMethod(MethodInfo m)
        {
            var modifiers = 0;

            if (m.IsPublic)
                modifiers |= Modifier.PUBLIC;
            else if (m.IsPrivate)
                modifiers |= Modifier.PRIVATE;
            else if (m.IsFamilyOrAssembly)
                modifiers |= Modifier.PROTECTED;

            if (!m.IsVirtual)
                modifiers |= Modifier.FINAL;

            if (m.IsStatic)
                modifiers |= Modifier.STATIC;

            return new Method(
                (Class) ReflectionBridge.GetClass(m.DeclaringType),
                string.Intern(m.Name),
                m.GetParameters().Select(p => (Class) ReflectionBridge.GetClass(p.ParameterType)).ToArray(),
                (Class) ReflectionBridge.GetClass(m.ReturnType),
                new Class[0],
                modifiers,
                0,
                "",
                new sbyte[0], new sbyte[0], new sbyte[0]
            ) {__nativeData = m};
        }

        internal static Constructor CreateConstructor(ConstructorInfo m)
        {
            var modifiers = 0;

            if (m.IsPublic)
                modifiers |= Modifier.PUBLIC;
            else if (m.IsPrivate)
                modifiers |= Modifier.PRIVATE;
            else if (m.IsFamilyOrAssembly)
                modifiers |= Modifier.PROTECTED;

            if (!m.IsVirtual)
                modifiers |= Modifier.FINAL;

            if (m.IsStatic)
                modifiers |= Modifier.STATIC;

            return new Constructor(
                    (Class) ReflectionBridge.GetClass(m.DeclaringType),
                    m.GetParameters().Select(p => (Class) ReflectionBridge.GetClass(p.ParameterType)).ToArray(),
                    new Class[0],
                    modifiers,
                    0,
                    "",
                    new sbyte[0], new sbyte[0]
                )
                {__nativeData = m};
        }
    }
}
