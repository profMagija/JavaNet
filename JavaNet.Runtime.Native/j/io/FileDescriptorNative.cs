using System;
using java.io;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.io
{
    public static class FileDescriptorNative
    {
        private const string TypeName = "java.io.FileDescriptor";

        [NativeMethodImpl]
        public static void InitIDs(Type fileDescriptor)
        {
        }

        [NativeMethodImpl]
        public static void Sync(FileDescriptor @this)
        {
            // TODO implement actual syncing
            // this will probably require some "catching onto"
            // various values ... (since we don't actually have access
            // to raw file handles)
        }
    }
}
