using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaIoFileOutputStream
    {
        public const string TypeName = "java.io.FileOutputStream";

        [NativeData(TypeName)]
        public struct Data
        {
            internal FileStream FileStream;
        }

        [NativeImpl(typeof(void), TypeName, "open0", typeof(string), typeof(bool))]
        public static void open0(
            object @this, string path, bool append,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            data.FileStream = File.Open(path, append ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write);
        }

        [NativeImpl(typeof(void), TypeName, "write", typeof(int), typeof(bool))]
        public static void write(
            object @this, int value, bool append,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            data.FileStream.WriteByte((byte)value);
        }

        [NativeImpl(typeof(void), TypeName, "writeBytes", typeof(sbyte[]), typeof(int), typeof(int), typeof(bool))]
        public static void writeBytes(
            object @this, sbyte[] buffer, int offset, int count, bool append,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            data.FileStream.Write((byte[]) (Array) buffer, offset, count);
        }

        [NativeImpl(typeof(void), TypeName, "close0")]
        public static void close0(
            object @this,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            data.FileStream.Close();
        }

        [NativeImpl(typeof(void), TypeName, "initIDs", IsStatic = true)]
        public static void initIDs()
        {
        }

    }
}
