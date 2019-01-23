package System.Threading;
public class ReaderWriterLock {
    public final boolean get_IsReaderLockHeld() {
        throw new Exception("STUB");
    }

    public final boolean get_IsWriterLockHeld() {
        throw new Exception("STUB");
    }

    public final int get_WriterSeqNum() {
        throw new Exception("STUB");
    }

    public final boolean AnyWritersSince(int seqNum) {
        throw new Exception("STUB");
    }

    public final void AcquireReaderLock(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public final void AcquireReaderLock(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public final void AcquireWriterLock(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public final void AcquireWriterLock(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public final void ReleaseReaderLock() {
        throw new Exception("STUB");
    }

    public final void ReleaseWriterLock() {
        throw new Exception("STUB");
    }

    public final System.Threading.LockCookie UpgradeToWriterLock(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public final System.Threading.LockCookie UpgradeToWriterLock(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public final void DowngradeFromWriterLock(System.Threading.LockCookie& lockCookie) {
        throw new Exception("STUB");
    }

    public final System.Threading.LockCookie ReleaseLock() {
        throw new Exception("STUB");
    }

    public final void RestoreLock(System.Threading.LockCookie& lockCookie) {
        throw new Exception("STUB");
    }

}
