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

        [NativeMethodImpl]
        public static void registerNatives(Type thread)
        {
            var clrThread = Thread.CurrentThread;
            _sysThreadGroup = Activator.CreateInstance(typeof(ThreadGroup), true);
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

        private static object _sysThreadGroup;


        [NativeMethodImpl]
        public static java.lang.Thread currentThread(Type thread)
        {
            lock (_javaThreads)
            {
                if (!_javaThreads.TryGetValue(Thread.CurrentThread.ManagedThreadId, out var value))
                {
                    value = (java.lang.Thread) Activator.CreateInstance(thread, _sysThreadGroup, null, "MainThread", 0L);
                    typeof(ThreadGroup)
                        .GetMethod("add", BindingFlags.Public | BindingFlags.Instance, null, CallingConventions.Any, new[] {thread}, new ParameterModifier[0])
                        .Invoke(_sysThreadGroup, new[] {value});
                }
                return value;
            }
        }

        [NativeMethodImpl]
        public static void yield(Type thread)
        {
            Thread.Yield();
        }

        [NativeMethodImpl]
        public static void sleep(Type thread, long milliseconds)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));
        }

        [NativeMethodImpl]
        public static bool isInterrupted(java.lang.Thread @this, bool clearInterrupt)
        {
            return false;
        }

        [NativeMethodImplAttribute]
        public static bool isAlive(java.lang.Thread @this)
        {
            return _nativeData[@this].ClrThread.IsAlive;
        }

        [NativeMethodImplAttribute]
        public static int countStackFrames(java.lang.Thread @this)
        {
            if (_nativeData[@this].ClrThread == Thread.CurrentThread)
            {
                return new StackTrace(1).FrameCount;
            }
            else
            {
                return _nativeData[@this].ClrThread.IsAlive ? 13 : 0; // TODO implement real logic
            }
        }

        [NativeMethodImpl]
        public static bool holdsLock(Type thread, object target)
        {
            return Monitor.IsEntered(target);
        }

        [NativeMethodImpl]
        public static java.lang.Thread[] getThreads(Type thread)
        {
            return _javaThreads.Values.ToArray();
        }

        [NativeMethodImpl]
        public static void setPriority0(java.lang.Thread @this, int priority)
        {
            var pr = (priority - 1) / 2;
            var priorities = new[] {ThreadPriority.Lowest, ThreadPriority.BelowNormal, ThreadPriority.Normal, ThreadPriority.AboveNormal, ThreadPriority.Highest};
            _nativeData[@this].ClrThread.Priority = priorities[pr];
        }

        [NativeMethodImplAttribute]
        public static void start0(java.lang.Thread @this)
        {
            _nativeData[@this].ClrThread.Start();
        }

        [NativeMethodImplAttribute]
        public static void stop0(java.lang.Thread @this, object param)
        {
            _nativeData[@this].ClrThread.Abort(param);
        }

        [NativeMethodImplAttribute]
        public static void suspend0(java.lang.Thread @this)
        {
            _nativeData[@this].ClrThread.Suspend();
        }

        [NativeMethodImplAttribute]
        public static void resume0(java.lang.Thread @this)
        {
            _nativeData[@this].ClrThread.Resume();
        }

        [NativeMethodImplAttribute]
        public static void interrupt0(java.lang.Thread @this)
        {
            _nativeData[@this].ClrThread.Interrupt();
        }

        [NativeMethodImplAttribute]
        public static void setNativeName(java.lang.Thread @this, java.lang.String name)
        {
            _nativeData[@this].ClrThread.Name = name;
        }
    }
}
