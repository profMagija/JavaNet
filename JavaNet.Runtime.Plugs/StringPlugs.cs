using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using java.lang;
using java.util;

// ReSharper disable UnusedMember.Global

namespace JavaNet.Runtime.Plugs
{
    public class StringPlugs
    {
        [MethodPlug(typeof(string), ".ctor")]
        public static string Ctor() => new string(new char[0]);

        [MethodPlug(typeof(string), ".ctor", typeof(byte[]))]
        public static string Ctor(byte[] bytes) => Encoding.UTF8.GetString(bytes);

        [MethodPlug(typeof(string), ".ctor", typeof(byte[]), typeof(int), typeof(int))]
        public static string Ctor(byte[] bytes, int offset, int length) => Encoding.UTF8.GetString(bytes, offset, length);

        //[MethodPlug("java.lang.String.<init>([C)V")]
        //public static string Ctor(char[] chars) => new string(chars);

        [MethodPlug(typeof(string), ".ctor", typeof(int[]), typeof(int), typeof(int))]
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

        [MethodPlug(typeof(string), ".ctor", typeof(string))]
        public static string Ctor(string original) => original;

        [MethodPlug(typeof(string), ".ctor", typeof(char[]), typeof(bool))]
        public static string Ctor(char[] data, bool _) => new string(data);

        [MethodPlug(typeof(string), "charAt", typeof(int))]
        public static char CharAt(string str, int index) => str[index];

        [MethodPlug(typeof(string), "codePointAt", typeof(int))]
        public static int CodePointAt(string str, int index) => char.ConvertToUtf32(str, index);

        [MethodPlug(typeof(string), "codePointBefore", typeof(int))]
        public static int CodePointBefore(string str, int index)
        {
            if (char.IsLowSurrogate(str, index))
                index--;
            return char.ConvertToUtf32(str, index);
        }

        [MethodPlug(typeof(string), "compareTo", typeof(string))]
        public static int CompareTo(string str, string other) => string.CompareOrdinal(str, other);

        [MethodPlug(typeof(string), "compareToIgnoreCase", typeof(string))]
        public static int CompareToIgnoreCase(string str, string other) => string.Compare(str, other, StringComparison.OrdinalIgnoreCase);

        [MethodPlug(typeof(string), "concat", typeof(string))]
        public static string Concat(string str, string other) => str + other;

        [MethodPlug(typeof(string), "contains", typeof(CharSequence))]
        public static bool Contains(string str, CharSequence other) => str.Contains(other.ToString());

        [MethodPlug(typeof(string), "contentEquals", typeof(CharSequence))]
        public static bool ContentEquals(string str, CharSequence other) => str.Equals(other.ToString());

        [MethodPlug(typeof(string), "copyValueOf", typeof(char[]), IsStatic = true)]
        public static string CopyValueOf(char[] data) => new string(data);

        [MethodPlug(typeof(string), "copyValueOf", typeof(char[]), typeof(int), typeof(int), IsStatic = true)]
        public static string CopyValueOf(char[] data, int offset, int count) => new string(data, offset, count);

        [MethodPlug(typeof(string), "endsWith", typeof(string))]
        public static bool EndsWith(string str, string other) => str.EndsWith(other);

        [MethodPlug(typeof(string), "equalsIgnoreCase", typeof(string))]
        public static bool EqualsIgnoreCase(string str, string other) => str.Equals(other, StringComparison.OrdinalIgnoreCase);

        [MethodPlug(typeof(string), "getChars", typeof(char[]), typeof(int))]
        public static void GetChars(string str, char[] dest, int offset) => str.CopyTo(0, dest, offset, str.Length);

        [MethodPlug(typeof(string), "getChars", typeof(int), typeof(int), typeof(char[]), typeof(int))]
        public static void GetChars(string str, int from, int to, char[] dest, int offset) => str.CopyTo(from, dest, offset, to - from);

        [MethodPlug(typeof(string), "lastIndexOf", typeof(char[]), typeof(int), typeof(int), typeof(string), typeof(int), IsStatic = true)]
        public static int LastIndexOf(char[] source, int sourceOffset, int sourceLen, string target, int searchStart)
        {
            return new string(source, sourceOffset, sourceLen).LastIndexOf(target, searchStart, StringComparison.Ordinal);
        }

        [MethodPlug(typeof(string), "lastIndexOf", typeof(char[]), typeof(int), typeof(int), typeof(char[]), typeof(int), typeof(int), typeof(int), IsStatic = true)]
        public static int LastIndexOf(char[] source, int sourceOffset, int sourceLen, char[] target, int targetOffset, int targetLen, int startOffset)
        {
            return new string(source, sourceOffset, sourceLen).LastIndexOf(new string(target, targetOffset, targetOffset), startOffset, StringComparison.Ordinal);
        }

