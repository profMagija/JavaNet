using System;
using System.Diagnostics;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class RuntimeNative
    {
        public const string TypeName = "java.lang.Runtime";

        [NativeMethodImpl]
        public static int availableProcessors(java.lang.Runtime @this)
        {
            return Environment.ProcessorCount;
        }

        [NativeMethodImpl]
        public static long freeMemory(java.lang.Runtime @this)
        {
            return Process.GetCurrentProcess().MaxWorkingSet.ToInt64() - Environment.WorkingSet;
        }

        [NativeMethodImpl]
        public static long totalMemory(java.lang.Runtime @this)
        {
            return 16L << 30; // TODO implement
        }

        [NativeMethodImpl]
        public static long maxMemory(java.lang.Runtime @this)
        {
            return Process.GetCurrentProcess().MaxWorkingSet.ToInt64();
        }

        [NativeMethodImpl]
        public static void gc(java.lang.Runtime @this)
        {
            GC.Collect();
        }

        [NativeMethodImpl]
        public static void runFinalization0(Type runtime)
        {
            GC.WaitForPendingFinalizers();
        }

        [NativeMethodImpl]
        public static void traceInstructions(java.lang.Runtime @this, bool doTrace)
        {

        }

        [NativeMethodImpl]
        public static void traceMethodCalls(java.lang.Runtime @this, bool doTrace)
        {

        }
    }
}
