using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using java.lang;

// ReSharper disable UnusedMember.Global

namespace JavaNet.Runtime.Plugs
{
    public class StringPlugs
    {
        [MethodPlug("java.lang.String.<init>:()V")]
        public static string Ctor() => new string(new char[0]);

        [MethodPlug("java.lang.String.<init>:([B)V")]
        public static string Ctor(byte[] bytes) => Encoding.UTF8.GetString(bytes);

        [MethodPlug("java.lang.String.<init>:([BII)V")]
        public static string Ctor(byte[] bytes, int offset, int length) => Encoding.UTF8.GetString(bytes, offset, length);

        //[MethodPlug("java.lang.String.<init>([C)V")]
        //public static string Ctor(char[] chars) => new string(chars);

        [MethodPlug("java.lang.String.<init>:([III)V")]
        public static string Ctor(int[] codePoints, int offset, int count)
        {
            var chars = new char[codePoints.Length * 2];
            var len = 0;
            for (int i = offset; i < offset + count; i++)
            {
                var s = char.ConvertFromUtf32(codePoints[i]);
                chars[len++] = s[0];
                if (s.Length > 1)
                    chars[len++] = s[1];
            }

            return new string(chars, 0, len);
        }

        [MethodPlug("java.lang.String.<init>:(Ljava/lang/String;)V")]
        public static string Ctor(string original) => original;

        [MethodPlug("java.lang.String.charAt:(I)C")]
        public static char CharAt(string str, int index) => str[index];

        [MethodPlug("java.lang.String.codePointAt:(I)I")]
        public static int CodePointAt(string str, int index) => char.ConvertToUtf32(str, index);

        [MethodPlug("java.lang.String.compareTo:(Ljava/lang/String;)I")]
        public static int CompareTo(string str, string other) => string.CompareOrdinal(str, other);

        [MethodPlug("java.lang.String.compareToIgnoreCase:(Ljava/lang/String;)I")]
        public static int CompareToIgnoreCase(string str, string other) => string.Compare(str, other, StringComparison.OrdinalIgnoreCase);

        [MethodPlug("java.lang.String.concat:(Ljava/lang/String;)Ljava/lang/String;")]
        public static string Concat(string str, string other) => str + other;

        [MethodPlug("java.lang.String.contains:(Ljava/lang/CharSequence;)Z")]
        public static bool Contains(string str, CharSequence other) => str.Contains(other.ToString());

        [MethodPlug("java.lang.String.contentEquals:(Ljava/lang/CharSequence;)Z")]
        public static bool ContentEquals(string str, CharSequence other) => str.Equals(other.ToString());

        [MethodPlug("java.lang.String.copyValueOf:([C)Ljava/lang/String;")]
        public static string CopyValueOf(char[] data) => new string(data);

        [MethodPlug("java.lang.String.copyValueOf:([CII)Ljava/lang/String;")]
        public static string CopyValueOf(char[] data, int offset, int count) => new string(data, offset, count);

        [MethodPlug("java.lang.String.endsWith:(Ljava/lang/String;)Z")]
        public static bool EndsWith(string str, string other) => str.EndsWith(other);

        [MethodPlug("java.lang.String.equalsIgnoreCase:(Ljava/lang/String;)Z")]
        public static bool EqualsIgnoreCase(string str, string other) => str.Equals(other, StringComparison.OrdinalIgnoreCase);

        [MethodPlug("java.lang.String.indexOf:(I)I")]
        public static int IndexOf(string s, int i) => s.IndexOf(char.ConvertFromUtf32(i), StringComparison.Ordinal);

        [MethodPlug("java.lang.String.indexOf:(II)I")]
        public static int IndexOf(string s, int ch, int fromIndex) => s.IndexOf(char.ConvertFromUtf32(ch), fromIndex, StringComparison.Ordinal);

        [MethodPlug("java.lang.String.indexOf:(Ljava/lang/String;)I")]
        public static int IndexOf(string s, string i) => s.IndexOf(i, StringComparison.Ordinal);

        [MethodPlug("java.lang.String.indexOf:(Ljava/lang/String;I)I")]
        public static int IndexOf(string s, string ch, int fromIndex) => s.IndexOf(ch, fromIndex, StringComparison.Ordinal);

        [MethodPlug("java.lang.String.intern:()Ljava/lang/String;")]
        public static string Intern(string str) => string.Intern(str);

        [MethodPlug("java.lang.String.isEmpty:()Z")]
        public static bool IsEmpty(string str) => str.Length == 0;

        [MethodPlug("java.lang.String.lastIndexOf:(I)I")]
        public static int LastIndexOf(string str, int ch) => str.LastIndexOf(char.ConvertFromUtf32(ch), StringComparison.Ordinal);

        [MethodPlug("java.lang.String.lastIndexOf:(II)I")]
        public static int LastIndexOf(string str, int ch, int fromIndex) => str.LastIndexOf(char.ConvertFromUtf32(ch), fromIndex, StringComparison.Ordinal);

        [MethodPlug("java.lang.String.lastIndexOf:(Ljava/lang/String;)I")]
        public static int LastIndexOf(string str, string ch) => str.LastIndexOf(ch, StringComparison.Ordinal);

