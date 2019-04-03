using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class SunMiscUnsafe
    {
        public const string TypeName = "sun.misc.Unsafe";

        [NativeImpl(typeof(void), TypeName, "registerNatives", IsStatic = true)]
        public static void RegisterNatives()
        {
        }
    }
}
