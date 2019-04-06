using System;
using System.Globalization;
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
            var javaHome = Assembly.GetCallingAssembly().CodeBase;

            props.setProperty("file.encoding", "UTF-8");
            props.setProperty("file.separator", Path.DirectorySeparatorChar.ToString());

            props.setProperty("java.class.path", "");
            props.setProperty("java.class.version", "55.0");
            props.setProperty("java.home", javaHome);
            props.setProperty("java.io.tmpdir", Path.GetTempPath());
            props.setProperty("java.library.path", javaHome);
            props.setProperty("java.runtime.name", "JavaNet Runtime");
            props.setProperty("java.runtime.version", "8.0");
            props.setProperty("java.specification.name", "Java Platform API Specification");
            props.setProperty("java.specification.vendor", "Oracle Corporation");
            props.setProperty("java.specification.version", "8");
            props.setProperty("java.vendor", "profMagija");
            props.setProperty("java.vendor.url", "https://github.com/profMagija/JavaNet");
            props.setProperty("java.vendor.version", Assembly.GetCallingAssembly().GetName().Version.ToString());
            props.setProperty("java.version", "8.0");
            props.setProperty("java.vm.info", "clr");
            props.setProperty("java.vm.name", "CLR");

            props.setProperty("line.separator", Environment.NewLine);

            props.setProperty("os.arch", "amd64"); // probably
            props.setProperty("os.name", Environment.OSVersion.ToString()); 
            props.setProperty("os.version", Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor);

            props.setProperty("path.separator", Path.PathSeparator.ToString());

            props.setProperty("sun.arch.data.model", (IntPtr.Size * 8).ToString());
            props.setProperty("sun.boot.library.path", javaHome);
            props.setProperty("sun.cpu.endian", BitConverter.IsLittleEndian ? "little" : "big");
            props.setProperty("sun.cpu.isalist", "amd64"); // probably
            props.setProperty("sun.desktop", "windows"); // whatever
            props.setProperty("sun.io.unicode.encoding", "UnicodeLittle"); // whatever
            props.setProperty("sun.java.launcher", "CLR");
            props.setProperty("sun.jnu.encoding", "UTF-8");
            props.setProperty("sun.management.compiler", "RyuJIT");
            props.setProperty("sun.os.patch.level", Environment.OSVersion.Version.Build.ToString());
            props.setProperty("sun.stderr.encoding", "UTF-8");
            props.setProperty("sun.stdout.encoding", "UTF-8");
            props.setProperty("user.country", "US");
            props.setProperty("user.dir", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            props.setProperty("user.home", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            props.setProperty("user.language", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            props.setProperty("user.name", Environment.UserName);
            props.setProperty("user.script", "");
            props.setProperty("user.timezone", "");
            props.setProperty("user.variant", "");

            props.setProperty("java.locale.providers", "JRE,CLDR,SPI,HOST,FALLBACK");
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
