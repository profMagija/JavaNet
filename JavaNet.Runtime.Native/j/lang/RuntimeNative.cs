using System;
using System.Diagnostics;
using JavaNet.Runtime.Plugs;

namespace JavaNet.Runtime.Native.j.lang
{
    public static class RuntimeNative
    {
        public const string TypeName = "java.lang.Runtime";

        [JniExport]
        public static int availableProcessors(java.lang.Runtime @this)
        {
            return Environment.ProcessorCount;
        }

        [JniExport]
        public static long freeMemory(java.lang.Runtime @this)
        {
            return Process.GetCurrentProcess().MaxWorkingSet.ToInt64() - Environment.WorkingSet;
        }

        [JniExport]
        public static long totalMemory(java.lang.Runtime @this)
        {
            return 16L << 30; // TODO implement
        }

        [JniExport]
        public static long maxMemory(java.lang.Runtime @this)
        {
            return Process.GetCurrentProcess().MaxWorkingSet.ToInt64();
        }

        [JniExport]
        public static void gc(java.lang.Runtime @this)
        {
            GC.Collect();
        }

        [JniExport]
        public static void runFinalization0(Type runtime)
        {
            GC.WaitForPendingFinalizers();
        }

        [JniExport]
        public static void traceInstructions(java.lang.Runtime @this, bool doTrace)
        {

        }

        [JniExport]
        public static void traceMethodCalls(java.lang.Runtime @this, bool doTrace)
        {

        }
    }
}
