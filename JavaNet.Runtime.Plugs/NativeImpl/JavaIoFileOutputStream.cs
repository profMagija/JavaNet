using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaIoFileOutputStream
    {
        public const string TypeName = "java.io.FileOutputStream";

        [NativeData(TypeName)]
        public struct Data
        {
            internal FileStream FileStream;
            internal int FileDesc;
        }

        [Hook(MethodName = ".ctor")]
        public static void Ctor(
            object @this,
            [ActualType("java.io.FileDescriptor")] object fd,
            [NativeDataParam] ref Data data)
        {
            if (fd == JavaIoFileDescriptor._out)
                data.FileDesc = 1;
            else if (fd == JavaIoFileDescriptor._err)
                data.FileDesc = 2;
        }

        [NativeImpl(typeof(void), TypeName, "open0", typeof(string), typeof(bool))]
        public static void open0(
            object @this, string path, bool append,
            [NativeDataParam] ref Data data)
        {
            data.FileStream = File.Open(path, append ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write);
        }

        [NativeImpl(typeof(void), TypeName, "write", typeof(int), typeof(bool))]
        public static void write(
            object @this, int value, bool append,
            [NativeDataParam] ref Data data)
        {
            if (data.FileStream != null)
            {
                data.FileStream.WriteByte((byte) value);
                return;
            }

            var cb = new[] {(char) value};
            switch (data.FileDesc)
            {
                case 1:
                    Console.Out.Write(cb);
                    break;
                case 2:
                    Console.Error.Write(cb);
                    break;
            }
        }

        [NativeImpl(typeof(void), TypeName, "writeBytes", typeof(sbyte[]), typeof(int), typeof(int), typeof(bool))]
        public static void writeBytes(
            object @this, sbyte[] buffer, int offset, int count, bool append,
            [NativeDataParam] ref Data data)
        {
            if (data.FileStream != null)
            {
                data.FileStream.Write((byte[]) (Array) buffer, offset, count);
                return;
            }

            var cb = new char[count];
            for (int i = 0; i < count; i++)
            {
                cb[i] = (char) (byte) buffer[offset + i];
            }

            switch (data.FileDesc)
            {
                case 1:
                    Console.Out.Write(cb);
                    break;
                case 2:
                    Console.Error.Write(cb);
                    break;
            }
        }

        [NativeImpl(typeof(void), TypeName, "close0")]
        public static void close0(
            object @this,
            [NativeDataParam] ref Data data)
        {
            data.FileStream?.Close();
        }

        [NativeImpl(typeof(void), TypeName, "initIDs", IsStatic = true)]
        public static void initIDs()
        {
        }

    }
}
