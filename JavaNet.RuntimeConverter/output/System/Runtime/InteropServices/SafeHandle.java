package System.Runtime.InteropServices;
public class SafeHandle {
    public final System.IntPtr DangerousGetHandle() {
        throw new Exception("STUB");
    }

    public final boolean get_IsClosed() {
        throw new Exception("STUB");
    }

    public boolean get_IsInvalid() {
        throw new Exception("STUB");
    }

    public final void Close() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final void SetHandleAsInvalid() {
        throw new Exception("STUB");
    }

    public final void DangerousAddRef(System.Boolean& success) {
        throw new Exception("STUB");
    }

    public final void DangerousRelease() {
        throw new Exception("STUB");
    }

}
