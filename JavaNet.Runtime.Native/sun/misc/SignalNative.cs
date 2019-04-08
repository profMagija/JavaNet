using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.sun.misc
{
    public static class SignalNative
    {
        public const string TypeName = "sun.misc.Signal";

        [NativeMethodImpl]
        public static int findSignal(java.lang.String name) => -1;
    }
}
