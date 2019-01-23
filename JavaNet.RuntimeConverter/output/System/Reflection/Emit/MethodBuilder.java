package System.Reflection.Emit;
public class MethodBuilder {
    public final System.Reflection.Emit.GenericTypeParameterBuilder[] DefineGenericParameters(System.String[] names) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodToken GetToken() {
        throw new Exception("STUB");
    }

    public final void SetParameters(System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final void SetReturnType(System.Type returnType) {
        throw new Exception("STUB");
    }

    public final void SetSignature(System.Type returnType, System.Type[] returnTypeRequiredCustomModifiers, System.Type[] returnTypeOptionalCustomModifiers, System.Type[] parameterTypes, System.Type[][] parameterTypeRequiredCustomModifiers, System.Type[][] parameterTypeOptionalCustomModifiers) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ParameterBuilder DefineParameter(int position, System.Reflection.ParameterAttributes attributes, String strParamName) {
        throw new Exception("STUB");
    }

    public final void SetImplementationFlags(System.Reflection.MethodImplAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ILGenerator GetILGenerator() {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ILGenerator GetILGenerator(int size) {
        throw new Exception("STUB");
    }

    public final boolean get_InitLocals() {
        throw new Exception("STUB");
    }

    public final void set_InitLocals(boolean value) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Module GetModule() {
        throw new Exception("STUB");
    }

    public final String get_Signature() {
        throw new Exception("STUB");
    }

    public final void SetCustomAttribute(System.Reflection.ConstructorInfo con, System.Byte[] binaryAttribute) {
        throw new Exception("STUB");
    }

    public final void SetCustomAttribute(System.Reflection.Emit.CustomAttributeBuilder customBuilder) {
        throw new Exception("STUB");
    }

}
