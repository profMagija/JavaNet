using System;
using System.IO;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.io
{
    public static class UnixFileSystemNative
    {
        public const string TypeName = "java.io.UnixFileSystem";

        [JniExport]
        public static string canonicalize0(object @this, string name)
        {
            try
            {
                return Path.GetFullPath(name);
            }
            catch (IOException ex)
            {
                throw new java.io.IOException(ex.ToString());
            }
        }

        [JniExport]
        public static int getBooleanAttributes0(object @this, java.io.File file)
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

        private static string GetPath(java.io.File file)
        {
            string path = file.getPath();

            if (path.StartsWith("file:/") || path.StartsWith("file:\\"))
                path = path.Substring("file:/".Length);

            return path;
        }

        [JniExport]
        public static bool checkAccess(object @this, java.io.File file, int access)
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

        [JniExport]
        public static long getLastModifiedTime(object @this, java.io.File file)
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

        [JniExport]
        public static long getLength(object @this, java.io.File file)
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

        [JniExport]
        public static void initIDs(Type type) { }
    }
}
