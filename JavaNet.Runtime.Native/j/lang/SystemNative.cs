using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using java.io;
using java.util;
using JavaNet.Runtime.Plugs;
using JavaNet.Runtime.Plugs.NativeImpl;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class SystemNative
    {
        public const string TypeName = "java.lang.System";

        [JniExport]
        public static void registerNatives(Type system)
        {
            JNI.RegisterNativeMethod(system, nameof(currentTimeMillis), (Func<Type, long>) currentTimeMillis);
            JNI.RegisterNativeMethod(system, nameof(nanoTime), (Func<Type, long>) nanoTime);
            JNI.RegisterNativeMethod(system, nameof(arraycopy), (Action<Type, object, int, object, int, int>) arraycopy);
        }

        [JniExport]
        public static void setIn0(Type system, InputStream in0)
        {
            system.SetStatic("in", in0);
        }

        [JniExport]
        public static void setOut0(Type system, OutputStream out0)
        {
            system.SetStatic("out", out0);
        }

        [JniExport]
        public static void setErr0(Type system, OutputStream err0)
        {
            system.SetStatic("err", err0);
        }

        [JniExport]
        public static long currentTimeMillis(Type system)
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        [JniExport]
        public static long nanoTime(Type system)
        {
            return DateTime.Now.Ticks * 1000000 / TimeSpan.TicksPerMillisecond;
        }

        [JniExport]
        public static void arraycopy(Type system, object src, int srcIndex, object target, int targetIndex, int length)
        {
            Array.Copy((Array) src, srcIndex, (Array) target, targetIndex, length);
        }

        [JniExport]
        public static int identityHashCode(Type system, object obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }

        [JniExport]
        public static object initProperties(Type system, Properties props)
        {
            void P(string n, string v)
            {
                props.setProperty(n, v);
            }

            void PI(string n, object v)
            {
                props.setProperty(n, v.ToString());
            }

            P("java.specification.name", "Java Platform API Specification");
            P("java.specification.vendor", "Oracle Corporation");
            P("java.specification.version", "8.0");

            P("java.version", "8.0");
            P("java.vendor", "profMagija");
            P("java.vendor.url", "https://github.com/profMagija/JavaNet");
            P("java.vendor.url.bug", "https://github.com/profMagija/JavaNet/issues");

            P("java.class.version", "55.0");

            P("os.name", Environment.OSVersion.ToString());
            P("os.name", Environment.OSVersion.ToString()); 

            P("file.separator", Path.DirectorySeparatorChar.ToString());
            P("path.separator", Path.PathSeparator.ToString());
            P("line.separator", Environment.NewLine);

            P("file.encoding", "UTF-8");
            P("sun.jnu.encoding", "UTF-8");
            P("sun.stdout.encoding", "UTF-8");
            P("sun.stderr.encoding", "UTF-8");
            P("file.encoding.pkg", "sun.io");
            P("sun.io.unicode.encoding", "little");
            P("sun.cpu.isalist", "amd64");

            PI("sun.arch.data.model", IntPtr.Size * 8);
            PI("sun.os.patch.level", Environment.OSVersion.Version.Revision);

            P("java.io.tmpdir", Path.GetTempPath());

            P("user.name", Environment.UserName);
            P("user.home", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

            P("user.dir", Environment.CurrentDirectory);

            P("sun.desktop", "windows");


            var culture = CultureInfo.CurrentCulture;

            void FillI18NProp(string propName, string value)
            {
                P(propName, value);
                P($"{propName}.display", value);
                P($"{propName}.format", value);
            }

            FillI18NProp("user.language", culture.TwoLetterISOLanguageName);
            FillI18NProp("user.script", "");
            FillI18NProp("user.country", "");
            FillI18NProp("user.variant", "");

            return props;
        }

        
        [JniExport]
        public static string mapLibraryName(Type system, string name) => name;

        [ModuleLoadHook]
        public static void LoadSystem()
        {
            JNI.RegisterNativeLibrary(typeof(SystemNative).Assembly);
            RuntimeHelpers.RunClassConstructor(Type.GetType("java.lang.System, JavaNet.Runtime", true).TypeHandle);
            typeof(java.lang.System).CallStatic("initializeSystemClass");
        }
    }
}
