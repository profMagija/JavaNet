using System;
using System.Collections.Generic;
using System.Text;
using java.security;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class AccessController
    {
        public const string TypeName = "java.security.AccessController";

        [NativeImpl(IsStatic = true)]
        public static object doPrivileged(PrivilegedExceptionAction action)
        {
            return action.run();
        }

        [NativeImpl(IsStatic = true)]
        public static object doPrivileged(PrivilegedAction action)
        {
            return action.run();
        }

        [NativeImpl(IsStatic = true)]
        [return: ActualType("java.security.AccessControlContext")]
        public static object getStackAccessControlContext()
        {
            return null;
        }
    }
}

namespace java.security
{
    [JavaName("java/security/PrivilegedExceptionAction"), TypePlug]
    public interface PrivilegedExceptionAction
    {
        object run();
    }

    [JavaName("java/security/PrivilegedAction"), TypePlug]
    public interface PrivilegedAction
    {
        object run();
    }
}
