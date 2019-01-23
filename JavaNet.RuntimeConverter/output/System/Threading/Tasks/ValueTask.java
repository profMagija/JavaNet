package System.Threading.Tasks;
public class ValueTask {
    public boolean Equals(System.Threading.Tasks.ValueTask other) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Threading.Tasks.ValueTask left, System.Threading.Tasks.ValueTask right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Threading.Tasks.ValueTask left, System.Threading.Tasks.ValueTask right) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task AsTask() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.ValueTask Preserve() {
        throw new Exception("STUB");
    }

    public final boolean get_IsCompleted() {
        throw new Exception("STUB");
    }

    public final boolean get_IsCompletedSuccessfully() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFaulted() {
        throw new Exception("STUB");
    }

    public final boolean get_IsCanceled() {
        throw new Exception("STUB");
    }

    public final System.Runtime.CompilerServices.ValueTaskAwaiter GetAwaiter() {
        throw new Exception("STUB");
    }

    public final System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable ConfigureAwait(boolean continueOnCapturedContext) {
        throw new Exception("STUB");
    }

}
