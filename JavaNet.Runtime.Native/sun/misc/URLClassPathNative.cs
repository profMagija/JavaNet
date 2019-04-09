using System;
using java.lang;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.sun.misc
{
    public static class URLClassPathNative
    {
        public const string TypeName = "sun.misc.URLClassPath";

        [JniExport]
        [return: ActualType("java.net.URL[]")]
        public static object[] getLookupCacheURLs(Type type, ClassLoader cl)
        {
            return new object[0];
        }

        [JniExport]
        public static int[] getLookupCacheForClassLoader(Type type, ClassLoader cl, string url)
        {
            return new int[0];
        }

        [JniExport]
        public static bool knownToNotExist0(Type type, ClassLoader cl, string url)
        {
            return true;
        }

    }
}
