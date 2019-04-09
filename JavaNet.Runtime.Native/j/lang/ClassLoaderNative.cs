using System;
using java.lang;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class ClassLoaderNative
    {
        public const string TypeName = "java.lang.ClassLoader";

        [JniExport]
        public static void resolveClass0(ClassLoader @this, Type type)
        {

        }

        [JniExport]
        public static Class findBootstrapClass(ClassLoader @this, string name)
        {
            return (Class) ReflectionBridge.GetClass(Type.GetType(name + ", JavaNet.Runtime"));
        }

        [JniExport]
        public static Class findLoadedClass(ClassLoader @this, string name)
        {
            var t = Class.forName(name, false, @this);
            // TODO what if it's not loaded ??
            return t;
        }

        [JniExport]
        public static void registerNatives(Type classLoader)
        {

        }
    }
}
