using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaIoFileDescriptor
    {
        private const string TypeName = "java.io.FileDescriptor";

        internal static object _in;
        internal static object _out;
        internal static object _err;

        [NativeImpl(typeof(void), TypeName, "initIDs", IsStatic = true)]
        public static void InitIDs(
            [FieldPtr("in", true)] ref object inFd,
            [FieldPtr("out", true)] ref object outFd,
            [FieldPtr("err", true)] ref object errFd
            )
        {
            _in = inFd;
            _out = outFd;
            _err = errFd;
        }

        [NativeImpl(typeof(void), TypeName, "sync")]
        public static void Sync(
            [ActualType("java.io.FileDescriptor")] object @this,
            [FieldPtr("fd", false)] ref int fd)
        {
            // TODO implement actual syncing
            // this will probably require some "catching onto"
            // various values ... (since we don't actually have access
            // to raw file handles)
        }
    }
}
