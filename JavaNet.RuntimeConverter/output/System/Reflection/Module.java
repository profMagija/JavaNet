package System.Reflection;
public class Module {
    public static final System.Reflection.TypeFilter FilterTypeName;

    public static final System.Reflection.TypeFilter FilterTypeNameIgnoreCase;

    public System.Reflection.Assembly get_Assembly() {
        throw new Exception("STUB");
    }

    public String get_FullyQualifiedName() {
        throw new Exception("STUB");
    }

    public String get_Name() {
        throw new Exception("STUB");
    }

    public int get_MDStreamVersion() {
        throw new Exception("STUB");
    }

    public System.Guid get_ModuleVersionId() {
        throw new Exception("STUB");
    }

    public String get_ScopeName() {
        throw new Exception("STUB");
    }

    public final System.ModuleHandle get_ModuleHandle() {
        throw new Exception("STUB");
    }

    public void GetPEKind(System.Reflection.PortableExecutableKinds& peKind, System.Reflection.ImageFileMachine& machine) {
        throw new Exception("STUB");
    }

    public boolean IsResource() {
        throw new Exception("STUB");
    }

    public boolean IsDefined(System.Type attributeType, boolean inherit) {
        throw new Exception("STUB");
    }

    public System.Object[] GetCustomAttributes(boolean inherit) {
        throw new Exception("STUB");
    }

    public System.Object[] GetCustomAttributes(System.Type attributeType, boolean inherit) {
        throw new Exception("STUB");
    }

    public final System.Reflection.MethodInfo GetMethod(String name) {
        throw new Exception("STUB");
    }

    public final System.Reflection.MethodInfo GetMethod(String name, System.Type[] types) {
        throw new Exception("STUB");
    }

    public final System.Reflection.MethodInfo GetMethod(String name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, System.Type[] types, System.Reflection.ParameterModifier[] modifiers) {
        throw new Exception("STUB");
    }

    public final System.Reflection.MethodInfo[] GetMethods() {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodInfo[] GetMethods(System.Reflection.BindingFlags bindingFlags) {
        throw new Exception("STUB");
    }

    public final System.Reflection.FieldInfo GetField(String name) {
        throw new Exception("STUB");
    }

    public System.Reflection.FieldInfo GetField(String name, System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public final System.Reflection.FieldInfo[] GetFields() {
        throw new Exception("STUB");
    }

    public System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags bindingFlags) {
        throw new Exception("STUB");
    }

    public System.Type[] GetTypes() {
        throw new Exception("STUB");
    }

    public System.Type GetType(String className) {
        throw new Exception("STUB");
    }

    public System.Type GetType(String className, boolean ignoreCase) {
        throw new Exception("STUB");
    }

    public System.Type GetType(String className, boolean throwOnError, boolean ignoreCase) {
        throw new Exception("STUB");
    }

    public System.Type[] FindTypes(System.Reflection.TypeFilter filter, Object filterCriteria) {
        throw new Exception("STUB");
    }

    public int get_MetadataToken() {
        throw new Exception("STUB");
    }

    public final System.Reflection.FieldInfo ResolveField(int metadataToken) {
        throw new Exception("STUB");
    }

    public System.Reflection.FieldInfo ResolveField(int metadataToken, System.Type[] genericTypeArguments, System.Type[] genericMethodArguments) {
        throw new Exception("STUB");
    }

    public final System.Reflection.MemberInfo ResolveMember(int metadataToken) {
        throw new Exception("STUB");
    }

    public System.Reflection.MemberInfo ResolveMember(int metadataToken, System.Type[] genericTypeArguments, System.Type[] genericMethodArguments) {
        throw new Exception("STUB");
    }

    public final System.Reflection.MethodBase ResolveMethod(int metadataToken) {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodBase ResolveMethod(int metadataToken, System.Type[] genericTypeArguments, System.Type[] genericMethodArguments) {
        throw new Exception("STUB");
    }

    public System.Byte[] ResolveSignature(int metadataToken) {
        throw new Exception("STUB");
    }

    public String ResolveString(int metadataToken) {
        throw new Exception("STUB");
    }

    public final System.Type ResolveType(int metadataToken) {
        throw new Exception("STUB");
    }

    public System.Type ResolveType(int metadataToken, System.Type[] genericTypeArguments, System.Type[] genericMethodArguments) {
        throw new Exception("STUB");
    }

    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Reflection.Module left, System.Reflection.Module right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Reflection.Module left, System.Reflection.Module right) {
        throw new Exception("STUB");
    }

}
