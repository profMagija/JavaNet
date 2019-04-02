using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaLangSystem
    {
        private const string TypeName = "java.lang.System";

        [NativeImpl("System.Void", TypeName, "registerNatives", IsStatic = true)]
        public static void RegisterNatives(
            [MethodPtr(true, "System.Void", "initializeSystemClass")] Action initializeSystemClass, 
            [TypeHandle(TypeName)] Type systemType)
        {
            _systemType = systemType;
            initializeSystemClass();
        }

        private static void sth() { }

        private static Type _systemType;

        [NativeImpl("System.Void", TypeName, "setIn0", "java.io.InputStream", IsStatic = true)]
        public static void SetIn0([ActualType("java.io.InputStream")] object in0)
        {
            _systemType.GetField("in", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, in0);
        }

        [NativeImpl("System.Void", TypeName, "setOut0", "java.io.PrintStream", IsStatic = true)]
        public static void SetOut0([ActualType("java.io.PrintStream")] object out0)
        {
            _systemType.GetField("out", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, out0);
        }

        [NativeImpl("System.Void", TypeName, "setErr0", "java.io.PrintStream", IsStatic = true)]
        public static void SetErr0([ActualType("java.io.PrintStream")] object err0)
        {
            _systemType.GetField("err", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, err0);
        }

        [NativeImpl(typeof(long), TypeName, "currentTimeMillis", IsStatic = true)]
        public static long CurrentTimeMillis()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        [NativeImpl(typeof(long), TypeName, "nanoTime", IsStatic = true)]
        public static long NanoTime()
        {
            return DateTime.Now.Ticks * 1000000 / TimeSpan.TicksPerMillisecond;
        }

        [NativeImpl(typeof(void), TypeName, "arraycopy", typeof(object), typeof(int), typeof(object), typeof(int), typeof(int), IsStatic = true)]
        public static void ArrayCopy(object src, int srcIndex, object target, int targetIndex, int length)
        {
            Array.Copy((Array) src, srcIndex, (Array) target, targetIndex, length);
        }

        [NativeImpl(typeof(int), TypeName, "identityHashCode", typeof(object), IsStatic = true)]
        public static int IdentityHashCode(object obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        [NativeImpl("java.util.Properties", TypeName, "initProperties", "java.util.Properties", IsStatic = true)]
        [return: ActualType("java.util.Properties")]
        public static object InitProperties(object props)
        {
            return props;
        }
    }
}
