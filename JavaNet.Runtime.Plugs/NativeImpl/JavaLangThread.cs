using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static readonly Type _javaLangThreadGroup = Type.GetType("java.lang.ThreadGroup, JavaNet.Runtime", true);

        [NativeImpl]
        public static void registerNatives()
        {
            var clrThread = Thread.CurrentThread;
            var sysThreadGroup = Activator.CreateInstance(_javaLangThreadGroup, true);
            var mainThread = FormatterServices.GetUninitializedObject(_javaLangThread);

            lock (_javaThreads)
            {
                _javaThreads.Add(clrThread.ManagedThreadId, mainThread);
            }

            void SetField(string name, object value)
            {
                _javaLangThread.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(mainThread, value);
            }

            SetField("name", "Main Thread".ToCharArray());
            SetField("group", sysThreadGroup);
            SetField("daemon", false);
            SetField("priority", 5);
            SetField("threadStatus", 0);
            SetField("__nativeData", new Data {ClrThread = Thread.CurrentThread, JavaThread = mainThread});

            //_javaLangThread.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            //    .Single(x => x.Name == "init" && x.GetParameters().Length == 4)
            //    .Invoke(mainThread, new object[] {sysThreadGroup, null, "Main Thread", 0});
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

        [NativeImpl(IsStatic = true)]
        public static void yield()
        {
            Thread.Yield();
        }

        [NativeImpl(IsStatic = true)]
        public static void sleep(long milliseconds)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));
        }

        [NativeImpl]
        public static bool isInterrupted(object @this, bool clearInterrupt, [NativeDataParam] ref Data data)
        {
            return false;
        }

        [NativeImpl]
        public static bool isAlive(object @this, [NativeDataParam] ref Data data)
        {
            return data.ClrThread.IsAlive;
        }

        [NativeImpl]
        public static int countStackFrames(object @this, [NativeDataParam] ref Data data)
        {
            if (data.ClrThread == Thread.CurrentThread)
            {
                return new StackTrace(1).FrameCount;
            }
            else
            {
                return data.ClrThread.IsAlive ? 13 : 0; // TODO implement real logic
            }
        }

        [NativeImpl(IsStatic = true)]
        public static bool holdsLock(object target)
        {
            return Monitor.IsEntered(target);
        }

        [NativeImpl(IsStatic = true)]
        [return: ActualType("java.lang.Thread[]")]
        public static object[] getThreads()
        {
            return _javaThreads.Values.ToArray();
        }

        [NativeImpl]
        public static void setPriority0(object @this, int priority, [NativeDataParam] ref Data data)
        {
            var pr = (priority - 1) / 2;
            var priorities = new[] {ThreadPriority.Lowest, ThreadPriority.BelowNormal, ThreadPriority.Normal, ThreadPriority.AboveNormal, ThreadPriority.Highest};
            data.ClrThread.Priority = priorities[pr];
        }

        [NativeImpl]
        public static void stop0(object @this, object param, [NativeDataParam] ref Data data)
        {
            data.ClrThread.Abort(param);
        }

        [NativeImpl]
        public static void suspend0(object @this, [NativeDataParam] ref Data data)
        {
            data.ClrThread.Suspend();
        }

        [NativeImpl]
        public static void resume0(object @this, [NativeDataParam] ref Data data)
        {
            data.ClrThread.Resume();
        }

        [NativeImpl]
        public static void interrupt0(object @this, [NativeDataParam] ref Data data)
        {
            data.ClrThread.Interrupt();
        }

        [NativeImpl]
        public static void setNativeName(object @this, string name, [NativeDataParam] ref Data data)
        {
            data.ClrThread.Name = name;
        }
    }
}
