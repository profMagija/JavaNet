using System;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.io
{
    public static class ConsoleNative
    {
        public const string TypeName = "java.io.Console";

        [NativeMethodImpl]
        public static bool istty(Type console)
        {
            return !System.Console.IsOutputRedirected && !System.Console.IsInputRedirected;
        }

        [NativeMethodImpl]
        public static bool echo(Type console, bool value)
        {
            return false;
        }

        [NativeMethodImpl]
        public static java.lang.String encoding(Type console)
        {
            return "UTF-8";
        }
    }
}
