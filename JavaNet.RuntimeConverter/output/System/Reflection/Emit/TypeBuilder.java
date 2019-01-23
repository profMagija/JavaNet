package System.Reflection.Emit;
public class TypeBuilder {
    public static final int UnspecifiedTypeSize = 0;

    public static final System.Reflection.MethodInfo GetMethod(System.Type type, System.Reflection.MethodInfo method) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.ConstructorInfo GetConstructor(System.Type type, System.Reflection.ConstructorInfo constructor) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.FieldInfo GetField(System.Type type, System.Reflection.FieldInfo field) {
        throw new Exception("STUB");
    }

    public final boolean IsCreated() {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.GenericTypeParameterBuilder[] DefineGenericParameters(System.String[] names) {
        throw new Exception("STUB");
    }

    public final void DefineMethodOverride(System.Reflection.MethodInfo methodInfoBody, System.Reflection.MethodInfo methodInfoDeclaration) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodBuilder DefineMethod(String name, System.Reflection.MethodAttributes attributes, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodBuilder DefineMethod(String name, System.Reflection.MethodAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodBuilder DefineMethod(String name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodBuilder DefineMethod(String name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.MethodBuilder DefineMethod(String name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] returnTypeRequiredCustomModifiers, System.Type[] returnTypeOptionalCustomModifiers, System.Type[] parameterTypes, System.Type[][] parameterTypeRequiredCustomModifiers, System.Type[][] parameterTypeOptionalCustomModifiers) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ConstructorBuilder DefineTypeInitializer() {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ConstructorBuilder DefineDefaultConstructor(System.Reflection.MethodAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ConstructorBuilder DefineConstructor(System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.ConstructorBuilder DefineConstructor(System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type[] parameterTypes, System.Type[][] requiredCustomModifiers, System.Type[][] optionalCustomModifiers) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineNestedType(String name) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineNestedType(String name, System.Reflection.TypeAttributes attr, System.Type parent, System.Type[] interfaces) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineNestedType(String name, System.Reflection.TypeAttributes attr, System.Type parent) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineNestedType(String name, System.Reflection.TypeAttributes attr) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineNestedType(String name, System.Reflection.TypeAttributes attr, System.Type parent, int typeSize) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineNestedType(String name, System.Reflection.TypeAttributes attr, System.Type parent, System.Reflection.Emit.PackingSize packSize) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeBuilder DefineNestedType(String name, System.Reflection.TypeAttributes attr, System.Type parent, System.Reflection.Emit.PackingSize packSize, int typeSize) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.FieldBuilder DefineField(String fieldName, System.Type type, System.Reflection.FieldAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.FieldBuilder DefineField(String fieldName, System.Type type, System.Type[] requiredCustomModifiers, System.Type[] optionalCustomModifiers, System.Reflection.FieldAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.FieldBuilder DefineInitializedData(String name, System.Byte[] data, System.Reflection.FieldAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.FieldBuilder DefineUninitializedData(String name, int size, System.Reflection.FieldAttributes attributes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.PropertyBuilder DefineProperty(String name, System.Reflection.PropertyAttributes attributes, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.PropertyBuilder DefineProperty(String name, System.Reflection.PropertyAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.PropertyBuilder DefineProperty(String name, System.Reflection.PropertyAttributes attributes, System.Type returnType, System.Type[] returnTypeRequiredCustomModifiers, System.Type[] returnTypeOptionalCustomModifiers, System.Type[] parameterTypes, System.Type[][] parameterTypeRequiredCustomModifiers, System.Type[][] parameterTypeOptionalCustomModifiers) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.PropertyBuilder DefineProperty(String name, System.Reflection.PropertyAttributes attributes, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] returnTypeRequiredCustomModifiers, System.Type[] returnTypeOptionalCustomModifiers, System.Type[] parameterTypes, System.Type[][] parameterTypeRequiredCustomModifiers, System.Type[][] parameterTypeOptionalCustomModifiers) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.EventBuilder DefineEvent(String name, System.Reflection.EventAttributes attributes, System.Type eventtype) {
        throw new Exception("STUB");
    }

    public final System.Reflection.TypeInfo CreateTypeInfo() {
        throw new Exception("STUB");
    }

    public final System.Type CreateType() {
        throw new Exception("STUB");
    }

    public final int get_Size() {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.PackingSize get_PackingSize() {
        throw new Exception("STUB");
    }

    public final void SetParent(System.Type parent) {
        throw new Exception("STUB");
    }

    public final void AddInterfaceImplementation(System.Type interfaceType) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Emit.TypeToken get_TypeToken() {
        throw new Exception("STUB");
    }

    public final void SetCustomAttribute(System.Reflection.ConstructorInfo con, System.Byte[] binaryAttribute) {
        throw new Exception("STUB");
    }

    public final void SetCustomAttribute(System.Reflection.Emit.CustomAttributeBuilder customBuilder) {
        throw new Exception("STUB");
    }

}
