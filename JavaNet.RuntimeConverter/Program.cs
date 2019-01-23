using System;
using System.IO;

namespace JavaNet.RuntimeConverter
{
    class Program
    {
        public static void Main(string[] args)
        {
            var jf = JarReader.BuildJarFile(File.OpenRead("/home/prof/git/openjdk/rt.jar"));
            var ab = new JavaAssemblyBuilder();
            var ad = ab.BuildAssembly("JavaNet.Runtime", new Version(1, 0, 0, 0), jf);
            
        }
    }
}