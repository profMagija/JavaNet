using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class ConstructorPlugs
    {
        public const string TypeName = "System.Reflection.ConstructorInfo";

        [MethodPlug]
        public static object newInstance(ConstructorInfo @this, object[] args) => @this.Invoke(args);

        [MethodPlug]
        public static string getName(ConstructorInfo @this) => @this.DeclaringType?.Name ?? "???";

        [MethodPlug]
        public static int getParameterCount(ConstructorInfo @this) => @this.GetParameters().Length;

        [MethodPlug]
        public static Type[] getParameterTypes(ConstructorInfo @this) => @this.GetParameters().Select(x => x.ParameterType).ToArray();

        [MethodPlug]
        public static bool isVarArgs(ConstructorInfo @this) => @this.GetParameters().Any(b => b.CustomAttributes.Any(ca => ca.AttributeType == typeof(ParamArrayAttribute)));

        [MethodPlug]
        public static bool isSynthetic(ConstructorInfo @this) => @this.GetCustomAttributes<CompilerGeneratedAttribute>().Any();
    }
}
