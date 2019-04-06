using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class SunMiscURLClassPath
    {
        public const string TypeName = "sun.misc.URLClassPath";

        [NativeImpl(IsStatic = true)]
        [return: ActualType("java.net.URL[]")]
        public static object[] getLookupCacheURLs([ActualType("java.lang.ClassLoader")] object cl)
        {
            return new object[0];
        }

        [NativeImpl(IsStatic = true)]
        public static int[] getLookupCacheForClassLoader([ActualType("java.lang.ClassLoader")] object cl, string url)
        {
            return new int[0];
        }

        [NativeImpl(IsStatic = true)]
        public static bool knownToNotExist0([ActualType("java.lang.ClassLoader")] object cl, string url)
        {
            return true;
        }

    }
}
