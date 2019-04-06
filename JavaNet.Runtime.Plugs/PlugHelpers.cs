using System;
using System.Reflection;

namespace JavaNet.Runtime.Plugs
{
    public static class PlugHelpers
    {
        public static Exception ThrowForName(string excName)
        {
            var type = ClassPlugs.ForName(excName);
            throw (Exception) Activator.CreateInstance(type);
        }

        public static Exception ThrowForName(string excName, Exception inner)
        {
            var type = ClassPlugs.ForName(excName);
            throw (Exception) Activator.CreateInstance(type, inner);
        }

        public static dynamic NewForName(string typeName, params object[] args)
        {
            return Activator.CreateInstance(ClassPlugs.ForName(typeName), args);
        }

        public static dynamic GetStaticField(Type type, string fieldName)
        {
            return type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
        }
    }
}