        [MethodPlug("java.lang.String.lastIndexOf:(Ljava/lang/String;I)I")]
        public static int LastIndexOf(string str, string ch, int fromIndex) => str.LastIndexOf(ch, fromIndex, StringComparison.Ordinal);

        [MethodPlug("java.lang.String.length:()I")]
        public static int Length(string str) => str.Length;

        [MethodPlug("java.lang.String.matches:(Ljava/lang/String)Z")]
        public static bool Matches(string str, string regex) => Regex.IsMatch(str, regex);

        [MethodPlug("java.lang.String.replace:(CC)Ljava/lang/String")]
        public static string Replace(string str, char oldChar, char newChar) => str.Replace(oldChar, newChar);

        [MethodPlug("java.lang.String.replace:(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String")]
        public static string Replace(string str, CharSequence oldChar, CharSequence newChar) => str.Replace(oldChar.ToString(), newChar.ToString());

        [MethodPlug("java.lang.String.split:(Ljava/lang/String;)[Ljava/lang/String;")]
        public static string[] Split(string str, string regex) => Split(str, regex, 0);

        [MethodPlug("java.lang.String.split:(Ljava/lang/String;I)[Ljava/lang/String;")]
        public static string[] Split(string str, string regex, int limit)
        {
            if (limit > 0)
                return Regex.Split(str, regex);
            if (limit == 0)
                return Regex.Split(str, regex).Reverse().SkipWhile(x => x.Length == 0).Reverse().ToArray();

            var split = Regex.Split(str, regex);
            if (split.Length <= limit) return split;
            var newSplit = new string[limit];
            Array.Copy(split, newSplit, limit - 1);
            newSplit[limit - 1] = string.Join("", split.Skip(limit - 1));
            return newSplit;
        }

        [MethodPlug("java.lang.String.startsWith:(Ljava/lang/String;)Z")]
        public static bool StartsWith(string s, string prefix) => s.StartsWith(prefix, StringComparison.Ordinal);

        [MethodPlug("java.lang.String.startsWith:(Ljava/lang/String;I)Z")]
        public static bool StartsWith(string s, string prefix, int offset) => 
            offset >= 0 && offset < s.Length && s.Substring(offset).StartsWith(prefix, StringComparison.Ordinal);

        [MethodPlug("java.lang.String.subSequence:(II)Ljava/lang/CharSequence;")]
        public static CharSequence SubSequence(string str, int begin, int end) => new StringAsCharSequence(str.Substring(begin, end - begin));

        [MethodPlug("java.lang.String.substring:(I)Ljava/lang/String;")]
        public static string Substring(string str, int begin) => str.Substring(begin);

        [MethodPlug("java.lang.String.substring:(II)Ljava/lang/String;")]
        public static string Substring(string str, int begin, int end) => str.Substring(begin, end - begin);

        [MethodPlug("java.lang.String.toCharArray:()[C")]
        public static char[] ToCharArray(string s) => s.ToCharArray();

        [MethodPlug("java.lang.String.toLowerCase:()Ljava/lang/String;")]
        public static string ToLowerCase(string s) => s.ToLower();

        [MethodPlug("java.lang.String.toLowerCase:(Ljava/lang/Locale;)Ljava/lang/String;")]
        public static string ToLowerCase(string s, Locale locale) => s.ToLower(locale.Culture);

        [MethodPlug("java.lang.String.toUpperCase:()Ljava/lang/String;")]
        public static string ToUpperCase(string s) => s.ToUpper();

        [MethodPlug("java.lang.String.toUpperCase:(Ljava/lang/Locale;)Ljava/lang/String;")]
        public static string ToUpperCase(string s, Locale locale) => s.ToUpper(locale.Culture);

        [MethodPlug("java.lang.String.trim:()Ljava/lang/String;")]
        public static string Trim(string s) => s.Trim();

        [MethodPlug("java.lang.String.valueOf:(Z)Ljava/lang/String;")]
        public static string ValueOf(bool l) => l.ToString();

        [MethodPlug("java.lang.String.valueOf:(C)Ljava/lang/String;")]
        public static string ValueOf(char l) => l.ToString();

        [MethodPlug("java.lang.String.valueOf:([C)Ljava/lang/String;")]
        public static string ValueOf(char[] l) => new string(l);

        [MethodPlug("java.lang.String.valueOf:([CII)Ljava/lang/String;")]
        public static string ValueOf(char[] l, int offset, int count) => new string(l, offset, count);

        [MethodPlug("java.lang.String.valueOf:(I)Ljava/lang/String;")]
        public static string ValueOf(int l) => l.ToString();

        [MethodPlug("java.lang.String.valueOf:(F)Ljava/lang/String;")]
        public static string ValueOf(float l) => l.ToString();

        [MethodPlug("java.lang.String.valueOf:(J)Ljava/lang/String;")]
        public static string ValueOf(long l) => l.ToString();

        [MethodPlug("java.lang.String.valueOf:(D)Ljava/lang/String;")]
        public static string ValueOf(double l) => l.ToString();

        [MethodPlug("java.lang.String.valueOf:(Ljava/lang/Object;)Ljava/lang/String;")]
        public static string ValueOf(object l) => l.ToString();

    }
}
