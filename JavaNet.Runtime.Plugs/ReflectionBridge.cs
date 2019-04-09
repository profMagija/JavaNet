using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JavaNet.Runtime.Plugs
{
    public static class ReflectionBridge
    {

        private static readonly Dictionary<Type, object> _classes = new Dictionary<Type, object>();

        public static object GetClass(Type type)
        {

            if (_classes.TryGetValue(type, out var value))
                return value;

            var cls = Type.GetType("java.lang.Class, JavaNet.Runtime", true)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single()
                .Invoke(new object[] {null});

            cls.SetField("__nativeData", type);

            _classes[type] = cls;

            return cls;
        }
    }
}