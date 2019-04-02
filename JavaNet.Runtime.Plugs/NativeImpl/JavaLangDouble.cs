using System;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaLangDouble
    {
        [NativeImpl(typeof(long), "java.lang.Double", "doubleToRawLongBits", typeof(double), IsStatic = true)]
        public static long DoubleToRawLongBits(double f)
        {
            return BitConverter.ToInt64(BitConverter.GetBytes(f), 0);
        }
        [NativeImpl(typeof(double), "java.lang.Double", "longBitsToDouble", typeof(long), IsStatic = true)]
        public static double LongBitsToDouble(long f)
        {
            return BitConverter.ToDouble(BitConverter.GetBytes(f), 0);
        }
    }
}