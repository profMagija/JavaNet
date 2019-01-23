package System.Reflection.Emit;
public class AssemblyBuilder {
    public static final System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ModuleBuilder DefineDynamicModule(String name) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ModuleBuilder DefineDynamicModule(String name, boolean emitSymbolInfo) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ModuleBuilder GetDynamicModule(String name) {
        throw new Exception("STUB");
    }

    public final void SetCustomAttribute(System.Reflection.ConstructorInfo con, System.Byte[] binaryAttribute) {
        throw new Exception("STUB");
    }

    public final void SetCustomAttribute(System.Reflection.Emit.CustomAttributeBuilder customBuilder) {
        throw new Exception("STUB");
    }

}
