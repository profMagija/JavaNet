using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.sun.misc
{
    public static class SignalNative
    {
        public const string TypeName = "sun.misc.Signal";

        [JniExport]
        public static int findSignal(string name) => -1;
    }
}
