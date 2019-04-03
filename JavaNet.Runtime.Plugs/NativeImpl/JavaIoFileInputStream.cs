using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaIoFileInputStream
    {
        public const string TypeName = "java.io.FileInputStream";

        [NativeData(TypeName)]
        public struct Data
        {
            internal FileStream FileStream;
        }

        [NativeImpl(typeof(void), TypeName, "open0", typeof(string))]
        public static void open0(
            object @this, string path,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            data.FileStream = File.OpenRead(path);
        }

        [NativeImpl(typeof(int), TypeName, "read0")]
        public static int read0(
            object @this,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            return data.FileStream.ReadByte();
        }

        [NativeImpl(typeof(int), TypeName, "readBytes", typeof(sbyte[]), typeof(int), typeof(int))]
        public static int readBytes(
            object @this, sbyte[] buffer, int offset, int count,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            return data.FileStream.Read((byte[]) (Array) buffer, 0, count);
        }

        [NativeImpl(typeof(long), TypeName, "skip", typeof(long))]
        public static long skip(
            object @this, long count,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            return data.FileStream.Seek(count, SeekOrigin.Current);
        }

        [NativeImpl(typeof(int), TypeName, "available")]
        public static int available(
            object @this,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            return (int) (data.FileStream.Length - data.FileStream.Position);
        }

        [NativeImpl(typeof(void), TypeName, "initIDs", IsStatic = true)]
        public static void initIDs()
        {
        }

        [NativeImpl(typeof(void), TypeName, "close0")]
        public static void close0(
            object @this,
            [FieldPtr("__nativeData", false)] ref Data data)
        {
            data.FileStream.Close();
        }


    }
}
