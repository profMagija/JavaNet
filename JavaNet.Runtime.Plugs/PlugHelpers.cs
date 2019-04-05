using System;

namespace JavaNet.Runtime.Plugs
{
    public static class PlugHelpers
    {
        public static Exception ThrowForName(string excName)
        {
            var type = Type.GetType(excName) ?? throw new TypeLoadException("Could not find exception type " + excName);
            throw (Exception) Activator.CreateInstance(type);
        }

        public static Exception ThrowForName(string excName, Exception inner)
        {
            var type = Type.GetType(excName) ?? throw new TypeLoadException("Could not find exception type " + excName);
            throw (Exception) Activator.CreateInstance(type, inner);
        }


    }
}
