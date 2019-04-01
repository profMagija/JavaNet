using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class ObjectPlugs
    {
        [MethodPlug(typeof(object), "getClass")]
        public static Type GetClass(object o) => o.GetType();

        [MethodPlug(typeof(object), "clone")]
        public static object Clone(object o)
        {
            if (o is ICloneable cloneable) return cloneable.Clone();
            return typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(o, null);
        }
    }
}
