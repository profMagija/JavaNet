using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class SunReflectReflection
    {
        public const string TypeName = "sun.reflect.Reflection";

        [MethodImpl(MethodImplOptions.NoInlining)]
        [NativeImpl(typeof(Type), TypeName, "getCallerClass", IsStatic = true)]
        public static Type getCallerClass()
        {
            var st = new StackTrace();
            var f = st.GetFrame(2);
            return f.GetMethod().DeclaringType;
        }

        [NativeImpl(typeof(Type), TypeName, "getCallerClass", typeof(int), IsStatic = true)]
        public static Type getCallerClass(int offset)
        {
            var st = new StackTrace();
            var f = st.GetFrame(1 + offset);
            return f.GetMethod().DeclaringType;
        }

        [NativeImpl(typeof(int), TypeName, "getClassAccessFlags", typeof(Type), IsStatic = true)]
        public static int getClassAccessFlags(Type type)
        {
            return ClassPlugs.GetModifiers(type);
        }
    }
}