        [MethodPlug(typeof(string), "indexOf", typeof(char[]), typeof(int), typeof(int), typeof(string), typeof(int), IsStatic = true)]
        public static int IndexOf(char[] source, int sourceOffset, int sourceLen, string target, int searchStart)
        {
            return new string(source, sourceOffset, sourceLen).IndexOf(target, searchStart, StringComparison.Ordinal);
        }

        [MethodPlug(typeof(string), "indexOf", typeof(char[]), typeof(int), typeof(int), typeof(char[]), typeof(int), typeof(int), typeof(int), IsStatic = true)]
        public static int IndexOf(char[] source, int sourceOffset, int sourceLen, char[] target, int targetOffset, int targetLen, int startOffset)
        {
            return new string(source, sourceOffset, sourceLen).IndexOf(new string(target, targetOffset, targetOffset), startOffset, StringComparison.Ordinal);
        }

        [MethodPlug(typeof(string), "indexOf", typeof(int))]
        public static int IndexOf(string s, int i) => s.IndexOf(char.ConvertFromUtf32(i), StringComparison.Ordinal);

        [MethodPlug(typeof(string), "indexOf", typeof(int), typeof(int))]
        public static int IndexOf(string s, int ch, int fromIndex) => s.IndexOf(char.ConvertFromUtf32(ch), fromIndex, StringComparison.Ordinal);

        [MethodPlug(typeof(string), "indexOf", typeof(string))]
        public static int IndexOf(string s, string i) => s.IndexOf(i, StringComparison.Ordinal);

        [MethodPlug(typeof(string), "indexOf", typeof(string), typeof(int))]
        public static int IndexOf(string s, string ch, int fromIndex) => s.IndexOf(ch, fromIndex, StringComparison.Ordinal);

        [MethodPlug(typeof(string), "intern")]
        public static string Intern(string str) => string.Intern(str);

        [MethodPlug(typeof(string), "isEmpty")]
        public static bool IsEmpty(string str) => str.Length == 0;

        [MethodPlug(typeof(string), "lastIndexOf", typeof(int))]
        public static int LastIndexOf(string str, int ch) => str.LastIndexOf(char.ConvertFromUtf32(ch), StringComparison.Ordinal);

        [MethodPlug(typeof(string), "lastIndexOf", typeof(int), typeof(int))]
        public static int LastIndexOf(string str, int ch, int fromIndex) => str.LastIndexOf(char.ConvertFromUtf32(ch), fromIndex, StringComparison.Ordinal);

        [MethodPlug(typeof(string), "lastIndexOf", typeof(string))]
        public static int LastIndexOf(string str, string ch) => str.LastIndexOf(ch, StringComparison.Ordinal);

        [MethodPlug(typeof(string), "lastIndexOf", typeof(string), typeof(int))]
        public static int LastIndexOf(string str, string ch, int fromIndex) => str.LastIndexOf(ch, fromIndex, StringComparison.Ordinal);

        [MethodPlug(typeof(string), "length")]
        public static int Length(string str) => str.Length;

        [MethodPlug(typeof(string), "matches", typeof(string))]
        public static bool Matches(string str, string regex) => Regex.IsMatch(str, regex);

        [MethodPlug(typeof(string), "offsetByCodePoints", typeof(int), typeof(int))]
        public static int OffsetByCodePoints(string str, int index, int codePointsOffset)
        {
            for (var i = 0; i < codePointsOffset; i++)
            {
                if (char.IsSurrogatePair(str, index + i))
                {
                    index += 2;
                }
                else
                    index++;
            }

            return index;
        }

        [MethodPlug(typeof(string), "regionMatches", typeof(bool), typeof(int), typeof(string), typeof(int), typeof(int))]
        public static bool RegionMatches(string str, bool ignoreCase, int tOffset, string other, int oOffset, int length)
        {
            if (tOffset < 0 || oOffset < 0 || tOffset + length > str.Length || oOffset + length > other.Length)
                return false;

            if (ignoreCase)
            {
                str = str.ToLowerInvariant();
                other = other.ToLowerInvariant();
            }

            for (int i = 0; i < length; i++)
            {
                if (str[tOffset + i] != other[oOffset + i])
                    return false;
            }

            return true;
        }

        [MethodPlug(typeof(string), "regionMatches", typeof(int), typeof(string), typeof(int), typeof(int))]
        public static bool RegionMatches(string str, int tOffset, string other, int oOffset, int length) => RegionMatches(str, false, tOffset, other, oOffset, length);

