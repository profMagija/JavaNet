using System;
using java.io;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.io
{
    public static class FileDescriptorNative
    {
        private const string TypeName = "java.io.FileDescriptor";

        [JniExport]
        public static void initIDs(Type fileDescriptor)
        {
        }

        [JniExport]
        public static void sync(FileDescriptor @this)
        {
            // TODO implement actual syncing
            // this will probably require some "catching onto"
            // various values ... (since we don't actually have access
            // to raw file handles)
        }
    }
}
