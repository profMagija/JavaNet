using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    public static class JavaLangRuntime
    {
        public const string TypeName = "java.lang.Runtime";

        [NativeImpl]
        public static int availableProcessors(object @this)
        {
            return Environment.ProcessorCount;
        }

        [NativeImpl]
        public static long freeMemory(object @this)
        {
            return Process.GetCurrentProcess().MaxWorkingSet.ToInt64() - Environment.WorkingSet;
        }

        [NativeImpl]
        public static long totalMemory(object @this)
        {
            return 16L << 30; // TODO implement
        }

        [NativeImpl]
        public static long maxMemory(object @this)
        {
            return Process.GetCurrentProcess().MaxWorkingSet.ToInt64();
        }

        [NativeImpl]
        public static void gc(object @this)
        {
            GC.Collect();
        }

        [NativeImpl(IsStatic = true)]
        public static void runFinalization0()
        {
            GC.WaitForPendingFinalizers();
        }

        [NativeImpl, NativeImpl(MethodName = "traceMethodCalls")]
        public static void traceInstructions(object @this, bool doTrace)
        {

        }
    }
}
