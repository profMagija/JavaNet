using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using java.lang;
using java.lang.reflect;
using JavaNet.Runtime.Native.j.lang;
using JavaNet.Runtime.Plugs;
using sun.misc;
using Array = System.Array;
using Type = System.Type;

namespace JavaNet.Runtime.Native.sun.misc
{
    public static class UnsafeNative
    {
        public const string TypeName = "sun.misc.Unsafe";

        [JniExport]
        public static void registerNatives(Type t)
        {
        }

        [JniExport]
        public static int arrayBaseOffset(Unsafe @this, Class t)
        {
            return 0;
        }

        [JniExport]
        public static int arrayIndexScale(Unsafe @this, Class t)
        {
            return ArrayScale(t.GetField<Type>("__nativeData"));
        }

        private static int ArrayScale(Type t)
        {
            var elementType = t.GetElementType();
            if (elementType == null)
                throw new ArgumentException("Not an array type", nameof(t));
            return elementType.IsValueType ? Marshal.SizeOf(elementType) : IntPtr.Size;
        }

        [JniExport]
        public static int addressSize(Unsafe @this)
        {
            return IntPtr.Size;
        }

        [JniExport]
        public static unsafe long objectFieldOffset(Unsafe @this, Field fi)
        {
            var o = FormatterServices.GetUninitializedObject(ClassNative.GetClass(fi.getDeclaringClass()));
            var p0 = index(o, 0);
            var p1 = (byte*) TypedReference.MakeTypedReference(o, new[] {ClassNative.GetField(fi)}).ToPointer();
            return (int)(p1 - p0);
        }

        [JniExport]
        public static unsafe long staticFieldOffset(Unsafe @this, Field fi)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Structure with layout equal to <see cref="TypedReference"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct TypedRef : IEquatable<TypedRef>
        {
            public readonly IntPtr Value;
            public readonly IntPtr Type;

            public TypedRef(IntPtr value, IntPtr type)
            {
                Value = value;
                Type = type;
            }

            public override bool Equals(object obj)
            {
                return (obj is TypedRef tr) && Equals(tr);
            }

            public bool Equals(TypedRef other)
            {
                return this.Value == other.Value && this.Type == other.Type;
            }

            public override int GetHashCode()
            {
                int hashCode = 0;
                unchecked
                {
                    hashCode += 1000000007 * Value.GetHashCode();
                    hashCode += 1000000009 * Type.GetHashCode();
                }
                return hashCode;
            }

            public static bool operator ==(TypedRef tr1, TypedRef tr2)
            {
                return tr1.Equals(tr2);
            }

            public static bool operator !=(TypedRef tr1, TypedRef tr2)
            {
                return !(tr1 == tr2);
            }
        }

        public static unsafe IntPtr ToPointer(this TypedReference target)
        {
            return ((TypedRef*)(&target))->Value;
        }

        public static unsafe byte* index(object ptr, long offset) => (byte*)*(IntPtr*)__makeref(ptr).ToPointer().ToPointer() + offset;

        [JniExport(TypeName, "getIntLjava/lang/Object;J")]
        public static unsafe int getInt(Unsafe @this, object ptr, long offset)
        {
            return *(int*) index(ptr, offset);
        }

        [JniExport]
        public static int getIntVolatile(Unsafe @this, object ptr, long offset)
        {
            return getInt(@this, ptr, offset);
        }

        [JniExport(TypeName, "putIntLjava/lang/Object;JI")]
        public static unsafe void putInt(Unsafe @this, object ptr, long offset, int value)
        {
            *(int*) index(ptr, offset) = value;
        }

        [JniExport]
        public static void putIntVolatile(Unsafe @this, object ptr, long offset, int value)
        {
            putInt(@this, ptr, offset, value);
        }

        [JniExport(TypeName, "getIntJ")]
        public static int getInt(Unsafe @this, long ptr)
        {
            return Marshal.ReadInt32(new IntPtr(ptr));
        }

        [JniExport(TypeName, "putIntJI")]
        public static void putInt(Unsafe @this, long ptr, int value)
        {
            Marshal.WriteInt32(new IntPtr(ptr), value);
        }

        [JniExport]
        public static unsafe object getObject(Unsafe @this, object ptr, long offset)
        {
            if (ptr is Array arr)
            {
                return arr.GetValue(offset / ArrayScale(ptr.GetType()));
            }

            var tr = new TypedRef(new IntPtr(index(ptr, offset)), typeof(object).TypeHandle.Value);
            var tr2 = *((TypedReference*) &tr);
            return __refvalue(tr2, object);
        }

        [JniExport]
        public static object getObjectVolatile(Unsafe @this, object ptr, long offset)
        {
            return getObject(@this, ptr, offset);
        }

