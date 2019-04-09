using System.Collections.Generic;
using System.Reflection;
using System.Text;
using java.lang.reflect;
using JavaNet.Runtime.Plugs;
using Type = System.Type;

namespace JavaNet.Runtime.Native.sun.reflect
{
    public static class NativeConstructorAccessorImplNative
    {
        public const string TypeName = "sun.reflect.NativeConstructorAccessorImpl";

        [JniExport]
        public static object newInstance0(Type thisType, Constructor ctor, object[] args)
        {
            var nt = (ConstructorInfo) (ctor.__nativeData ?? ctor.getRoot().__nativeData);
            return nt.Invoke(args);
        }
    }
}
