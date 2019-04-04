using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

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

        [NativeImpl(typeof(long), TypeName, "objectFieldOffset", typeof(FieldInfo))]
        [NativeImpl(typeof(long), TypeName, "staticFieldOffset", typeof(FieldInfo))]
        public static long objectFieldOffset(object @this, FieldInfo fi)
        {
            return Marshal.OffsetOf(fi.DeclaringType, fi.Name).ToInt64();
        }

        [NativeImpl(typeof(int), TypeName, "getIntVolatile", typeof(object), typeof(long))]
        [NativeImpl(typeof(int), TypeName, "getInt", typeof(object), typeof(long))]
        public static int getInt(object @this, object ptr, long offset)
        {
            return Marshal.ReadInt32(ptr, (int)offset);
        }

        [NativeImpl(typeof(void), TypeName, "putIntVolatile", typeof(object), typeof(long), typeof(int))]
        [NativeImpl(typeof(void), TypeName, "putInt", typeof(object), typeof(long), typeof(int))]
        public static void putInt(object @this, object ptr, long offset, int value)
        {
            Marshal.WriteInt32(ptr, (int) offset, value);
        }

        [NativeImpl(typeof(bool), TypeName, "compareAndSwapInt", typeof(object), typeof(long), typeof(int), typeof(int))]
        public static bool compareAndSwapInt(object @this, object ptr, long offset, int value, int original)
        {
            lock (ptr)
            {
                var read = getInt(@this, ptr, offset);
                putInt(@this, ptr, offset, value);
                return read != original;
            }
        }

    }
}
