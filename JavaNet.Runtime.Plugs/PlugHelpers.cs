using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class PlugHelpers
    {
        public static Exception ThrowForName(string excName)
        {
            throw (Exception) Activator.CreateInstance(Type.GetType(excName) ?? throw new TypeLoadException("Could not find exception type " + excName));
        }
    }
}
