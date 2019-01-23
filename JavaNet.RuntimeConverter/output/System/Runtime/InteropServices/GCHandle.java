package System.Runtime.InteropServices;
public class GCHandle {
    public static final System.Runtime.InteropServices.GCHandle Alloc(Object value) {
        throw new Exception("STUB");
    }

    public static final System.Runtime.InteropServices.GCHandle Alloc(Object value, System.Runtime.InteropServices.GCHandleType type) {
        throw new Exception("STUB");
    }

    public final void Free() {
        throw new Exception("STUB");
    }

    public final Object get_Target() {
        throw new Exception("STUB");
    }

    public final void set_Target(Object value) {
        throw new Exception("STUB");
    }

    public final System.IntPtr AddrOfPinnedObject() {
        throw new Exception("STUB");
    }

    public final boolean get_IsAllocated() {
        throw new Exception("STUB");
    }

    public static final System.Runtime.InteropServices.GCHandle op_Explicit(System.IntPtr value) {
        throw new Exception("STUB");
    }

    public static final System.Runtime.InteropServices.GCHandle FromIntPtr(System.IntPtr value) {
        throw new Exception("STUB");
    }

    public static final System.IntPtr op_Explicit(System.Runtime.InteropServices.GCHandle value) {
        throw new Exception("STUB");
    }

    public static final System.IntPtr ToIntPtr(System.Runtime.InteropServices.GCHandle value) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Runtime.InteropServices.GCHandle a, System.Runtime.InteropServices.GCHandle b) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Runtime.InteropServices.GCHandle a, System.Runtime.InteropServices.GCHandle b) {
        throw new Exception("STUB");
    }

}
