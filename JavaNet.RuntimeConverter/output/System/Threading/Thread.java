package System.Threading;
public class Thread {
    public static final System.Threading.Thread get_CurrentThread() {
        throw new Exception("STUB");
    }

    public final System.Globalization.CultureInfo get_CurrentCulture() {
        throw new Exception("STUB");
    }

    public final void set_CurrentCulture(System.Globalization.CultureInfo value) {
        throw new Exception("STUB");
    }

    public final System.Globalization.CultureInfo get_CurrentUICulture() {
        throw new Exception("STUB");
    }

    public final void set_CurrentUICulture(System.Globalization.CultureInfo value) {
        throw new Exception("STUB");
    }

    public static final System.Security.Principal.IPrincipal get_CurrentPrincipal() {
        throw new Exception("STUB");
    }

    public static final void set_CurrentPrincipal(System.Security.Principal.IPrincipal value) {
        throw new Exception("STUB");
    }

    public final System.Threading.ExecutionContext get_ExecutionContext() {
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

    public final void Abort() {
        throw new Exception("STUB");
    }

    public final void Abort(Object stateInfo) {
        throw new Exception("STUB");
    }

    public static final void ResetAbort() {
        throw new Exception("STUB");
    }

    public final void Suspend() {
        throw new Exception("STUB");
    }

    public final void Resume() {
        throw new Exception("STUB");
    }

    public static final void BeginCriticalRegion() {
        throw new Exception("STUB");
    }

    public static final void EndCriticalRegion() {
        throw new Exception("STUB");
    }

    public static final void BeginThreadAffinity() {
        throw new Exception("STUB");
    }

    public static final void EndThreadAffinity() {
        throw new Exception("STUB");
    }

    public static final System.LocalDataStoreSlot AllocateDataSlot() {
        throw new Exception("STUB");
    }

    public static final System.LocalDataStoreSlot AllocateNamedDataSlot(String name) {
        throw new Exception("STUB");
    }

    public static final System.LocalDataStoreSlot GetNamedDataSlot(String name) {
        throw new Exception("STUB");
    }

    public static final void FreeNamedDataSlot(String name) {
        throw new Exception("STUB");
    }

    public static final Object GetData(System.LocalDataStoreSlot slot) {
        throw new Exception("STUB");
    }

    public static final void SetData(System.LocalDataStoreSlot slot, Object data) {
        throw new Exception("STUB");
    }

    public final System.Threading.ApartmentState get_ApartmentState() {
        throw new Exception("STUB");
    }

    public final void set_ApartmentState(System.Threading.ApartmentState value) {
        throw new Exception("STUB");
    }

    public final void SetApartmentState(System.Threading.ApartmentState state) {
        throw new Exception("STUB");
    }

    public final boolean TrySetApartmentState(System.Threading.ApartmentState state) {
        throw new Exception("STUB");
    }

    public final System.Threading.CompressedStack GetCompressedStack() {
        throw new Exception("STUB");
    }

    public final void SetCompressedStack(System.Threading.CompressedStack stack) {
        throw new Exception("STUB");
    }

    public static final int GetCurrentProcessorId() {
        throw new Exception("STUB");
    }

    public static final System.AppDomain GetDomain() {
        throw new Exception("STUB");
    }

    public static final int GetDomainID() {
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

    public final boolean Join(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public static final void MemoryBarrier() {
        throw new Exception("STUB");
    }

    public static final void Sleep(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final void Sleep(System.TimeSpan timeout) {
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

    public static final byte VolatileRead(System.Byte& address) {
        throw new Exception("STUB");
    }

    public static final double VolatileRead(System.Double& address) {
        throw new Exception("STUB");
    }

    public static final short VolatileRead(System.Int16& address) {
        throw new Exception("STUB");
    }

    public static final int VolatileRead(System.Int32& address) {
        throw new Exception("STUB");
    }

    public static final long VolatileRead(System.Int64& address) {
        throw new Exception("STUB");
    }

    public static final System.IntPtr VolatileRead(System.IntPtr& address) {
        throw new Exception("STUB");
    }

    public static final Object VolatileRead(System.Object& address) {
        throw new Exception("STUB");
    }

    public static final System.SByte VolatileRead(System.SByte& address) {
        throw new Exception("STUB");
    }

    public static final float VolatileRead(System.Single& address) {
        throw new Exception("STUB");
    }

    public static final System.UInt16 VolatileRead(System.UInt16& address) {
        throw new Exception("STUB");
    }

    public static final System.UInt32 VolatileRead(System.UInt32& address) {
        throw new Exception("STUB");
    }

    public static final System.UInt64 VolatileRead(System.UInt64& address) {
        throw new Exception("STUB");
    }

    public static final System.UIntPtr VolatileRead(System.UIntPtr& address) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.Byte& address, byte value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.Double& address, double value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.Int16& address, short value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.Int32& address, int value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.Int64& address, long value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.IntPtr& address, System.IntPtr value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.Object& address, Object value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.SByte& address, System.SByte value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.Single& address, float value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.UInt16& address, System.UInt16 value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.UInt32& address, System.UInt32 value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.UInt64& address, System.UInt64 value) {
        throw new Exception("STUB");
    }

    public static final void VolatileWrite(System.UIntPtr& address, System.UIntPtr value) {
        throw new Exception("STUB");
    }

    public final System.Threading.ApartmentState GetApartmentState() {
        throw new Exception("STUB");
    }

    public final void DisableComObjectEagerCleanup() {
        throw new Exception("STUB");
    }

}
