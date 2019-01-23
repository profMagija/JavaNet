package Internal.Runtime.Augments;
public class RuntimeThread {
    public static final Internal.Runtime.Augments.RuntimeThread Create(System.Threading.ThreadStart start) {
        throw new Exception("STUB");
    }

    public static final Internal.Runtime.Augments.RuntimeThread Create(System.Threading.ThreadStart start, int maxStackSize) {
        throw new Exception("STUB");
    }

    public static final Internal.Runtime.Augments.RuntimeThread Create(System.Threading.ParameterizedThreadStart start) {
        throw new Exception("STUB");
    }

    public static final Internal.Runtime.Augments.RuntimeThread Create(System.Threading.ParameterizedThreadStart start, int maxStackSize) {
        throw new Exception("STUB");
    }

    public static final Internal.Runtime.Augments.RuntimeThread get_CurrentThread() {
        throw new Exception("STUB");
    }

    public final boolean get_IsAlive() {
        throw new Exception("STUB");
    }

    public final boolean get_IsBackground() {
        throw new Exception("STUB");
    }

    public final void set_IsBackground(boolean value) {
        throw new Exception("STUB");
    }

    public final boolean get_IsThreadPoolThread() {
        throw new Exception("STUB");
    }

    public final int get_ManagedThreadId() {
        throw new Exception("STUB");
    }

    public final String get_Name() {
        throw new Exception("STUB");
    }

    public final void set_Name(String value) {
        throw new Exception("STUB");
    }

    public final System.Threading.ThreadPriority get_Priority() {
        throw new Exception("STUB");
    }

    public final void set_Priority(System.Threading.ThreadPriority value) {
        throw new Exception("STUB");
    }

    public final System.Threading.ThreadState get_ThreadState() {
        throw new Exception("STUB");
    }

    public final System.Threading.ApartmentState GetApartmentState() {
        throw new Exception("STUB");
    }

    public final boolean TrySetApartmentState(System.Threading.ApartmentState state) {
        throw new Exception("STUB");
    }

    public final void DisableComObjectEagerCleanup() {
        throw new Exception("STUB");
    }

    public final void Interrupt() {
        throw new Exception("STUB");
    }

    public final void Join() {
        throw new Exception("STUB");
    }

    public final boolean Join(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final void Sleep(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final int GetCurrentProcessorId() {
        throw new Exception("STUB");
    }

    public static final void SpinWait(int iterations) {
        throw new Exception("STUB");
    }

    public static final boolean Yield() {
        throw new Exception("STUB");
    }

    public final void Start() {
        throw new Exception("STUB");
    }

    public final void Start(Object parameter) {
        throw new Exception("STUB");
    }

}
