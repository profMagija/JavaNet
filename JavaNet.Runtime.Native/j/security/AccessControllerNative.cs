using System;
using java.security;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.security
{
    public static class AccessControllerNative
    {
        public const string TypeName = "java.security.AccessController";

        [JniExport(TypeName, "doPrivilegedLjava/security/PrivilegedExceptionAction;")]
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

        [JniExport(TypeName, "doPrivilegedLjava/security/PrivilegedExceptionAction;Ljava/security/AccessControlContext;")]
        public static object doPrivileged(Type accessController, PrivilegedExceptionAction action, AccessControlContext context)
        {
            return doPrivileged(accessController, action);
        }

        [JniExport(TypeName, "doPrivilegedLjava/security/PrivilegedAction;")]
        public static object doPrivileged(Type accessController, PrivilegedAction action)
        {
            return action.run();
        }

        [JniExport(TypeName, "doPrivilegedLjava/security/PrivilegedAction;Ljava/security/AccessControlContext;")]
        public static object doPrivileged(Type accessController, PrivilegedAction action, AccessControlContext context)
        {
            return doPrivileged(accessController, action);
        }

        [JniExport]
        public static object getStackAccessControlContext(Type accessController)
        {
            return null;
        }
    }
}
