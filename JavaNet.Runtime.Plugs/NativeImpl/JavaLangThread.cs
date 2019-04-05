using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaLangThread
    {
        public const string TypeName = "java.lang.Thread";

        private static Dictionary<int, object> _javaThreads = new Dictionary<int, object>();

        [NativeData(TypeName)]
        public class Data
        {
            internal Thread ClrThread;
            internal Action Run;
            internal object JavaThread;

            public void Start()
            {
                var id = ClrThread.ManagedThreadId;
                lock (_javaThreads)
                {
                    _javaThreads.Add(id, JavaThread);
                }

                Run();

                lock (_javaThreads)
                {
                    _javaThreads.Remove(id);
                }
            }
        }

        private static readonly Type _javaLangThread = Type.GetType("java.lang.Thread, JavaNet.Runtime", true);

        [NativeImpl]
        public static void registerNatives()
        {
            var clrThread = Thread.CurrentThread;
            var sysThreadGroup = Activator.CreateInstance(Type.GetType("java.lang.ThreadGroup, JavaNet.Runtime"), true);
            var mainThread = FormatterServices.GetUninitializedObject(_javaLangThread);

            lock (_javaThreads)
            {
                _javaThreads.Add(clrThread.ManagedThreadId, mainThread);
            }

            _javaLangThread.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "init" && x.GetParameters().Length == 4)
                .Invoke(mainThread, new[] {sysThreadGroup, null, "Main Thread", 0L});
        }

        [Hook]
        public static void init(
            object @this,
            [ActualType("java.lang.ThreadGroup")] object threadGroup, 
            [ActualType("java.lang.Runnable")] object runnable,
            string name,
            long stackSize,
            [ActualType("java.security.AccessControlContext")] object acc,
            [NativeDataParam] ref Data data,
            [MethodPtr(false, "System.Void", "run")] Action run
        )
        {
            data = new Data {JavaThread = @this, Run = run};
            data.ClrThread = new Thread(data.Start, (int) stackSize);
        }

        [NativeImpl(IsStatic = true)]
        [return: ActualType(TypeName)]
        public static object currentThread()
        {
            lock (_javaThreads)
            {
                _javaThreads.TryGetValue(Thread.CurrentThread.ManagedThreadId, out var value);
                return value;
            }
        }
    }
}
