using System;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.io
{
    public static class ConsoleNative
    {
        public const string TypeName = "java.io.Console";

        [JniExport]
        public static bool istty(Type console)
        {
            return !System.Console.IsOutputRedirected && !System.Console.IsInputRedirected;
        }

        [JniExport]
        public static bool echo(Type console, bool value)
        {
            return false;
        }

        [JniExport]
        public static string encoding(Type console)
        {
            return "UTF-8";
        }
    }
}
