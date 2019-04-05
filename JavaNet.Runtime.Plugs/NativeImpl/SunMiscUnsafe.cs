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

        [NativeImpl(typeof(int), TypeName, "getInt", typeof(long))]
        public static int getInt(object @this, long ptr)
        {
            return Marshal.ReadInt32(new IntPtr(ptr));
        }

        [NativeImpl(typeof(void), TypeName, "putInt", typeof(long), typeof(int))]
        public static void putInt(object @this, long ptr, int value)
        {
            Marshal.WriteInt32(new IntPtr(ptr), value);
        }

        [NativeImpl(typeof(long), TypeName, "getLongVolatile", typeof(object), typeof(long))]
        [NativeImpl(typeof(long), TypeName, "getLong", typeof(object), typeof(long))]
        public static long getLong(object @this, object ptr, long offset)
        {
            return Marshal.ReadInt64(ptr, (int)offset);
        }

        [NativeImpl(typeof(void), TypeName, "putLongVolatile", typeof(object), typeof(long), typeof(long))]
        [NativeImpl(typeof(void), TypeName, "putLong", typeof(object), typeof(long), typeof(long))]
        public static void putLong(object @this, object ptr, long offset, long value)
        {
            Marshal.WriteInt64(ptr, (int)offset, value);
        }

        [NativeImpl(typeof(long), TypeName, "getLong", typeof(long))]
        public static long getLong(object @this, long ptr)
        {
            return Marshal.ReadInt64(new IntPtr(ptr));
        }

        [NativeImpl(typeof(void), TypeName, "putLong", typeof(long), typeof(long))]
        public static void putLong(object @this, long ptr, long value)
        {
            Marshal.WriteInt64(new IntPtr(ptr), value);
        }

        [NativeImpl(typeof(sbyte), TypeName, "getByteVolatile", typeof(object), typeof(long))]
        [NativeImpl(typeof(sbyte), TypeName, "getByte", typeof(object), typeof(long))]
        public static long getByte(object @this, object ptr, long offset)
        {
            return (sbyte)Marshal.ReadByte(ptr, (int)offset);
        }

        [NativeImpl(typeof(void), TypeName, "putByteVolatile", typeof(object), typeof(long), typeof(sbyte))]
        [NativeImpl(typeof(void), TypeName, "putByte", typeof(object), typeof(long), typeof(sbyte))]
        public static void putByte(object @this, object ptr, long offset, sbyte value)
        {
            Marshal.WriteByte(ptr, (int) offset, (byte) value);
        }

        [NativeImpl(typeof(sbyte), TypeName, "getByte", typeof(long))]
        public static sbyte getByte(object @this, long ptr)
        {
            return (sbyte)Marshal.ReadByte(new IntPtr(ptr));
        }

        [NativeImpl(typeof(void), TypeName, "putByte", typeof(long), typeof(sbyte))]
        public static void putByte(object @this, long ptr, sbyte value)
        {
            Marshal.WriteByte(new IntPtr(ptr), (byte) value);
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

        [NativeImpl(typeof(long), TypeName, "allocateMemory", typeof(long))]
        public static long allocateMemory(object @this, long size)
        {
            return Marshal.AllocHGlobal(new IntPtr(size)).ToInt64();
        }

        [NativeImpl(typeof(void), TypeName, "freeMemory", typeof(long))]
        public static void freeMemory(object @this, long ptr)
        {
            Marshal.FreeHGlobal(new IntPtr(ptr));
        }

        [NativeImpl(typeof(long), TypeName, "reallocateMemory", typeof(long), typeof(long))]
        public static long reallocateMemory(object @this, long ptr, long newSize)
        {
            return Marshal.ReAllocHGlobal(new IntPtr(ptr), new IntPtr(newSize)).ToInt64();
        }

        [NativeImpl]
        public static void ensureClassInitialized(object @this, Type type)
        {
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }
}
