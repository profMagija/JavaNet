using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;

namespace JavaNet.Runtime.Plugs
{
    public static class StringPlugs
    {
        public const string TypeName = "System.String";

        [MethodPlug]
        public static string Ctor(int[] codePoints, int offset, int count)
        {
            var chars = new char[count * 2];
            var length = 0;

            for (var i = 0; i < count; i++)
            {
                if (codePoints[offset + i] <= char.MaxValue)
                {
                    chars[length++] = (char)codePoints[offset + i];
                }
                else
                {
                    var s = char.ConvertFromUtf32(codePoints[offset + i]);
                    chars[length++] = s[0];
                    chars[length++] = s[1];
                }

            }

            return new string(chars, 0, length);
        }

        [MethodPlug]
        public static string Ctor(sbyte[] bytes, int highByte, int offset, int count)
        {
            CheckBounds(bytes, offset, count);
            var chars = new char[count];
            if (highByte == 0)
            {
                for (var i = 0; i < count; i++)
                {
                    chars[i] = (char) (byte) bytes[offset + i];
                }
            }
            else
            {
                highByte <<= 8;
                for (var i = 0; i < count; i++)
                {
                    chars[i] = (char) (highByte | bytes[offset + i]);
                }
            }

            return new string(chars);
        }

        [MethodPlug]
        public static string Ctor(sbyte[] bytes, int highByte)
        {
            return Ctor(bytes, highByte, 0, bytes.Length);
        }

        [MethodPlug]
        public static string Ctor(sbyte[] bytes, int offset, int count, string encoding)
        {
            if (encoding == null) throw new NullReferenceException(nameof(encoding));
            return Encoding.GetEncoding(encoding).GetString((byte[]) (Array) bytes, offset, count);
        }

        [MethodPlug]
        public static string Ctor(sbyte[] bytes, string encoding)
        {
            return Ctor(bytes, 0, bytes.Length, encoding);
        }

        [MethodPlug]
        public static string Ctor(sbyte[] bytes, int offset, int count)
        {
            return Encoding.UTF8.GetString((byte[])(Array)bytes, offset, count);
        }

        [MethodPlug]
        public static string Ctor(sbyte[] bytes)
        {
            return Ctor(bytes, 0, bytes.Length);
        }

        [MethodPlug]
        public static string Ctor(char[] chars, bool _)
        {
            return new string(chars);
        }

        [MethodPlug]
        public static int length(string @this) => @this.Length;

        [MethodPlug]
        public static bool isEmpty(string @this) => @this.Length == 0;

        [MethodPlug]
        public static char charAt(string @this, int index) => @this[index];

        [MethodPlug]
        public static int codePointAt(string @this, int index) => char.ConvertToUtf32(@this, index);

        [MethodPlug]
        public static int codePointBefore(string @this, int index) =>
            char.IsLowSurrogate(@this, index) ? char.ConvertToUtf32(@this, index - 1) : char.ConvertToUtf32(@this, index);

        [MethodPlug]
        public static int codePointCount(string @this, int index)
        {
            return @this.Length - @this.Count(char.IsLowSurrogate);
        }

        [MethodPlug]
        public static int offsetByCodePoints(string @this, int offset)
        {
            var index = 0;
            while (offset-- > 0)
            {
                index++;
                if (char.IsSurrogate(@this[index]))
                    index++;
            }

            return index;
        }

        [MethodPlug]
        public static void getChars(string @this, char[] destination, int offset)
        {
            var source = @this.ToCharArray();
            Array.Copy(source, 0, destination, offset, source.Length);
        }

        [MethodPlug]
        public static void getChars(string @this, int from, int to, char[] destination, int offset)
        {
            var source = @this.ToCharArray(from, to - from);
            Array.Copy(source, 0, destination, offset, source.Length);
        }

        [MethodPlug]
        public static void getBytes(string @this, int from, int to, sbyte[] destination, int offset)
        {
            for (var i = from; i < to; i++)
            {
                destination[offset++] = (sbyte) (byte) @this[i];
            }
        }

