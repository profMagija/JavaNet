using System;
using System.IO;

namespace JavaNet.RuntimeConverter
{
    class Program
    {
        public static void Main(string[] args)
        {
            var jf = JarReader.BuildJarFile(@"C:\Program Files\Java\java-se-8u40-ri-compact1\lib\rt.jar");
            var ab = new JavaAssemblyBuilder();
            var ad = ab.BuildAssembly("JavaNet.Runtime", new Version(1, 0, 0, 0), jf);
            ad.Write("JavaNet.Runtime.dll");
        }
    }
}