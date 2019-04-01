using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class EnumPlugs
    {
        [MethodPlug("System.Object", "java.lang.Enum", "valueOf", "System.Type", "System.String", IsStatic = true)]
        public static object ValueOf(Type enumType, string constName)
        {
            if (constName == null) throw new NullReferenceException("Name is null");
            return enumType.GetField(constName) ?? throw new ArgumentException($"No enum constant {enumType.Name}.{constName}");
        }
    }
}
