using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace JavaNet.Runtime.Plugs
{
    public static class ReflectionExtensions
    {
        public static Type TypeOf(this string name)
        {
            return Type.GetType(name + ", JavaNet.Runtime", true);
        }
        public static void SetStatic(this Type t, string name, object value)
        {
            var fieldInfo = t.GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo == null)
                throw new MissingFieldException(t.FullName, name);
            fieldInfo.SetValue(null, value);
        }

        public static T GetStatic<T>(this Type t, string name)
        {
            var fieldInfo = t.GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo == null)
                throw new MissingFieldException(t.FullName, name);
            return (T) fieldInfo.GetValue(null);
        }

        public static void CallStatic(this Type t, string name, params object[] args)
        {
            CallStatic<object>(t, name, args);
        }

        public static T CallStatic<T>(this Type t, string name, params object[] args)
        {
            var mi = t.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (mi == null)
                throw new MissingMethodException(t.FullName, name);
            return (T) mi.Invoke(null, args);
        }

        public static void SetField(this object o, string name, object value)
        {
            if (o == null)
                throw new NullReferenceException();
            var fieldInfo = o.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo == null)
                throw new MissingFieldException(o.GetType().FullName, name);
            fieldInfo.SetValue(o, value);
        }

        public static T GetField<T>(this object o, string name)
        {
            if (o == null)
                throw new NullReferenceException();
            var fieldInfo = o.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo == null)
                throw new MissingFieldException(o.GetType().FullName, name);
            return (T)fieldInfo.GetValue(o);
        }

        public static void CallInstance(this object o, string name, params object[] args)
        {
            CallInstance<object>(o, name, args);
        }

        public static T CallInstance<T>(this object o, string name, params object[] args)
        {
            if (o == null)
                throw new NullReferenceException();
            var t = o.GetType();
            var mi = t.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (mi == null)
                throw new MissingMethodException(t.FullName, name);
            return (T) mi.Invoke(o, args);
        }

        public static T New<T>(params object[] args)
        {
            return (T) Activator.CreateInstance(typeof(T), BindingFlags.Public | BindingFlags.NonPublic, null, args, CultureInfo.InvariantCulture);
        }
    }

    public class FieldRef<T>
    {
        public FieldInfo Field { get; }

        public FieldRef(FieldInfo field)
        {
            Field = field;
        }

        public FieldRef(Type type, string name) : this (type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
        {
        }

        public T this[object o]
        {
            get => (T) Field.GetValue(o);
            set => Field.SetValue(o, value);
        }
    }

    public class MethodRef<TD> where TD : Delegate
    {
        public MethodInfo Method { get; }

        public MethodRef(MethodInfo method)
        {
            Method = method;
        }

        public MethodRef(Type t, string name)
        {
            Type[] ta;
            if (typeof(TD).Name.StartsWith("Func"))
            {
                ta = typeof(TD).GenericTypeArguments;
                ta = ta.Take(ta.Length - 1).ToArray();
            }
            else
            {
                ta = typeof(TD).GenericTypeArguments;
            }

            Method = t.GetMethod(name,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance,
                null, ta, new ParameterModifier[0]);

            if (Method == null)
                throw new MissingMethodException(t.FullName, name);
        }

        public TD this[object o]
        {
            get => (TD) Delegate.CreateDelegate(typeof(TD), o, Method);
        }
    }
}