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

        [JniExport]
        public static void open0(FileInputStream @this, string path)
        {
            System.Console.WriteLine("Opening {0}", path);
            @this.SetField("__nativeData", File.OpenRead(path));
        }

        [JniExport]
        public static int read0(FileInputStream @this)
        {
            return _nativeData[@this].ReadByte();
        }

        [JniExport]
        public static int readBytes(FileInputStream @this, sbyte[] buffer, int offset, int count)
        {
            return _nativeData[@this].Read((byte[]) (Array) buffer, offset, count);
        }

        [JniExport]
        public static long skip(FileInputStream @this, long count)
        {
            return _nativeData[@this].Seek(count, SeekOrigin.Current);
        }

        [JniExport]
        public static int available(FileInputStream @this)
        {
            var str = _nativeData[@this];
            return (int) (str.Length - str.Position);
        }

        [JniExport]
        public static void initIDs(Type fileInputStream)
        {
            _nativeData = new FieldRef<FileStream>(fileInputStream, "__nativeData");
        }

        [JniExport]
        public static void close0(FileInputStream @this)
        {
            _nativeData[@this].Close();
        }


    }
}
