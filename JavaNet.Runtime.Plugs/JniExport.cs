using System;

namespace JavaNet.Runtime.Plugs
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class JniExport : Attribute
    {
        public string DeclType { get; }
        public string Name { get; }

        public JniExport()
        {
            
        }

        public JniExport(Type declType, string name) : this(declType.FullName, name)
        {
        }

        public JniExport(string declType, string name)
        {
            DeclType = declType;
            Name = name;
        }
    }
}