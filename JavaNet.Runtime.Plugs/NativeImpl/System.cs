using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaLangSystem
    {
        [NativeImpl("System.Void", "java.lang.System", "registerNatives", IsStatic = true)]
        public static void RegisterNatives(
            [MethodPtr(true, "System.Void", "initializeSystemClass")] Action initializeSystemClass, 
            [TypeHandle("java.lang.System")] Type systemType)
        {
            _systemType = systemType;
            initializeSystemClass();
        }

        private static void sth() { }

        private static Type _systemType;

        [NativeImpl("System.Void", "java.lang.System", "setIn0", "java.io.InputStream", IsStatic = true)]
        public static void SetIn0([ActualType("java.io.InputStream")] object in0)
        {

            _systemType.GetField("in", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, in0);
        }

        [NativeImpl("System.Void", "java.lang.System", "setOut0", "java.io.PrintStream", IsStatic = true)]
        public static void SetOut0([ActualType("java.io.PrintStream")] object out0, [TypeHandle("java.lang.System")] Type systemType)
        {
            systemType.GetField("out", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, out0);
        }

        [NativeImpl("System.Void", "java.lang.System", "setErr0", "java.io.PrintStream", IsStatic = true)]
        public static void SetErr0([ActualType("java.io.PrintStream")] object err0, [TypeHandle("java.lang.System")] Type systemType)
        {
            systemType.GetField("err", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, err0);
        }
    }
}
