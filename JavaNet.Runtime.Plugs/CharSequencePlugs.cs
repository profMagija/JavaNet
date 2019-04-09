using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class CharSequencePlugs
    {
        public const string TypeName = "System.Collections.Generic.IEnumerable";

        public static char charAt(IEnumerable<char> @this, int index)
        {
            return @this.Skip(index).First();
        }

        public static int length(IEnumerable<char> @this)
        {
            return @this.Count();
        }

        public static IEnumerable<char> subSequence(IEnumerable<char> @this, int start, int end)
        {
            return @this.Skip(start).Take(end - start);
        }
    }
}
