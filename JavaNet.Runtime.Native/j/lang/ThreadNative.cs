using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using java.lang;
using JavaNet.Runtime.Plugs;
using Thread = System.Threading.Thread;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class ThreadNative
    {
        public const string TypeName = "java.lang.Thread";

        private static volatile Dictionary<int, java.lang.Thread> _javaThreads = new Dictionary<int, java.lang.Thread>();

        public class Data
        {
            internal Thread ClrThread;
            internal Action Run;
            internal java.lang.Thread JavaThread;

            public void Start()
            {
                var id = Thread.CurrentThread.ManagedThreadId;

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

        private static FieldRef<Data> _nativeData;
        private static MethodRef<Action> _run;

        private static ThreadGroup _sysThreadGroup;

        [JniExport]
        public static void registerNatives(Type thread)
        {
            _nativeData = new FieldRef<Data>(thread, "__nativeData");
            _run = new MethodRef<Action>(thread, "run");

            var clrThread = Thread.CurrentThread;
            _sysThreadGroup = (ThreadGroup) Activator.CreateInstance(typeof(ThreadGroup), true);
            var mainThread = (java.lang.Thread) FormatterServices.GetUninitializedObject(thread);

            //_isIniting = true;
            //var mainThread = Activator.CreateInstance(_javaLangThread, _sysThreadGroup, null, "MainThread", 0L);

            lock (_javaThreads)
            {
                _javaThreads.Add(clrThread.ManagedThreadId, mainThread);
            }
            //_isIniting = false;

            mainThread.SetField("name", "Main Thread".ToCharArray());
            mainThread.SetField("group", _sysThreadGroup);
            mainThread.SetField("daemon", false);
            mainThread.SetField("priority", 5);
            mainThread.SetField("threadStatus", 0);
            _nativeData[mainThread] = new Data {ClrThread = Thread.CurrentThread, JavaThread = mainThread};

            //_javaLangThread.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            //    .Single(x => x.Name == "init" && x.GetParameters().Length == 4)
            //    .Invoke(mainThread, new object[] { _sysThreadGroup, null, "Main Thread", 0L });


        }

        private static java.lang.Thread CreateJavaThread(Thread netThread)
        {
            var value = (java.lang.Thread)Activator.CreateInstance(typeof(java.lang.Thread), _sysThreadGroup, null, netThread.Name ?? "Some .NET thread", 0L);
            _nativeData[value] = new Data {ClrThread = netThread, JavaThread = value, Run = null};
            value.SetField("threadStatus", 1);
            value.SetField("priority", (int) netThread.Priority * 2 + 1);
            _sysThreadGroup.CallInstance("add", value);
            return value;
        }

        [JniExport]
        public static java.lang.Thread currentThread(Type thread)
        {
            lock (_javaThreads)
            {
                if (!_javaThreads.TryGetValue(Thread.CurrentThread.ManagedThreadId, out var value))
                    value = CreateJavaThread(Thread.CurrentThread);
                return value;
            }
        }

        [JniExport]
        public static void yield(Type thread)
        {
            Thread.Yield();
        }

        [JniExport]
        public static void sleep(Type thread, long milliseconds)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));
        }

        [JniExport]
        public static bool isInterrupted(java.lang.Thread @this, bool clearInterrupt)
        {
            return false;
        }

        [JniExport]
        public static bool isAlive(java.lang.Thread @this)
        {
            return GetData(@this).ClrThread.IsAlive;
        }

        [JniExport]
        public static int countStackFrames(java.lang.Thread @this)
        {
            if (_nativeData[@this]?.ClrThread == Thread.CurrentThread)
            {
                return new StackTrace(1).FrameCount;
            }

            return GetData(@this).ClrThread.IsAlive ? 13 : 0; // TODO implement real logic
        }

        [JniExport]
        public static bool holdsLock(Type thread, object target)
        {
            return Monitor.IsEntered(target);
        }

        [JniExport]
        public static java.lang.Thread[] getThreads(Type thread)
        {
            return _javaThreads.Values.ToArray();
        }

        [JniExport]
        public static void setPriority0(java.lang.Thread @this, int priority)
        {
            var pr = (priority - 1) / 2;
            var priorities = new[] {ThreadPriority.Lowest, ThreadPriority.BelowNormal, ThreadPriority.Normal, ThreadPriority.AboveNormal, ThreadPriority.Highest};
            GetData(@this).ClrThread.Priority = priorities[pr];
        }

        [JniExport]
        public static void start0(java.lang.Thread @this)
        {
            GetData(@this).ClrThread.Start();
        }

        [JniExport]
        public static void stop0(java.lang.Thread @this, object param)
        {
            GetData(@this).ClrThread.Abort(param);
        }

        [JniExport]
        public static void suspend0(java.lang.Thread @this)
        {
            GetData(@this).ClrThread.Suspend();
        }

        [JniExport]
        public static void resume0(java.lang.Thread @this)
        {
            GetData(@this).ClrThread.Resume();
        }

        [JniExport]
        public static void interrupt0(java.lang.Thread @this)
        {
            GetData(@this).ClrThread.Interrupt();
        }

        [JniExport]
        public static void setNativeName(java.lang.Thread @this, string name)
        {
            GetData(@this).ClrThread.Name = name;
        }

        private static Data GetData(java.lang.Thread @this)
        {
            if (_nativeData[@this] != null)
                return _nativeData[@this];

            var data = new Data
            {
                JavaThread = @this,
                Run = _run[@this]
            };
            data.ClrThread = new Thread(data.Start);

            _nativeData[@this] = data;

            return data;
        }
    }
}
