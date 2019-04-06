using System;
using System.Collections.Generic;
using System.Linq;
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

        [NativeImpl(IsStatic = true)]
        public static void registerNatives()
        {
        }

        [NativeImpl]
        public static int arrayBaseOffset(object @this, Type t)
        {
            return 0;
        }

        [NativeImpl]
        public static int arrayIndexScale(object @this, Type t)
        {
            var elementType = t.GetElementType();
            if (elementType == null)
                throw new ArgumentException("Not an array type", nameof(t));
            return elementType.IsValueType ? Marshal.SizeOf(elementType) : Marshal.SizeOf<IntPtr>();
        }

        [NativeImpl]
        public static int addressSize(object @this)
        {
            return Marshal.SizeOf<IntPtr>();
        }

        [NativeImpl, NativeImpl(MethodName = "staticFieldOffset")]
        public static long objectFieldOffset(object @this, FieldInfo fi)
        {
            return Marshal.OffsetOf(fi.DeclaringType, fi.Name).ToInt64();
        }

        [NativeImpl, NativeImpl(MethodName = "getIntVolatile")]
        public static int getInt(object @this, object ptr, long offset)
        {
            return Marshal.ReadInt32(ptr, (int)offset);
        }

        [NativeImpl, NativeImpl(MethodName = "putIntVolatile")]
        public static void putInt(object @this, object ptr, long offset, int value)
        {
            Marshal.WriteInt32(ptr, (int) offset, value);
        }

        [NativeImpl]
        public static int getInt(object @this, long ptr)
        {
            return Marshal.ReadInt32(new IntPtr(ptr));
        }

        [NativeImpl]
        public static void putInt(object @this, long ptr, int value)
        {
            Marshal.WriteInt32(new IntPtr(ptr), value);
        }

        [NativeImpl, NativeImpl(MethodName = "getObjectVolatile")]
        public static object getObject(object @this, object ptr, long offset)
        {
            if (ptr is Array arr)
            {
                return arr.GetValue(offset / arrayIndexScale(@this, ptr.GetType()));
            }
            
            return ptr.GetType().GetRuntimeFields()
                .First(f => Marshal.OffsetOf(ptr.GetType(), f.Name).ToInt64() == offset)
                .GetValue(ptr);
        }

        [NativeImpl, NativeImpl(MethodName = "putObjectVolatile")]
        public static void putObject(object @this, object ptr, long offset, object value)
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

        [NativeImpl, NativeImpl(MethodName = "getBooleanVolatile")]
        public static bool getBoolean(object @this, object ptr, long offset)
        {
            return getByte(@this, ptr, offset) != 0;
        }

        [NativeImpl, NativeImpl(MethodName = "putBooleanVolatile")]
        public static void putBoolean(object @this, object ptr, long offset, bool value)
        {
            putByte(@this, ptr, offset, (sbyte) (value ? 1 : 0));
        }

        [NativeImpl, NativeImpl(MethodName = "getLongVolatile")]
        public static long getLong(object @this, object ptr, long offset)
        {
            return Marshal.ReadInt64(ptr, (int)offset);
        }

        [NativeImpl, NativeImpl(MethodName = "putLongVolatile")]
        public static void putLong(object @this, object ptr, long offset, long value)
        {
            Marshal.WriteInt64(ptr, (int)offset, value);
        }

        [NativeImpl]
        public static long getLong(object @this, long ptr)
        {
            return Marshal.ReadInt64(new IntPtr(ptr));
        }

        [NativeImpl]
        public static void putLong(object @this, long ptr, long value)
        {
            Marshal.WriteInt64(new IntPtr(ptr), value);
        }

        [NativeImpl, NativeImpl(MethodName = "getByteVolatile")]
        public static sbyte getByte(object @this, object ptr, long offset)
        {
            return (sbyte)Marshal.ReadByte(ptr, (int)offset);
        }

        [NativeImpl, NativeImpl(MethodName = "putByteVolatile")]
        public static void putByte(object @this, object ptr, long offset, sbyte value)
        {
            Marshal.WriteByte(ptr, (int) offset, (byte) value);
        }

        [NativeImpl]
        public static sbyte getByte(object @this, long ptr)
        {
            return (sbyte)Marshal.ReadByte(new IntPtr(ptr));
        }

        [NativeImpl]
        public static void putByte(object @this, long ptr, sbyte value)
        {
            Marshal.WriteByte(new IntPtr(ptr), (byte) value);
        }

        [NativeImpl]
        public static bool compareAndSwapInt(object @this, object ptr, long offset, int value, int original)
        {
            lock (ptr)
            {
                var read = getInt(@this, ptr, offset);
                putInt(@this, ptr, offset, value);
                return read != original;
            }
        }

        [NativeImpl]
        public static bool compareAndSwapObject(object @this, object ptr, long offset, object value, object original)
        {
            lock (ptr)
            {
                var read = getObject(@this, ptr, offset);
                putObject(@this, ptr, offset, value);
                return read != original;
            }
        }

        [NativeImpl]
        public static bool compareAndSwapLong(object @this, object ptr, long offset, long value, long original)
        {
            lock (ptr)
            {
                var read = getLong(@this, ptr, offset);
                putLong(@this, ptr, offset, value);
                return read != original;
            }
        }

        [NativeImpl]
        public static long allocateMemory(object @this, long size)
        {
            return Marshal.AllocHGlobal(new IntPtr(size)).ToInt64();
        }

        [NativeImpl]
        public static void freeMemory(object @this, long ptr)
        {
            Marshal.FreeHGlobal(new IntPtr(ptr));
        }

        [NativeImpl]
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
