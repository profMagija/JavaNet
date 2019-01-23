package System.Threading;
public class WaitHandle {
    public static final int WaitTimeout = 258;

    public System.IntPtr get_Handle() {
        throw new Exception("STUB");
    }

    public void set_Handle(System.IntPtr value) {
        throw new Exception("STUB");
    }

    public boolean WaitOne(int millisecondsTimeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public boolean WaitOne(System.TimeSpan timeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public boolean WaitOne() {
        throw new Exception("STUB");
    }

    public boolean WaitOne(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public boolean WaitOne(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public void Close() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final Microsoft.Win32.SafeHandles.SafeWaitHandle get_SafeWaitHandle() {
        throw new Exception("STUB");
    }

    public final void set_SafeWaitHandle(Microsoft.Win32.SafeHandles.SafeWaitHandle value) {
        throw new Exception("STUB");
    }

    public static final boolean WaitAll(System.Threading.WaitHandle[] waitHandles, int millisecondsTimeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public static final boolean WaitAll(System.Threading.WaitHandle[] waitHandles, System.TimeSpan timeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public static final boolean WaitAll(System.Threading.WaitHandle[] waitHandles) {
        throw new Exception("STUB");
    }

    public static final boolean WaitAll(System.Threading.WaitHandle[] waitHandles, int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final boolean WaitAll(System.Threading.WaitHandle[] waitHandles, System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.WaitHandle[] waitHandles, int millisecondsTimeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.WaitHandle[] waitHandles, System.TimeSpan timeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.WaitHandle[] waitHandles, System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.WaitHandle[] waitHandles) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.WaitHandle[] waitHandles, int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final boolean SignalAndWait(System.Threading.WaitHandle toSignal, System.Threading.WaitHandle toWaitOn) {
        throw new Exception("STUB");
    }

    public static final boolean SignalAndWait(System.Threading.WaitHandle toSignal, System.Threading.WaitHandle toWaitOn, System.TimeSpan timeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public static final boolean SignalAndWait(System.Threading.WaitHandle toSignal, System.Threading.WaitHandle toWaitOn, int millisecondsTimeout, boolean exitContext) {
        throw new Exception("STUB");
    }

}
