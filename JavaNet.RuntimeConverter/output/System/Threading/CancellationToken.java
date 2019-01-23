package System.Threading;
public class CancellationToken {
    public static final System.Threading.CancellationToken get_None() {
        throw new Exception("STUB");
    }

    public final boolean get_IsCancellationRequested() {
        throw new Exception("STUB");
    }

    public final boolean get_CanBeCanceled() {
        throw new Exception("STUB");
    }

    public final System.Threading.WaitHandle get_WaitHandle() {
        throw new Exception("STUB");
    }

    public final System.Threading.CancellationTokenRegistration Register(System.Action callback) {
        throw new Exception("STUB");
    }

    public final System.Threading.CancellationTokenRegistration Register(System.Action callback, boolean useSynchronizationContext) {
        throw new Exception("STUB");
    }

    public final boolean Equals(System.Threading.CancellationToken other) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Threading.CancellationToken left, System.Threading.CancellationToken right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Threading.CancellationToken left, System.Threading.CancellationToken right) {
        throw new Exception("STUB");
    }

    public final void ThrowIfCancellationRequested() {
        throw new Exception("STUB");
    }

}
