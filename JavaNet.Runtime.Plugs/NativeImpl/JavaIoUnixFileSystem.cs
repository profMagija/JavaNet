using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaIoUnixFileSystem
    {
        public const string TypeName = "java.io.UnixFileSystem";

        [NativeImpl]
        public static string canonicalize0(object @this, string name)
        {
            try
            {
                return Path.GetFullPath(name);
            }
            catch (IOException ex)
            {
                throw PlugHelpers.ThrowForName("java.io.IOException", ex);
            }
        }

        [NativeImpl]
        public static int getBooleanAttributes0(object @this, [ActualType("java.io.File")] object file)
        {
            int rv = 0;
            var path = GetPath(file);

            if (File.Exists(path) || Directory.Exists(path))
                rv |= 1;

            if (File.Exists(path))
            {
                rv |= 2;
            }

            if (Directory.Exists(path))
                rv |= 4;

            if (rv != 0)
            {
                var attrs = File.GetAttributes(path);
                if ((attrs & FileAttributes.Hidden) != 0)
                    rv |= 8;
            }

            return rv;
        }

        private static string GetPath(object file)
        {
            string path = ((dynamic)file).getPath();

            if (path.StartsWith("file:/") || path.StartsWith("file:\\"))
                path = path.Substring("file:/".Length);

            return path;
        }

        [NativeImpl]
        public static bool checkAccess(object @this, [ActualType("java.io.File")] object file, int access)
        {
            var path = GetPath(file);

            if ((access & 4) != 0)
            {
                try
                {
                    File.OpenRead(path).Dispose();
                }
                catch (IOException)
                {
                    return false;
                }
            }

            if ((access & 2) != 0)
            {
                try
                {
                    File.Open(path, FileMode.Open, FileAccess.Write).Dispose();
                }
                catch (IOException)
                {
                    return false;
                }
            }

            return true;
        }

        [NativeImpl]
        public static long getLastModifiedTime(object @this, [ActualType("java.io.File")] object file)
        {
            var path = GetPath(file);

            try
            {
                return File.GetLastWriteTime(path).ToFileTime();
            }
            catch (IOException)
            {
                return 0;
            }
        }

        [NativeImpl]
        public static long getLength(object @this, [ActualType("java.io.File")] object file)
        {
            var path = GetPath(file);

            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                    return stream.Length;
            }
            catch (IOException)
            {
                return 0;
            }
        }

        [NativeImpl(IsStatic = true)]
        public static void initIDs() { }
    }
}
