package System.Runtime.CompilerServices;
public class RuntimeHelpers {
    public static final Object GetUninitializedObject(System.Type type) {
        throw new Exception("STUB");
    }

    public static final void InitializeArray(System.Array array, System.RuntimeFieldHandle fldHandle) {
        throw new Exception("STUB");
    }

    public static final Object GetObjectValue(Object obj) {
        throw new Exception("STUB");
    }

    public static final void RunClassConstructor(System.RuntimeTypeHandle type) {
        throw new Exception("STUB");
    }

    public static final void RunModuleConstructor(System.ModuleHandle module) {
        throw new Exception("STUB");
    }

    public static final void PrepareMethod(System.RuntimeMethodHandle method) {
        throw new Exception("STUB");
    }

    public static final void PrepareMethod(System.RuntimeMethodHandle method, System.RuntimeTypeHandle[] instantiation) {
        throw new Exception("STUB");
    }

    public static final void PrepareContractedDelegate(System.Delegate d) {
        throw new Exception("STUB");
    }

    public static final void PrepareDelegate(System.Delegate d) {
        throw new Exception("STUB");
    }

    public static final int GetHashCode(Object o) {
        throw new Exception("STUB");
    }

    public static final boolean Equals(Object o1, Object o2) {
        throw new Exception("STUB");
    }

    public static final int get_OffsetToStringData() {
        throw new Exception("STUB");
    }

    public static final void EnsureSufficientExecutionStack() {
        throw new Exception("STUB");
    }

    public static final boolean TryEnsureSufficientExecutionStack() {
        throw new Exception("STUB");
    }

    public static final void ProbeForSufficientStack() {
        throw new Exception("STUB");
    }

    public static final void PrepareConstrainedRegions() {
        throw new Exception("STUB");
    }

    public static final void PrepareConstrainedRegionsNoOP() {
        throw new Exception("STUB");
    }

    public static final void ExecuteCodeWithGuaranteedCleanup(System.Runtime.CompilerServices.RuntimeHelpers+TryCode code, System.Runtime.CompilerServices.RuntimeHelpers+CleanupCode backoutCode, Object userData) {
        throw new Exception("STUB");
    }

}
