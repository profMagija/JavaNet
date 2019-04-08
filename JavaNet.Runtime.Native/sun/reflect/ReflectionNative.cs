using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using java.lang;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.sun.reflect
{
    public static class ReflectionNative
    {
        public const string TypeName = "sun.reflect.Reflection";

        [MethodImpl(MethodImplOptions.NoInlining), NativeMethodImpl]
        public static Type getCallerClass()
        {
            var st = new StackTrace();
            var f = st.GetFrame(2);
            return f.GetMethod().DeclaringType;
        }

        [MethodImpl(MethodImplOptions.NoInlining), NativeMethodImpl]
        public static Type getCallerClass(int offset)
        {
            var st = new StackTrace();
            var f = st.GetFrame(1 + offset);
            return f.GetMethod().DeclaringType;
        }

        [NativeMethodImpl]
        public static int getClassAccessFlags(Type reflection, Class type)
        {
            return type.getModifiers();
        }
    }
}
