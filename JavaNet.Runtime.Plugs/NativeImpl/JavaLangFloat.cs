using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaLangFloat
    {
        [NativeImpl(typeof(int), "java.lang.Float", "floatToRawIntBits", typeof(float), IsStatic = true)]
        public static int FloatToRawIntBits(float f)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(f), 0);
        }
        [NativeImpl(typeof(float), "java.lang.Float", "intBitsToFloat", typeof(int), IsStatic = true)]
        public static float IntBitsToFloat(int f)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(f), 0);
        }
    }
}
