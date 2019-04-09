using System;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class DoubleNative
    {
        public const string TypeName = "java.lang.Double";

        [JniExport]
        public static long doubleToRawLongBits(Type dbl, double f)
        {
            return BitConverter.ToInt64(BitConverter.GetBytes(f), 0);
        }
        [JniExport]
        public static double longBitsToDouble(Type dbl, long f)
        {
            return BitConverter.ToDouble(BitConverter.GetBytes(f), 0);
        }
    }
}