        [JniExport]
        public static unsafe void putObject(Unsafe @this, object ptr, long offset, object value)
        {
            if (ptr is Array arr)
            {
                arr.SetValue(value, offset / ArrayScale(ptr.GetType()));
            }
            else
            {
                var tr = new TypedRef(new IntPtr(index(ptr, offset)), value?.GetType().TypeHandle.Value ?? typeof(object).TypeHandle.Value);
                var tr2 = *((TypedReference*)&tr);
                __refvalue(tr2, object) = value;
            }
        }

        [JniExport]
        public static void putObjectVolatile(Unsafe @this, object ptr, long offset, object value)
        {
            putObject(@this, ptr, offset, value);
        }

        [JniExport]
        public static bool getBoolean(Unsafe @this, object ptr, long offset)
        {
            return getByte(@this, ptr, offset) != 0;
        }

        [JniExport]
        public static bool getBooleanVolatile(Unsafe @this, object ptr, long offset)
        {
            return getBoolean(@this, ptr, offset);
        }

        [JniExport]
        public static void putBoolean(Unsafe @this, object ptr, long offset, bool value)
        {
            putByte(@this, ptr, offset, (sbyte)(value ? 1 : 0));
        }

        [JniExport]
        public static void putBooleanVolatile(Unsafe @this, object ptr, long offset, bool value)
        {
            putBoolean(@this, ptr, offset, value);
        }

        [JniExport(TypeName, "getLongLjava/lang/Object;J")]
        public static unsafe long getLong(Unsafe @this, object ptr, long offset)
        {
            return *(long*) index(ptr, offset);
        }

        [JniExport(TypeName, "putLongLjava/lang/Object;JJ")]
        public static unsafe void putLong(Unsafe @this, object ptr, long offset, long value)
        {
            *(long*) index(ptr, offset) = value;
        }

        [JniExport(TypeName, "getLongJ")]
        public static long getLong(Unsafe @this, long ptr)
        {
            return Marshal.ReadInt64(new IntPtr(ptr));
        }

        [JniExport(TypeName, "putLongJJ")]
        public static void putLong(Unsafe @this, long ptr, long value)
        {
            Marshal.WriteInt64(new IntPtr(ptr), value);
        }

        [JniExport(TypeName, "getByteLjava/lang/Object;J")]
        public static unsafe sbyte getByte(Unsafe @this, object ptr, long offset)
        {
            return (sbyte) *index(ptr, offset);
        }

        [JniExport(TypeName, "putByteLjava/lang/Object;JB")]
        public static unsafe void putByte(Unsafe @this, object ptr, long offset, sbyte value)
        {
            *index(ptr, offset) = (byte) value;
        }

        [JniExport(TypeName, "getByteJ")]
        public static sbyte getByte(Unsafe @this, long ptr)
        {
            return (sbyte)Marshal.ReadByte(new IntPtr(ptr));
        }

        [JniExport(TypeName, "putByteJB")]
        public static void putByte(Unsafe @this, long ptr, sbyte value)
        {
            Marshal.WriteByte(new IntPtr(ptr), (byte) value);
        }

        [JniExport]
        public static bool compareAndSwapInt(Unsafe @this, object ptr, long offset, int value, int original)
        {
            lock (ptr)
            {
                var read = getInt(@this, ptr, offset);
                putInt(@this, ptr, offset, value);
                return read != original;
            }
        }

        [JniExport]
        public static bool compareAndSwapObject(Unsafe @this, object ptr, long offset, object value, object original)
        {
            lock (ptr)
            {
                var read = getObject(@this, ptr, offset);
                putObject(@this, ptr, offset, value);
                return read != original;
            }
        }

        [JniExport]
        public static bool compareAndSwapLong(Unsafe @this, object ptr, long offset, long value, long original)
        {
            lock (ptr)
            {
                var read = getLong(@this, ptr, offset);
                putLong(@this, ptr, offset, value);
                return read != original;
            }
        }

        [JniExport]
        public static long allocateMemory(Unsafe @this, long size)
        {
            return Marshal.AllocHGlobal(new IntPtr(size)).ToInt64();
        }

        [JniExport]
        public static void freeMemory(Unsafe @this, long ptr)
        {
            Marshal.FreeHGlobal(new IntPtr(ptr));
        }

        [JniExport]
        public static long reallocateMemory(Unsafe @this, long ptr, long newSize)
        {
            return Marshal.ReAllocHGlobal(new IntPtr(ptr), new IntPtr(newSize)).ToInt64();
        }

        [JniExport]
        public static void ensureClassInitialized(Unsafe @this, Class clazz)
        {
            RuntimeHelpers.RunClassConstructor(clazz.GetField<Type>("__nativeData").TypeHandle);
        }
    }
}
