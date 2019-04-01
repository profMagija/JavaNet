using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class FieldPlugs
    {
        [MethodPlug(typeof(FieldInfo), "getName")]
        public static string GetName(FieldInfo fi) => fi.Name;
    }
}
