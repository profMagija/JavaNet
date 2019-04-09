using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace JavaNet.Runtime.Plugs
{
    public static class ObjectPlugs
    {
        [MethodPlug(typeof(object), "getClass")]
        [return: ActualType("java.lang.Class")]
        public static object getClass(object o) => o.GetType();

        [MethodPlug(typeof(object), "clone")]
        public static object Clone(object o)
        {
            if (o is ICloneable cloneable) return cloneable.Clone();
            return typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(o, null);
        }

        [MethodPlug(typeof(object), "notify")]
        public static void Notify(object t) => Monitor.Pulse(t);

        [MethodPlug(typeof(object), "notifyAll")]
        public static void NotifyAll(object t) => Monitor.PulseAll(t);

        [MethodPlug(typeof(object), "wait", typeof(long), typeof(int))]
        public static void Wait(object t, long timeout, int nanoseconds) =>
            Monitor.Wait(t, TimeSpan.FromMilliseconds(timeout + nanoseconds / 1000000.0));

        [MethodPlug(typeof(object), "wait", typeof(long))]
        public static void Wait(object t, long timeout) => Monitor.Wait(t, TimeSpan.FromMilliseconds(timeout));

        [MethodPlug(typeof(object), "wait")]
        public static void Wait(object t) => Monitor.Wait(t);

        [MethodPlug(typeof(object), "hashCode")]
        public static int hashCode(object @this)
        {
            if (@this is string s)
                return StringPlugs.hashCode(s);

            return @this.GetHashCode();
        }


    }
}