        [MethodPlug]
        public static sbyte[] getBytes(string @this, string encoding)
        {
            return (sbyte[])(Array)Encoding.GetEncoding(encoding).GetBytes(@this);
        }

        [MethodPlug]
        public static sbyte[] getBytes(string @this)
        {
            return (sbyte[])(Array)Encoding.UTF8.GetBytes(@this);
        }

        [MethodPlug]
        public static bool contentEquals(string @this, IEnumerable<char> charSequence)
        {
            using (var en1 = @this.GetEnumerator())
            using (var en2 = charSequence.GetEnumerator())
            {
                bool m1, m2;
                while ((m1 = en1.MoveNext()) & (m2 = en2.MoveNext()))
                {
                    if (en1.Current != en2.Current)
                        return false;
                }

                if (m1 || m2)
                    return false;

                return true;
            }
        }

        [MethodPlug]
        public static bool equalsIgnoreCase(string @this, string other)
        {
            return @this.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }

        [MethodPlug]
        public static int compareTo(string @this, string other)
        {
            return string.Compare(@this, other, StringComparison.Ordinal);
        }

        [MethodPlug]
        public static int compareToIgnoreCase(string @this, string other)
        {
            return string.Compare(@this, other, StringComparison.OrdinalIgnoreCase);
        }

        [MethodPlug]
        public static bool regionMatches(string @this, bool ignoreCase, int thisOffset, string other, int otherOffset, int length)
        {
            return string.Equals(
                @this.Substring(thisOffset, length), 
                other.Substring(otherOffset, length), 
                ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        [MethodPlug]
        public static bool regionMatches(string @this, int thisOffset, string other, int otherOffset, int length)
        {
            return regionMatches(@this, false, thisOffset, other, otherOffset, length);
        }

        [MethodPlug]
        public static bool startsWith(string @this, string prefix, int offset)
        {
            return offset < @this.Length && @this.Substring(offset).StartsWith(prefix);
        }

        [MethodPlug]
        public static int hashCode(string @this)
        {
            var code = 0;
            foreach (var c in @this)
            {
                code = code * 31 + c;
            }

            return code;
        }

        [MethodPlug]
        public static int indexOf(string @this, int codePoint)
        {
            return @this.IndexOf(char.ConvertFromUtf32(codePoint), StringComparison.Ordinal);
        }

        [MethodPlug]
        public static int indexOf(string @this, int codePoint, int offset)
        {
            return @this.IndexOf(char.ConvertFromUtf32(codePoint), offset, StringComparison.Ordinal);
        }

        [MethodPlug]
        public static int lastIndexOf(string @this, int codePoint)
        {
            return @this.LastIndexOf(char.ConvertFromUtf32(codePoint), StringComparison.Ordinal);
        }

        [MethodPlug]
        public static int lastIndexOf(string @this, int codePoint, int offset)
        {
            return @this.LastIndexOf(char.ConvertFromUtf32(codePoint), offset, StringComparison.Ordinal);
        }

        [MethodPlug]
        public static string intern(string @this) => string.Intern(@this);

        [MethodPlug(IsStatic = true)]
        public static string valueOf(object obj) => obj != null ? obj.ToString() : "null";

        [MethodPlug(IsStatic = true)]
        public static string valueOf(int obj) => obj.ToString();

        [MethodPlug(IsStatic = true)]
        public static string valueOf(float obj) => obj.ToString();

        [MethodPlug(IsStatic = true)]
        public static string valueOf(double obj) => obj.ToString();

        [MethodPlug(IsStatic = true)]
        public static string valueOf(long obj) => obj.ToString();

        [MethodPlug(IsStatic = true)]
        public static string valueOf(short obj) => obj.ToString();

        [MethodPlug(IsStatic = true)]
        public static string valueOf(sbyte obj) => obj.ToString();

        [MethodPlug(IsStatic = true)]
        public static string valueOf(char obj) => obj.ToString();


        private static void CheckBounds(Array array, int offset, int count)
        {
            if (offset < 0)
                throw new IndexOutOfRangeException();

            if (count < 0)
                throw new IndexOutOfRangeException();

            if (array.Length <= offset + count)
                throw new IndexOutOfRangeException();
        }
    }
}
