using System;
using java.lang;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.sun.misc
{
    public static class VMNative
    {
        public const string TypeName = "sun.misc.VM";

        [JniExport]
        public static void initialize(Type vm)
        {

        }

        [JniExport]
        public static ClassLoader latestUserDefinedLoader(Type vm)
        {
            return null;
        }


    }
}
