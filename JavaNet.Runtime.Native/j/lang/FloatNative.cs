using System;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class FloatNative
    {
        public const string TypeName = "java.lang.Float";
        [JniExport]
        public static int floatToRawIntBits(Type flt, float f)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(f), 0);
        }
        [JniExport]
        public static float intBitsToFloat(Type flt, int f)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(f), 0);
        }
    }
}
