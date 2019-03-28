using System.IO;

namespace JavaNet
{
    public static class StreamExtensions
    {
        public static byte U1(this Stream s) => (byte) s.ReadByte();

        public static sbyte I1(this Stream s) => (sbyte) s.ReadByte();

        public static ushort U2(this Stream s) => (ushort) ((s.ReadByte() << 8) | s.ReadByte());

        public static short I2(this Stream s) => (short) s.U2();

        public static uint U4(this Stream s) => (uint) ((s.U2() << 16) | s.U2());

        public static int I4(this Stream s) => (s.U2() << 16) | s.U2();

        public static ulong U8(this Stream s) => ((ulong)s.U4() << 32) | s.U4();

        public static long I8(this Stream s) => ((long) s.U4() << 32) | s.U4();

        public static void WriteU1(this Stream s, byte b) => s.WriteByte(b);
        
        public static void WriteI1(this Stream s, sbyte b) => s.WriteByte((byte)b);

        public static void WriteU2(this Stream s, ushort b)
        {
            s.WriteByte((byte) (b >> 8));
            s.WriteByte((byte) b);
        }

        public static void WriteI2(this Stream s, short b)
        {
            s.WriteByte((byte) (b >> 8));
            s.WriteByte((byte) b);
        }
        
        public static void WriteU4(this Stream s, uint b)
        {
            s.WriteByte((byte) (b >> 24));
            s.WriteByte((byte) (b >> 16));
            s.WriteByte((byte) (b >> 8));
            s.WriteByte((byte) b);
        }

        public static void WriteI4(this Stream s, int b)
        {
            s.WriteByte((byte) (b >> 24));
            s.WriteByte((byte) (b >> 16));
            s.WriteByte((byte) (b >> 8));
            s.WriteByte((byte) b);
        }
        
        public static void WriteU8(this Stream s, ulong b)
        {
            s.WriteU4((uint) (b >> 32));
            s.WriteU4((uint) b);
        }
        
        public static void WriteI8(this Stream s, long b)
        {
            s.WriteU4((uint) (b >> 32));
            s.WriteU4((uint) b);
        }

        public static void WriteBytes(this Stream s, byte[] bytes)
        {
            s.Write(bytes, 0, bytes.Length);
        }

        public static byte[] ReadNext(this Stream s, int len)
        {
            var rv = new byte[len];
            s.Read(rv, 0, len);
            return rv;
        }
    }
}