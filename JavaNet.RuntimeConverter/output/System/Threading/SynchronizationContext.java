package System.Threading;
public class SynchronizationContext {
    public final boolean IsWaitNotificationRequired() {
        throw new Exception("STUB");
    }

    public void Send(System.Threading.SendOrPostCallback d, Object state) {
        throw new Exception("STUB");
    }

    public void Post(System.Threading.SendOrPostCallback d, Object state) {
        throw new Exception("STUB");
    }

    public void OperationStarted() {
        throw new Exception("STUB");
    }

    public void OperationCompleted() {
        throw new Exception("STUB");
    }

    public int Wait(System.IntPtr[] waitHandles, boolean waitAll, int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final void SetSynchronizationContext(System.Threading.SynchronizationContext syncContext) {
        throw new Exception("STUB");
    }

    public static final System.Threading.SynchronizationContext get_Current() {
        throw new Exception("STUB");
    }

    public System.Threading.SynchronizationContext CreateCopy() {
        throw new Exception("STUB");
    }

}
