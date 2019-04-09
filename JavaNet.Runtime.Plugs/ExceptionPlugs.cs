using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class ExceptionPlugs
    {
        public const string TypeName = "System.Exception";

        [MethodPlug]
        public static Exception initCause(Exception @this, Exception cause)
        {
            @this.SetField("_innerException", cause);
            return @this;
        }


    }
}
