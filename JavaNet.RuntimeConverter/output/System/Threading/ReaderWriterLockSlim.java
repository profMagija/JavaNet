package System.Threading;
public class ReaderWriterLockSlim {
    public final void EnterReadLock() {
        throw new Exception("STUB");
    }

    public final boolean TryEnterReadLock(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public final boolean TryEnterReadLock(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public final void EnterWriteLock() {
        throw new Exception("STUB");
    }

    public final boolean TryEnterWriteLock(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public final boolean TryEnterWriteLock(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public final void EnterUpgradeableReadLock() {
        throw new Exception("STUB");
    }

    public final boolean TryEnterUpgradeableReadLock(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public final boolean TryEnterUpgradeableReadLock(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public final void ExitReadLock() {
        throw new Exception("STUB");
    }

    public final void ExitWriteLock() {
        throw new Exception("STUB");
    }

    public final void ExitUpgradeableReadLock() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final boolean get_IsReadLockHeld() {
        throw new Exception("STUB");
    }

    public final boolean get_IsUpgradeableReadLockHeld() {
        throw new Exception("STUB");
    }

    public final boolean get_IsWriteLockHeld() {
        throw new Exception("STUB");
    }

    public final System.Threading.LockRecursionPolicy get_RecursionPolicy() {
        throw new Exception("STUB");
    }

    public final int get_CurrentReadCount() {
        throw new Exception("STUB");
    }

    public final int get_RecursiveReadCount() {
        throw new Exception("STUB");
    }

    public final int get_RecursiveUpgradeCount() {
        throw new Exception("STUB");
    }

    public final int get_RecursiveWriteCount() {
        throw new Exception("STUB");
    }

    public final int get_WaitingReadCount() {
        throw new Exception("STUB");
    }

    public final int get_WaitingUpgradeCount() {
        throw new Exception("STUB");
    }

    public final int get_WaitingWriteCount() {
        throw new Exception("STUB");
    }

}
