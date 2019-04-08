using System;
using System.Diagnostics;
using System.Reflection;

namespace JavaNet.Runtime.Plugs
{
    public static class ReflectionExtensions
    {
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
            var mi = t.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (mi == null)
                throw new MissingMethodException(t.FullName, name);
            mi.Invoke(null, args);
        }

        public static void SetField(this object o, string name, object value)
        {
            if (o == null)
                throw new NullReferenceException();
            var fieldInfo = o.GetType().GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo == null)
                throw new MissingFieldException(o.GetType().FullName, name);
            fieldInfo.SetValue(o, value);
        }

        public static T GetField<T>(this object o, string name)
        {
            if (o == null)
                throw new NullReferenceException();
            var fieldInfo = o.GetType().GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo == null)
                throw new MissingFieldException(o.GetType().FullName, name);
            return (T)fieldInfo.GetValue(o);
        }
    }

    public class FieldRef<T>
    {
        public FieldInfo Field { get; }

        public FieldRef(FieldInfo field)
        {
            Field = field;
        }

        public FieldRef(Type type, string name) : this (type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic))
        {
        }

        public T this[object o]
        {
            get => (T) Field.GetValue(o);
            set => Field.SetValue(o, value);
        }
    }
}