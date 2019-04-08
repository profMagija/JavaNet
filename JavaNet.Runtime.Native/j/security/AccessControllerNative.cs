using System;
using java.security;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.security
{
    public static class AccessControllerNative
    {
        public const string TypeName = "java.security.AccessController";

        [NativeMethodImpl]
        public static object doPrivileged(Type accessController, PrivilegedExceptionAction action)
        {
            try
            {
                return action.run();
            }
            catch (global::java.lang.Exception ex)
            {
                throw new PrivilegedActionException(ex);
            }
        }

        [NativeMethodImpl]
        public static object doPrivileged(Type accessController, PrivilegedExceptionAction action, AccessControlContext context)
        {
            return doPrivileged(accessController, action);
        }

        [NativeMethodImpl]
        public static object doPrivileged(Type accessController, PrivilegedAction action)
        {
            return action.run();
        }

        [NativeMethodImpl]
        public static object doPrivileged(Type accessController, PrivilegedAction action, AccessControlContext context)
        {
            return doPrivileged(accessController, action);
        }

        [NativeMethodImpl]
        public static object getStackAccessControlContext(Type accessController)
        {
            return null;
        }
    }
}
