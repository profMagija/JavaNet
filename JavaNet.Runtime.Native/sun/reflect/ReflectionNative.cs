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

        [MethodImpl(MethodImplOptions.NoInlining), JniExport]
        public static Class getCallerClass(Type reflection)
        {
            var st = new StackTrace();
            var f = st.GetFrame(2);
            return (Class) ReflectionBridge.GetClass(f.GetMethod().DeclaringType);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [JniExport(TypeName, "getCallerClassI")]
        public static Class getCallerClass(Type reflection, int offset)
        {
            var st = new StackTrace();
            var f = st.GetFrame(1 + offset);
            return (Class) ReflectionBridge.GetClass(f.GetMethod().DeclaringType);
        }

        [JniExport]
        public static int getClassAccessFlags(Type reflection, Class type)
        {
            return type.getModifiers();
        }
    }
}
