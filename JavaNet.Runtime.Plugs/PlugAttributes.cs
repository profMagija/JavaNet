using System;
using System.Linq;

namespace JavaNet.Runtime.Plugs
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MethodPlugAttribute : Attribute
    {
        public bool IsStatic { get; set; } = false;
        public string DeclaringType { get; }
        public string MethodName { get; }
        public string[] ParamTypes { get; }
        public string ReturnType { get; set; }

        public MethodPlugAttribute(Type declaringType, string methodName, params Type[] paramTypes)
        {
            DeclaringType = declaringType.FullName;
            MethodName = methodName;
            ParamTypes = paramTypes.Select(x => x.FullName).ToArray();
        }

        public MethodPlugAttribute(string returnType, string declaringType, string methodName, params string[] paramTypes)
        {
            ReturnType = returnType;
            DeclaringType = declaringType;
            MethodName = methodName;
            ParamTypes = paramTypes;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public sealed class TypePlugAttribute : Attribute
    {
        public string Name { get; }
        public TypePlugAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class CastPlugAttribute : Attribute
    {
        public Type TargetType { get; }
        public CastPlugAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class InstanceOfPlugAttribute : Attribute
    {
        public Type TargetType { get; }
        public InstanceOfPlugAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }

    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class ActualTypeAttribute : Attribute
    {
        public string TypeName { get; }

        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public ActualTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}
