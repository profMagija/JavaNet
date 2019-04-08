using System;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class FloatNative
    {
        public const string TypeName = "java.lang.Float";
        [NativeMethodImpl]
        public static int FloatToRawIntBits(Type flt, float f)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(f), 0);
        }
        [NativeMethodImpl]
        public static float IntBitsToFloat(Type flt, int f)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(f), 0);
        }
    }
}
