using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaLangClassLoader
    {
        public const string TypeName = "java.lang.ClassLoader";

        [MethodPlug("System.Boolean", TypeName, "loadLibrary0", "System.Type", "java.io.File", IsStatic = true)]
        public static bool loadLibrary0(Type callerClass, dynamic file)
        {
            // TODO implement
            return true;
        }

        [NativeImpl]
        public static void resolveClass0(object @this, Type type)
        {

        }

        [NativeImpl]
        public static Type findBootstrapClass(object @this, string name)
        {
            return Type.GetType(name + ", JavaNet.Runtime");
        }

        [NativeImpl]
        public static Type findLoadedClass(object @this, string name)
        {
            var t = ClassPlugs.ForName(name, false, @this);
            // TODO what if it's not loaded ??
            return t;
        }

        [NativeImpl(IsStatic = true)]
        public static void registerNatives()
        {

        }
    }
}
