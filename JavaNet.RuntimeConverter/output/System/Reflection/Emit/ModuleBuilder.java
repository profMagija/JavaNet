package System.Reflection.Emit;
public class ModuleBuilder {
    public final System.Reflection.Emit.TypeBuilder DefineType(String name) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineType(String name, System.Reflection.TypeAttributes attr) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineType(String name, System.Reflection.TypeAttributes attr, System.Type parent) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineType(String name, System.Reflection.TypeAttributes attr, System.Type parent, int typesize) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineType(String name, System.Reflection.TypeAttributes attr, System.Type parent, System.Reflection.Emit.PackingSize packingSize, int typesize) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineType(String name, System.Reflection.TypeAttributes attr, System.Type parent, System.Type[] interfaces) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineType(String name, System.Reflection.TypeAttributes attr, System.Type parent, System.Reflection.Emit.PackingSize packsize) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.EnumBuilder DefineEnum(String name, System.Reflection.TypeAttributes visibility, System.Type underlyingType) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodBuilder DefineGlobalMethod(String name, System.Reflection.MethodAttributes attributes, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodBuilder DefineGlobalMethod(String name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodBuilder DefineGlobalMethod(String name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] requiredReturnTypeCustomModifiers, System.Type[] optionalReturnTypeCustomModifiers, System.Type[] parameterTypes, System.Type[][] requiredParameterTypeCustomModifiers, System.Type[][] optionalParameterTypeCustomModifiers) {
        throw new Exception("STUB");
    }

    public final void CreateGlobalFunctions() {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.FieldBuilder DefineInitializedData(String name, System.Byte[] data, System.Reflection.FieldAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.FieldBuilder DefineUninitializedData(String name, int size, System.Reflection.FieldAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeToken GetTypeToken(System.Type type) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeToken GetTypeToken(String name) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodToken GetMethodToken(System.Reflection.MethodInfo method) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodToken GetArrayMethodToken(System.Type arrayClass, String methodName, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.MethodInfo GetArrayMethod(System.Type arrayClass, String methodName, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodToken GetConstructorToken(System.Reflection.ConstructorInfo con) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.FieldToken GetFieldToken(System.Reflection.FieldInfo field) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.StringToken GetStringConstant(String str) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.SignatureToken GetSignatureToken(System.Reflection.Emit.SignatureHelper sigHelper) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.SignatureToken GetSignatureToken(System.Byte[] sigBytes, int sigLength) {
        throw new Exception("STUB");
    }

    public final void SetCustomAttribute(System.Reflection.ConstructorInfo con, System.Byte[] binaryAttribute) {
        throw new Exception("STUB");
    }

    public final void SetCustomAttribute(System.Reflection.Emit.CustomAttributeBuilder customBuilder) {
        throw new Exception("STUB");
    }

    public final System.Diagnostics.SymbolStore.ISymbolDocumentWriter DefineDocument(String url, System.Guid language, System.Guid languageVendor, System.Guid documentType) {
        throw new Exception("STUB");
    }

    public final boolean IsTransient() {
        throw new Exception("STUB");
    }

}
