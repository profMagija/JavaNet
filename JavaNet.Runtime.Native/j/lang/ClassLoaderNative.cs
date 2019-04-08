using System;
using java.lang;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class ClassLoaderNative
    {
        public const string TypeName = "java.lang.ClassLoader";

        [NativeMethodImpl]
        public static void resolveClass0(ClassLoader @this, Type type)
        {

        }

        [NativeMethodImpl]
        public static Class findBootstrapClass(ClassLoader @this, java.lang.String name)
        {
            return (Class) ReflectionBridge.GetClass(Type.GetType(name + ", JavaNet.Runtime"));
        }

        [NativeMethodImpl]
        public static Class findLoadedClass(ClassLoader @this, java.lang.String name)
        {
            var t = Class.forName(name, false, @this);
            // TODO what if it's not loaded ??
            return t;
        }

        [NativeMethodImpl]
        public static void registerNatives(Type classLoader)
        {

        }
    }
}
