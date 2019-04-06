using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    [VolatileFields(TypeName)]
    public static class JavaLangSystem
    {
        public const string TypeName = "java.lang.System";

        [NativeImpl(IsStatic = true)]
        public static void registerNatives(
            [MethodPtr(true, "System.Void", "initializeSystemClass")] Action initSystem
        )
        {
            _initSystem = initSystem;
        }

        private static Action _initSystem;

        [NativeImpl("System.Void", TypeName, "setIn0", "java.io.InputStream", IsStatic = true)]
        public static void SetIn0([ActualType("java.io.InputStream")] object in0, [FieldPtr("in", true)] out object @in)
        {
            @in = in0;
        }

        [NativeImpl("System.Void", TypeName, "setOut0", "java.io.PrintStream", IsStatic = true)]
        public static void SetOut0(
            [ActualType("java.io.PrintStream")] object out0,
            [FieldPtr("out", true)] out object @out)
        {
            @out = out0;
        }

        [NativeImpl("System.Void", TypeName, "setErr0", "java.io.PrintStream", IsStatic = true)]
        public static void SetErr0(
            [ActualType("java.io.PrintStream")] object err0,
            [FieldPtr("out", true)] out object err)
        {
            err = err0;
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
        public static object InitProperties(dynamic props)
        {
            props.setProperty("file.encoding", "US_ASCII");
            props.setProperty("line.separator", Environment.NewLine);
            props.setProperty("file.separator", Path.DirectorySeparatorChar.ToString());
            props.setProperty("path.separator", Path.PathSeparator.ToString());
            props.setProperty("java.home", "");
            props.setProperty("sun.stdout.encoding", "UTF-8");
            props.setProperty("sun.stderr.encoding", "UTF-8");
            return props;
        }

        [NativeImpl(IsStatic = true)]
        public static string mapLibraryName(string name) => name;

        [ModuleLoadHook]
        public static void LoadSystem()
        {
            RuntimeHelpers.RunClassConstructor(Type.GetType("java.lang.System, JavaNet.Runtime", true).TypeHandle);
            _initSystem();

        }
    }
}
