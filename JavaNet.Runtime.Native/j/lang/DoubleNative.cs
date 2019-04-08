using System;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class DoubleNative
    {
        public const string TypeName = "java.lang.Double";

        [NativeMethodImpl]
        public static long doubleToRawLongBits(Type dbl, double f)
        {
            return BitConverter.ToInt64(BitConverter.GetBytes(f), 0);
        }
        [NativeMethodImpl]
        public static double longBitsToDouble(Type dbl, long f)
        {
            return BitConverter.ToDouble(BitConverter.GetBytes(f), 0);
        }
    }
}