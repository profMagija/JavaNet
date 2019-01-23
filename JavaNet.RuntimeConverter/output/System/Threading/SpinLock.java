package System.Threading;
public class SpinLock {
    public final void Enter(System.Boolean& lockTaken) {
        throw new Exception("STUB");
    }

    public final void TryEnter(System.Boolean& lockTaken) {
        throw new Exception("STUB");
    }

    public final void TryEnter(System.TimeSpan timeout, System.Boolean& lockTaken) {
        throw new Exception("STUB");
    }

    public final void TryEnter(int millisecondsTimeout, System.Boolean& lockTaken) {
        throw new Exception("STUB");
    }

    public final void Exit() {
        throw new Exception("STUB");
    }

    public final void Exit(boolean useMemoryBarrier) {
        throw new Exception("STUB");
    }

    public final boolean get_IsHeld() {
        throw new Exception("STUB");
    }

    public final boolean get_IsHeldByCurrentThread() {
        throw new Exception("STUB");
    }

    public final boolean get_IsThreadOwnerTrackingEnabled() {
        throw new Exception("STUB");
    }

}
