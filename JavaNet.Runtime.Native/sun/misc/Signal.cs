using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class SunMiscSignal
    {
        public const string TypeName = "sun.misc.Signal";

        [NativeImpl(IsStatic = true)]
        public static int findSignal(string name) => -1;
    }
}
