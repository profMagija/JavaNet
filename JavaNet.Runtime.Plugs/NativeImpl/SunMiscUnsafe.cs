using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
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

        [NativeImpl(typeof(int), TypeName, "arrayBaseOffset", typeof(Type))]
        public static int ArrayBaseOffset(object @this, Type t)
        {
            return 0;
        }

        [NativeImpl(typeof(int), TypeName, "arrayIndexScale", typeof(Type))]
        public static int ArrayIndexScale(object @this, Type t)
        {
            var elementType = t.GetElementType();
            if (elementType == null)
                throw new ArgumentException("Not an array type", nameof(t));
            return elementType.IsValueType ? Marshal.SizeOf(elementType) : Marshal.SizeOf<IntPtr>();
        }

        [NativeImpl(typeof(int), TypeName, "addressSize")]
        public static int AddressSize(object @this)
        {
            return Marshal.SizeOf<IntPtr>();
        }

    }
}
