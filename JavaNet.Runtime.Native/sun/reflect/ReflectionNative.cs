using System.Diagnostics;
using System.Runtime.CompilerServices;
using java.lang;
using java.lang.reflect;
using JavaNet.Runtime.Plugs;
using Type = System.Type;

namespace JavaNet.Runtime.Native.sun.reflect
{
    public static class ReflectionNative
    {
        public const string TypeName = "sun.reflect.Reflection";

        [MethodImpl(MethodImplOptions.NoInlining), JniExport]
        public static Class getCallerClass(Type reflection)
        {
            return getCallerClass(reflection, 2);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [JniExport(TypeName, "getCallerClassI")]
        public static Class getCallerClass(Type reflection, int offset)
        {
            var st = new StackTrace();
            var gcc = 0;
            while (st.GetFrame(gcc).GetMethod()?.DeclaringType?.FullName != TypeName)
            {
                gcc++;
            }

            // gcc is now index of lava.lang.Class::getCallerClass

            return (Class) ReflectionBridge.GetClass(st.GetFrame(gcc + offset).GetMethod().DeclaringType);
        }

        [JniExport]
        public static int getClassAccessFlags(Type reflection, Class type)
        {
            return type.getModifiers() & 7;
        }
    }
}
