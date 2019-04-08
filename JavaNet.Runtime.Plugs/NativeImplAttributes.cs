using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class VolatileFieldsAttribute : Attribute
    {
        public string Name { get; }

        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public VolatileFieldsAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ModuleLoadHookAttribute : Attribute
    {
        public ModuleLoadHookAttribute()
        {
        }
    }
}
