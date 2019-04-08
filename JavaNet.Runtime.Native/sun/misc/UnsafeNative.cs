using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JavaNet.Runtime.Plugs;
using sun.misc;

namespace JavaNet.Runtime.Native.sun.misc
{
    public static class UnsafeNative
    {
        public const string TypeName = "sun.misc.Unsafe";

        [NativeMethodImpl]
        public static void registerNatives(Type t)
        {
        }

        [NativeMethodImpl]
        public static int arrayBaseOffset(Unsafe @this, Type t)
        {
            return 0;
        }

        [NativeMethodImpl]
        public static int arrayIndexScale(Unsafe @this, Type t)
        {
            var elementType = t.GetElementType();
            if (elementType == null)
                throw new ArgumentException("Not an array type", nameof(t));
            return elementType.IsValueType ? Marshal.SizeOf(elementType) : Marshal.SizeOf<IntPtr>();
        }

        [NativeMethodImpl]
        public static int addressSize(Unsafe @this)
        {
            return Marshal.SizeOf<IntPtr>();
        }

        [NativeMethodImpl]
        public static long objectFieldOffset(Unsafe @this, FieldInfo fi)
        {
            return Marshal.OffsetOf(fi.DeclaringType, fi.Name).ToInt64();
        }

        [NativeMethodImpl]
        public static long staticFieldOffset(Unsafe @this, FieldInfo fi)
        {
            return Marshal.OffsetOf(fi.DeclaringType, fi.Name).ToInt64();
        }


        [NativeMethodImpl]
        public static int getInt(Unsafe @this, object ptr, long offset)
        {
            return Marshal.ReadInt32(ptr, (int)offset);
        }

        [NativeMethodImpl]
        public static int getIntVolatile(Unsafe @this, object ptr, long offset)
        {
            return Marshal.ReadInt32(ptr, (int)offset);
        }

        [NativeMethodImpl]
        public static void putInt(Unsafe @this, object ptr, long offset, int value)
        {
            Marshal.WriteInt32(ptr, (int)offset, value);
        }

        [NativeMethodImpl]
        public static void putIntVolatile(Unsafe @this, object ptr, long offset, int value)
        {
            putInt(@this, ptr, offset, value);
        }

        [NativeMethodImpl]
        public static int getInt(Unsafe @this, long ptr)
        {
            return Marshal.ReadInt32(new IntPtr(ptr));
        }

        [NativeMethodImpl]
        public static void putInt(Unsafe @this, long ptr, int value)
        {
            Marshal.WriteInt32(new IntPtr(ptr), value);
        }

        [NativeMethodImpl]
        public static object getObject(Unsafe @this, object ptr, long offset)
        {
            if (ptr is Array arr)
            {
                return arr.GetValue(offset / arrayIndexScale(@this, ptr.GetType()));
            }

            return ptr.GetType().GetRuntimeFields()
                .First(f => Marshal.OffsetOf(ptr.GetType(), f.Name).ToInt64() == offset)
                .GetValue(ptr);
        }

        [NativeMethodImpl]
        public static object getObjectVolatile(Unsafe @this, object ptr, long offset)
        {
            return getObject(@this, ptr, offset);
        }

        [NativeMethodImpl]
        public static void putObject(Unsafe @this, object ptr, long offset, object value)
        {
            if (ptr is Array arr)
            {
                arr.SetValue(value, offset / arrayIndexScale(@this, ptr.GetType()));
            }
            else
            {
                ptr.GetType().GetRuntimeFields()
                    .First(f => Marshal.OffsetOf(ptr.GetType(), f.Name).ToInt64() == offset)
                    .SetValue(ptr, value);
            }
        }

        [NativeMethodImpl]
        public static void putObjectVolatile(Unsafe @this, object ptr, long offset, object value)
        {
            putObject(@this, ptr, offset, value);
        }

        [NativeMethodImpl]
        public static bool getBoolean(Unsafe @this, object ptr, long offset)
        {
            return getByte(@this, ptr, offset) != 0;
        }

        [NativeMethodImpl]
        public static bool getBooleanVolatile(Unsafe @this, object ptr, long offset)
        {
            return getBoolean(@this, ptr, offset);
        }

        [NativeMethodImpl]
        public static void putBoolean(Unsafe @this, object ptr, long offset, bool value)
        {
            putByte(@this, ptr, offset, (sbyte)(value ? 1 : 0));
        }

        [NativeMethodImpl]
        public static void putBooleanVolatile(Unsafe @this, object ptr, long offset, bool value)
        {
            putBoolean(@this, ptr, offset, value);
        }

        [NativeMethodImpl]
        public static long getLong(Unsafe @this, object ptr, long offset)
        {
            return Marshal.ReadInt64(ptr, (int)offset);
        }

        [NativeMethodImpl]
        public static void putLong(Unsafe @this, object ptr, long offset, long value)
        {
            Marshal.WriteInt64(ptr, (int)offset, value);
        }

        [NativeMethodImpl]
        public static long getLong(Unsafe @this, long ptr)
        {
            return Marshal.ReadInt64(new IntPtr(ptr));
        }

        [NativeMethodImpl]
        public static void putLong(Unsafe @this, long ptr, long value)
        {
            Marshal.WriteInt64(new IntPtr(ptr), value);
        }

        [NativeMethodImpl]
        public static sbyte getByte(Unsafe @this, object ptr, long offset)
        {
            return (sbyte)Marshal.ReadByte(ptr, (int)offset);
        }

        [NativeMethodImpl]
        public static void putByte(Unsafe @this, object ptr, long offset, sbyte value)
        {
            Marshal.WriteByte(ptr, (int) offset, (byte) value);
        }

        [NativeMethodImpl]
        public static sbyte getByte(Unsafe @this, long ptr)
        {
            return (sbyte)Marshal.ReadByte(new IntPtr(ptr));
        }

        [NativeMethodImpl]
        public static void putByte(Unsafe @this, long ptr, sbyte value)
        {
            Marshal.WriteByte(new IntPtr(ptr), (byte) value);
        }

        [NativeMethodImpl]
        public static bool compareAndSwapInt(Unsafe @this, object ptr, long offset, int value, int original)
        {
            lock (ptr)
            {
                var read = getInt(@this, ptr, offset);
                putInt(@this, ptr, offset, value);
                return read != original;
            }
        }

        [NativeMethodImpl]
        public static bool compareAndSwapObject(Unsafe @this, object ptr, long offset, object value, object original)
        {
            lock (ptr)
            {
                var read = getObject(@this, ptr, offset);
                putObject(@this, ptr, offset, value);
                return read != original;
            }
        }

        [NativeMethodImpl]
        public static bool compareAndSwapLong(Unsafe @this, object ptr, long offset, long value, long original)
        {
            lock (ptr)
            {
                var read = getLong(@this, ptr, offset);
                putLong(@this, ptr, offset, value);
                return read != original;
            }
        }

        [NativeMethodImpl]
        public static long allocateMemory(Unsafe @this, long size)
        {
            return Marshal.AllocHGlobal(new IntPtr(size)).ToInt64();
        }

        [NativeMethodImpl]
        public static void freeMemory(Unsafe @this, long ptr)
        {
            Marshal.FreeHGlobal(new IntPtr(ptr));
        }

        [NativeMethodImpl]
        public static long reallocateMemory(Unsafe @this, long ptr, long newSize)
        {
            return Marshal.ReAllocHGlobal(new IntPtr(ptr), new IntPtr(newSize)).ToInt64();
        }

        [NativeMethodImpl]
        public static void ensureClassInitialized(Unsafe @this, Type type)
        {
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }
}
