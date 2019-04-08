using System;
using System.IO;
using java.io;
using JavaNet.Runtime.Plugs;
using File = System.IO.File;

namespace JavaNet.Runtime.Native.j.io
{
    public static class FileOutputStreamNative
    {
        public const string TypeName = "java.io.FileOutputStream";

        public class Data
        {
            internal FileStream FileStream;
            internal int FileDesc;
        }

        private static FieldRef<Data> _nativeData;

        public static void open0(FileOutputStream @this, java.lang.String path, bool append)
        {
            _nativeData[@this].FileStream = File.Open(path, append ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write);
        }

        public static void write(FileOutputStream @this, int value, bool append)
        {
            if (_nativeData[@this].FileStream != null)
            {
                _nativeData[@this].FileStream.WriteByte((byte) value);
                return;
            }

            var cb = new[] {(char) value};
            if (@this.getFD() == FileDescriptor.@out)
                System.Console.Out.Write(cb);
            else if (@this.getFD() == FileDescriptor.err)
                System.Console.Error.Write(cb);
        }

        public static void writeBytes(FileOutputStream @this, sbyte[] buffer, int offset, int count, bool append)
        {
            if (_nativeData[@this].FileStream != null)
            {
                _nativeData[@this].FileStream.Write((byte[]) (Array) buffer, offset, count);
                return;
            }

            var cb = new char[count];
            for (int i = 0; i < count; i++)
            {
                cb[i] = (char) (byte) buffer[offset + i];
            }

            if (@this.getFD() == FileDescriptor.@out)
                System.Console.Out.Write(cb);
            else if (@this.getFD() == FileDescriptor.err)
                System.Console.Error.Write(cb);
        }

        public static void close0(object @this)
        {
            _nativeData[@this].FileStream?.Close();
        }

        public static void initIDs(Type fileOutputStream)
        {
            _nativeData = new FieldRef<Data>(fileOutputStream, "__nativeData");
        }

    }
}