        [MethodPlug(typeof(string), "replace", typeof(char), typeof(char))]
        public static string Replace(string str, char oldChar, char newChar) => str.Replace(oldChar, newChar);

        [MethodPlug(typeof(string), "replace", typeof(CharSequence), typeof(CharSequence))]
        public static string Replace(string str, CharSequence oldChar, CharSequence newChar) => str.Replace(oldChar.ToString(), newChar.ToString());

        [MethodPlug(typeof(string), "replaceAll", typeof(string), typeof(string))]
        public static string ReplaceAll(string str, string regex, string replacement) => Regex.Replace(str, regex, replacement);

        [MethodPlug(typeof(string), "replaceFirst", typeof(string), typeof(string))]
        public static string ReplaceFirst(string str, string regex, string replacement)
        {
            var match = Regex.Match(str, regex);

            if (match.Success)
            {
                return str.Substring(0, match.Index) + replacement + (match.Index + match.Length < str.Length ? str.Substring(match.Index + match.Length) : "");
            }

            return str;
        }

        [MethodPlug(typeof(string), "split", typeof(string))]
        public static string[] Split(string str, string regex) => Split(str, regex, 0);

        [MethodPlug(typeof(string), "split", typeof(string), typeof(int))]
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

        [MethodPlug(typeof(string), "startsWith", typeof(string))]
        public static bool StartsWith(string s, string prefix) => s.StartsWith(prefix, StringComparison.Ordinal);

        [MethodPlug(typeof(string), "startsWith", typeof(string), typeof(int))]
        public static bool StartsWith(string s, string prefix, int offset) => 
            offset >= 0 && offset < s.Length && s.Substring(offset).StartsWith(prefix, StringComparison.Ordinal);

        [MethodPlug(typeof(string), "subSequence", typeof(int), typeof(int))]
        public static CharSequence SubSequence(string str, int begin, int end) => new StringAsCharSequence(str.Substring(begin, end - begin));

        [MethodPlug(typeof(string), "substring", typeof(int))]
        public static string Substring(string str, int begin) => str.Substring(begin);

        [MethodPlug(typeof(string), "substring", typeof(int), typeof(int))]
        public static string Substring(string str, int begin, int end) => str.Substring(begin, end - begin);

        [MethodPlug(typeof(string), "toCharArray")]
        public static char[] ToCharArray(string s) => s.ToCharArray();

        [MethodPlug(typeof(string), "toLowerCase")]
        public static string ToLowerCase(string s) => s.ToLower();

        [MethodPlug(typeof(string), "toLowerCase", typeof(Locale))]
        public static string ToLowerCase(string s, Locale locale) => s.ToLower(locale.Culture);

        [MethodPlug(typeof(string), "toUpperCase")]
        public static string ToUpperCase(string s) => s.ToUpper();

        [MethodPlug(typeof(string), "toUpperCase", typeof(Locale))]
        public static string ToUpperCase(string s, Locale locale) => s.ToUpper(locale.Culture);

        [MethodPlug(typeof(string), "trim")]
        public static string Trim(string s) => s.Trim();

        [MethodPlug(typeof(string), "valueOf", typeof(bool), IsStatic = true)]
        public static string ValueOf(bool l) => l.ToString();

        [MethodPlug(typeof(string), "valueOf", typeof(char), IsStatic = true)]
        public static string ValueOf(char l) => l.ToString();

        [MethodPlug(typeof(string), "valueOf", typeof(char[]), IsStatic = true)]
        public static string ValueOf(char[] l) => new string(l);

        [MethodPlug(typeof(string), "valueOf", typeof(char[]), typeof(int), typeof(int), IsStatic = true)]
        public static string ValueOf(char[] l, int offset, int count) => new string(l, offset, count);

        [MethodPlug(typeof(string), "valueOf", typeof(int), IsStatic = true)]
        public static string ValueOf(int l) => l.ToString();

        [MethodPlug(typeof(string), "valueOf", typeof(float), IsStatic = true)]
        public static string ValueOf(float l) => l.ToString(CultureInfo.InvariantCulture);

        [MethodPlug(typeof(string), "valueOf", typeof(long), IsStatic = true)]
        public static string ValueOf(long l) => l.ToString();

        [MethodPlug(typeof(string), "valueOf", typeof(double), IsStatic = true)]
        public static string ValueOf(double l) => l.ToString(CultureInfo.InvariantCulture);

        [MethodPlug(typeof(string), "valueOf", typeof(object), IsStatic = true)]
        public static string ValueOf(object l) => l.ToString();

    }
}
