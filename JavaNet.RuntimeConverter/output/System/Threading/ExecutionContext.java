package System.Threading;
public class ExecutionContext {
    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {
        throw new Exception("STUB");
    }

    public static final System.Threading.ExecutionContext Capture() {
        throw new Exception("STUB");
    }

    public static final System.Threading.AsyncFlowControl SuppressFlow() {
        throw new Exception("STUB");
    }

    public static final void RestoreFlow() {
        throw new Exception("STUB");
    }

    public static final boolean IsFlowSuppressed() {
        throw new Exception("STUB");
    }

    public static final void Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.Threading.ExecutionContext CreateCopy() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

}
