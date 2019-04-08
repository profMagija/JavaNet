using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class SunMiscVm
    {
        public const string TypeName = "sun.misc.VM";

        [NativeImpl(typeof(void), TypeName, "initialize", IsStatic = true)]
        public static void Initialize()
        {

        }

        [NativeImpl("java.lang.ClassLoader", TypeName, "latestUserDefinedLoader", IsStatic = true)]
        [return: ActualType("java.lang.ClassLoader")]
        public static object LatestUserDefinedLoader()
        {
            return null;
        }


    }
}
