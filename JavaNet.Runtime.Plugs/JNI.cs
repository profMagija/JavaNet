using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class JNI
    {

        public static void RegisterNativeLibrary(Assembly asm)
        {
            foreach (var type in asm.ExportedTypes)
            {
                foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    if (methodInfo.GetCustomAttribute<NativeMethodImplAttribute>() is NativeMethodImplAttribute atr)
                    {
                        var typeName = atr.DeclType ?? type.GetStatic<string>("TypeName");
                        var methodName = atr.Name ?? methodInfo.Name;
                        var key = typeName + ":" + methodName;
                        _nativeMethods.Add(key, methodInfo);
                    }
                }
            }
        }

        public static void RegisterNativeMethod(Type clazz, string name, Delegate func)
        {
            clazz.SetStatic("nativeMethods<" + name + ">", func);
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
}
