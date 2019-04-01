using System;

namespace JavaNet.Runtime.Plugs
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MethodPlugAttribute : Attribute
    {
        public string Name { get; }
        public MethodPlugAttribute(string name)
        {
            Name = name;
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
}
