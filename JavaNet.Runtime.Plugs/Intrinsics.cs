using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class Intrinsics
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Fcmpg(float a, float b)
        {
            return a < b ? -1 : a == b ? 0 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Fcmpl(float a, float b)
        {
            return a > b ? 1 : a == b ? 0 : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Lcmp(long a, long b)
        {
            return a > b ? 1 : a == b ? 0 : -1;
        }
    }
}
