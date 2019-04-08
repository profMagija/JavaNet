using System;
using System.IO;
using java.io;
using JavaNet.Runtime.Plugs;
using File = System.IO.File;

namespace JavaNet.Runtime.Native.j.io
{
    public static class FileInputStreamNative
    {
        public const string TypeName = "java.io.FileInputStream";

        private static FieldRef<FileStream> _nativeData;

        [NativeMethodImpl]
        public static void open0(FileInputStream @this, java.lang.String path)
        {
            System.Console.WriteLine("Opening {0}", path);
            @this.SetField("__nativeData", File.OpenRead(path));
        }

        [NativeMethodImpl]
        public static int read0(FileInputStream @this)
        {
            return _nativeData[@this].ReadByte();
        }

        [NativeMethodImpl]
        public static int readBytes(FileInputStream @this, sbyte[] buffer, int offset, int count)
        {
            return _nativeData[@this].Read((byte[]) (Array) buffer, offset, count);
        }

        [NativeMethodImpl]
        public static long skip(FileInputStream @this, long count)
        {
            return _nativeData[@this].Seek(count, SeekOrigin.Current);
        }

        [NativeMethodImpl]
        public static int available(FileInputStream @this)
        {
            var str = _nativeData[@this];
            return (int) (str.Length - str.Position);
        }

        [NativeMethodImpl]
        public static void initIDs(Type fileInputStream)
        {
            _nativeData = new FieldRef<FileStream>(fileInputStream, "__nativeData");
        }

        [NativeMethodImpl]
        public static void close0(FileInputStream @this)
        {
            _nativeData[@this].Close();
        }


    }
}
