package System;
public class ModuleHandle {
    public static final System.ModuleHandle EmptyHandle;

    public final boolean Equals(System.ModuleHandle handle) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.ModuleHandle left, System.ModuleHandle right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.ModuleHandle left, System.ModuleHandle right) {
        throw new Exception("STUB");
    }

    public final System.RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken) {
        throw new Exception("STUB");
    }

    public final System.RuntimeTypeHandle ResolveTypeHandle(int typeToken) {
        throw new Exception("STUB");
    }

    public final System.RuntimeTypeHandle ResolveTypeHandle(int typeToken, System.RuntimeTypeHandle[] typeInstantiationContext, System.RuntimeTypeHandle[] methodInstantiationContext) {
        throw new Exception("STUB");
    }

    public final System.RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken) {
        throw new Exception("STUB");
    }

    public final System.RuntimeMethodHandle ResolveMethodHandle(int methodToken) {
        throw new Exception("STUB");
    }

    public final System.RuntimeMethodHandle ResolveMethodHandle(int methodToken, System.RuntimeTypeHandle[] typeInstantiationContext, System.RuntimeTypeHandle[] methodInstantiationContext) {
        throw new Exception("STUB");
    }

    public final System.RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken) {
        throw new Exception("STUB");
    }

    public final System.RuntimeFieldHandle ResolveFieldHandle(int fieldToken) {
        throw new Exception("STUB");
    }

    public final System.RuntimeFieldHandle ResolveFieldHandle(int fieldToken, System.RuntimeTypeHandle[] typeInstantiationContext, System.RuntimeTypeHandle[] methodInstantiationContext) {
        throw new Exception("STUB");
    }

    public final int get_MDStreamVersion() {
        throw new Exception("STUB");
    }

}
