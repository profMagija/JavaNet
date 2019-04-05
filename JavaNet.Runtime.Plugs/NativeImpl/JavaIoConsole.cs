using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaIoConsole
    {
        public const string TypeName = "java.io.Console";

        [NativeImpl(IsStatic = true)]
        public static bool istty()
        {
            return !Console.IsOutputRedirected && !Console.IsInputRedirected;
        }

        [NativeImpl(IsStatic = true)]
        public static bool echo(bool value)
        {
            return false;
        }

        [NativeImpl(IsStatic = true)]
        public static string encoding()
        {
            return "UTF-8";
        }
    }
}
