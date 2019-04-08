using System;

namespace JavaNet.Runtime.Plugs
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class NativeMethodImplAttribute : Attribute
    {
        public string DeclType { get; }
        public string Name { get; }

        public NativeMethodImplAttribute()
        {
            
        }

        public NativeMethodImplAttribute(Type declType, string name) : this(declType.FullName, name)
        {
        }

        public NativeMethodImplAttribute(string declType, string name)
        {
            DeclType = declType;
            Name = name;
        }
    }
}