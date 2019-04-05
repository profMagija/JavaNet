using System;
using System.Collections.Generic;
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

        [NativeImpl(IsStatic = true)]
        public static void registerNatives()
        {

        }
    }
}
