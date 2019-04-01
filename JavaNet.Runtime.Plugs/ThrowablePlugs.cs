using System;
using System.Collections.Generic;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class ThrowablePlugs
    {
        [MethodPlug(typeof(Exception), ".ctor", typeof(Exception))]
        public static Exception Ctor(Exception cause) => new Exception(cause?.Message, cause);

        [MethodPlug(typeof(Exception), "getMessage")]
        public static string GetMessage(Exception t) => t.Message;

        [MethodPlug(typeof(Exception), "getLocalizedMessage")]
        public static string GetLocalizedMessage(Exception t) => t.Message;

        [MethodPlug(typeof(Exception), "getCause")]
        public static Exception GetCause(Exception t) => t.InnerException;

        [MethodPlug(typeof(Exception), "initCause", typeof(Exception))]
        public static Exception InitCause(Exception t, Exception cause)
        {
            return t;
        }

        [MethodPlug(typeof(Exception), "printStackTrace")]
        public static void PrintStackTrace(Exception ex)
        {
            Console.Error.WriteLine(ex);
        }

        [MethodPlug(typeof(Exception), "fillInStackTrace")]
        public static Exception FillInStackTrace(Exception ex)
        {
            return ex;
        }
    }
}
