﻿using System.Collections.Generic;
using System.Linq;
using java.lang;

namespace JavaNet.Runtime.Plugs
{
    internal class StringAsCharSequence : CharSequence
    {
        internal string Str;

        public StringAsCharSequence(string str)
        {
            Str = str;
        }

        public char charAt(int index)
        {
            return Str[index];
        }

        public int length()
        {
            return Str.Length;
        }

        public CharSequence subSequence(int start, int end)
        {
            return new StringAsCharSequence(Str.Substring(start, end - start));
        }

        public override string ToString() => Str;
    }

    public static class CharSequenceMagic
    {
        

        [InstanceOfPlug(typeof(CharSequence))]
        public static int IsInstance(object o)
        {
            return o is string || o is CharSequence ? 1 : 0;
        }

        [CastPlug(typeof(CharSequence))]
        public static CharSequence CastToCharSequence(object o)
        {
            if (o is string s)
                return new StringAsCharSequence(s);
            return (CharSequence) o;
        }

        [CastPlug(typeof(string))]
        public static string CastToString(object o)
        {
            if (o is StringAsCharSequence s)
                return s.Str;
            return (string) o;
        }
    }
}
