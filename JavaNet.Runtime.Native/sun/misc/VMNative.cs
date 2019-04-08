using System;
using java.lang;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.sun.misc
{
    public static class VMNative
    {
        public const string TypeName = "sun.misc.VM";

        [NativeMethodImpl]
        public static void Initialize(Type t)
        {

        }

        [NativeMethodImpl]
        public static ClassLoader LatestUserDefinedLoader(Type t)
        {
            return null;
        }


    }
}
