package System.Threading;
public class ThreadPool {
    public static final boolean SetMaxThreads(int workerThreads, int completionPortThreads) {
        throw new Exception("STUB");
    }

    public static final void GetMaxThreads(System.Int32& workerThreads, System.Int32& completionPortThreads) {
        throw new Exception("STUB");
    }

    public static final boolean SetMinThreads(int workerThreads, int completionPortThreads) {
        throw new Exception("STUB");
    }

    public static final void GetMinThreads(System.Int32& workerThreads, System.Int32& completionPortThreads) {
        throw new Exception("STUB");
    }

    public static final void GetAvailableThreads(System.Int32& workerThreads, System.Int32& completionPortThreads) {
        throw new Exception("STUB");
    }

    public static final System.Threading.RegisteredWaitHandle RegisterWaitForSingleObject(System.Threading.WaitHandle waitObject, System.Threading.WaitOrTimerCallback callBack, Object state, System.UInt32 millisecondsTimeOutInterval, boolean executeOnlyOnce) {
        throw new Exception("STUB");
    }

    public static final System.Threading.RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(System.Threading.WaitHandle waitObject, System.Threading.WaitOrTimerCallback callBack, Object state, System.UInt32 millisecondsTimeOutInterval, boolean executeOnlyOnce) {
        throw new Exception("STUB");
    }

    public static final System.Threading.RegisteredWaitHandle RegisterWaitForSingleObject(System.Threading.WaitHandle waitObject, System.Threading.WaitOrTimerCallback callBack, Object state, int millisecondsTimeOutInterval, boolean executeOnlyOnce) {
        throw new Exception("STUB");
    }

    public static final System.Threading.RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(System.Threading.WaitHandle waitObject, System.Threading.WaitOrTimerCallback callBack, Object state, int millisecondsTimeOutInterval, boolean executeOnlyOnce) {
        throw new Exception("STUB");
    }

    public static final System.Threading.RegisteredWaitHandle RegisterWaitForSingleObject(System.Threading.WaitHandle waitObject, System.Threading.WaitOrTimerCallback callBack, Object state, long millisecondsTimeOutInterval, boolean executeOnlyOnce) {
        throw new Exception("STUB");
    }

    public static final System.Threading.RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(System.Threading.WaitHandle waitObject, System.Threading.WaitOrTimerCallback callBack, Object state, long millisecondsTimeOutInterval, boolean executeOnlyOnce) {
        throw new Exception("STUB");
    }

    public static final System.Threading.RegisteredWaitHandle RegisterWaitForSingleObject(System.Threading.WaitHandle waitObject, System.Threading.WaitOrTimerCallback callBack, Object state, System.TimeSpan timeout, boolean executeOnlyOnce) {
        throw new Exception("STUB");
    }

    public static final System.Threading.RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(System.Threading.WaitHandle waitObject, System.Threading.WaitOrTimerCallback callBack, Object state, System.TimeSpan timeout, boolean executeOnlyOnce) {
        throw new Exception("STUB");
    }

    public static final boolean QueueUserWorkItem(System.Threading.WaitCallback callBack) {
        throw new Exception("STUB");
    }

    public static final boolean QueueUserWorkItem(System.Threading.WaitCallback callBack, Object state) {
        throw new Exception("STUB");
    }

    public static final boolean UnsafeQueueUserWorkItem(System.Threading.WaitCallback callBack, Object state) {
        throw new Exception("STUB");
    }

    public static final boolean BindHandle(System.IntPtr osHandle) {
        throw new Exception("STUB");
    }

    public static final boolean BindHandle(System.Runtime.InteropServices.SafeHandle osHandle) {
        throw new Exception("STUB");
    }

}
