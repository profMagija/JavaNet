using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class FieldInfoPlugs
    {
        [MethodPlug(typeof(FieldInfo), "get", typeof(object))]
        public static object get(
            FieldInfo @this, object from
        )
        {
            return @this.GetValue(from);
        }

        [MethodPlug(typeof(FieldInfo), "getBoolean", typeof(object))]
        public static bool getBoolean(
            FieldInfo @this, object from
        )
        {
            return (bool)@this.GetValue(@from);
        }

        [MethodPlug(typeof(FieldInfo), "getByte", typeof(object))]
        public static sbyte getByte(
            FieldInfo @this, object from
        )
        {
            return (sbyte)@this.GetValue(@from);
        }

        [MethodPlug(typeof(FieldInfo), "getChar", typeof(object))]
        public static char getChar(
            FieldInfo @this, object from
        )
        {
            return (char)@this.GetValue(@from);
        }

        [MethodPlug(typeof(FieldInfo), "getShort", typeof(object))]
        public static short getShort(
            FieldInfo @this, object from
        )
        {
            return (short)@this.GetValue(@from);
        }

        [MethodPlug(typeof(FieldInfo), "getInt", typeof(object))]
        public static int getInt(
            FieldInfo @this, object from
        )
        {
            return (int)@this.GetValue(@from);
        }

        [MethodPlug(typeof(FieldInfo), "getLong", typeof(object))]
        public static long getLong(
            FieldInfo @this, object from
        )
        {
            return (long)@this.GetValue(@from);
        }

        [MethodPlug(typeof(FieldInfo), "getFloat", typeof(object))]
        public static float getFloat(
            FieldInfo @this, object from
        )
        {
            return (float)@this.GetValue(@from);
        }

        [MethodPlug(typeof(FieldInfo), "getDouble", typeof(object))]
        public static double getDouble(
            FieldInfo @this, object from
        )
        {
            return (double)@this.GetValue(@from);
        }

        [MethodPlug(typeof(FieldInfo), "getModifiers")]
        public static int getModifiers(FieldInfo @this)
        {
            JavaModifiers jm = (JavaModifiers) 0;
            if (@this.IsPublic)
                jm |= JavaModifiers.PUBLIC;
            else if (@this.IsPrivate)
                jm |= JavaModifiers.PRIVATE;
            else if (@this.IsFamilyOrAssembly)
                jm |= JavaModifiers.PROTECTED;

            if (@this.IsInitOnly || @this.IsLiteral)
                jm |= JavaModifiers.FINAL;

            if (@this.IsStatic)
                jm |= JavaModifiers.STATIC;

            if (@this.GetRequiredCustomModifiers().Any(t => t.FullName == typeof(IsVolatile).FullName))
                jm |= JavaModifiers.VOLATILE;

            if (@this.DeclaringType.BaseType?.FullName == "java.lang.Enum" || @this.DeclaringType.IsEnum)
                jm |= JavaModifiers.ENUM;

            if (@this.GetCustomAttributes<CompilerGeneratedAttribute>().Any())
                jm |= JavaModifiers.SYNTHETIC;

            return (int) jm;
        }

        [MethodPlug(typeof(FieldInfo), "getDeclaringClass")]
        public static Type getDeclaringClass(FieldInfo @this)
        {
            return @this.DeclaringType;
        }

        [MethodPlug(typeof(FieldInfo), "getName")]
        public static string getName(FieldInfo @this)
        {
            return @this.GetCustomAttribute<JavaNameAttribute>()?.Name ?? @this.Name;
        }

        [MethodPlug(typeof(FieldInfo), "isEnumConstant")]
        public static bool isEnumConstant(FieldInfo @this)
        {
            return ((JavaModifiers)getModifiers(@this) & JavaModifiers.ENUM) != 0;
        }

        [MethodPlug(typeof(FieldInfo), "isSynthetic")]
        public static bool isSynthetic(FieldInfo @this)
        {
            return ((JavaModifiers) getModifiers(@this) & JavaModifiers.SYNTHETIC) != 0;
        }

        [MethodPlug(typeof(FieldInfo), "getType")]
        [MethodPlug(typeof(FieldInfo), "getGenericType")]
        public static Type getType(FieldInfo @this)
        {
            return @this.FieldType;
        }

        [MethodPlug(typeof(FieldInfo), "toGenericString")]
        public static string toGenericString(FieldInfo @this)
        {
            return @this.ToString();
        }
    }
}
