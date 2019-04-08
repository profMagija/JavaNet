using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class Native
    {

        public static void RegisterNativeLibrary(Assembly asm)
        {
            foreach (var type in asm.ExportedTypes)
            {
                foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    if (methodInfo.GetCustomAttribute<NativeMethodImplAttribute>() is NativeMethodImplAttribute atr)
                    {
                        var key = atr.DeclType.FullName + ":" + atr.Name;
                        _nativeMethods.Add(key, methodInfo);
                    }
                }
            }
        }

        private static readonly Dictionary<string, MethodInfo> _nativeMethods = new Dictionary<string, MethodInfo>();

        public static object NativeMethodEntryPoint(Type type, string methodName, object[] arguments)
        {
            var key = type.FullName + ":" + methodName;
            if (_nativeMethods.TryGetValue(key, out var method))
            {
                return method.Invoke(null, arguments);
            }

            // TODO do some dynamic loader things

            throw new MissingMethodException($"No implementation for {type.FullName}::{methodName}");
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    sealed class NativeMethodImplAttribute : Attribute
    {
        public Type DeclType { get; }
        public string Name { get; }

        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public NativeMethodImplAttribute(Type declType, string name)
        {
            DeclType = declType;
            Name = name;
        }
    }
}
