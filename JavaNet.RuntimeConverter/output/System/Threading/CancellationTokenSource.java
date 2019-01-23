package System.Threading;
public class CancellationTokenSource {
    public final boolean get_IsCancellationRequested() {
        throw new Exception("STUB");
    }

    public final System.Threading.CancellationToken get_Token() {
        throw new Exception("STUB");
    }

    public final void Cancel() {
        throw new Exception("STUB");
    }

    public final void Cancel(boolean throwOnFirstException) {
        throw new Exception("STUB");
    }

    public final void CancelAfter(System.TimeSpan delay) {
        throw new Exception("STUB");
    }

    public final void CancelAfter(int millisecondsDelay) {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public static final System.Threading.CancellationTokenSource CreateLinkedTokenSource(System.Threading.CancellationToken token1, System.Threading.CancellationToken token2) {
        throw new Exception("STUB");
    }

    public static final System.Threading.CancellationTokenSource CreateLinkedTokenSource(System.Threading.CancellationToken[] tokens) {
        throw new Exception("STUB");
    }

}
