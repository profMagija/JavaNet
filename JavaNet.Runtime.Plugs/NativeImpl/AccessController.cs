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

        private static bool IsChecked(Exception ex)
        {
            var t = ex.GetType();
            while (t != null)
            {
                if (t.FullName == "java.lang.Exception")
                    return true;

                t = t.BaseType;
            }

            return false;
        }

        [NativeImpl(IsStatic = true)]
        public static object doPrivileged(PrivilegedExceptionAction action)
        {
            try
            {
                return action.run();
            }
            catch (Exception ex)
            {
                if (IsChecked(ex))
                    PlugHelpers.ThrowForName("java.security.PrivilegedActionException", ex);


                throw;
            }
        }

        [NativeImpl(IsStatic = true)]
        public static object doPrivileged(
            PrivilegedExceptionAction action, 
            [ActualType("java.security.AccessControlContext")] object context)
        {
            return doPrivileged(action);
        }

        [NativeImpl(IsStatic = true)]
        public static object doPrivileged(PrivilegedAction action)
        {
            return action.run();
        }

        [NativeImpl(IsStatic = true)]
        public static object doPrivileged(
            PrivilegedAction action,
            [ActualType("java.security.AccessControlContext")] object context)
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
