package System.Reflection;
public class FieldInfo {
    public static final System.Reflection.FieldInfo GetFieldFromHandle(System.RuntimeFieldHandle handle) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.FieldInfo GetFieldFromHandle(System.RuntimeFieldHandle handle, System.RuntimeTypeHandle declaringType) {
        throw new Exception("STUB");
    }

    public System.Reflection.FieldAttributes get_Attributes() {
        throw new Exception("STUB");
    }

    public System.Type get_FieldType() {
        throw new Exception("STUB");
    }

    public final boolean get_IsInitOnly() {
        throw new Exception("STUB");
    }

    public final boolean get_IsLiteral() {
        throw new Exception("STUB");
    }

    public final boolean get_IsNotSerialized() {
        throw new Exception("STUB");
    }

    public final boolean get_IsPinvokeImpl() {
        throw new Exception("STUB");
    }

    public final boolean get_IsSpecialName() {
        throw new Exception("STUB");
    }

    public final boolean get_IsStatic() {
        throw new Exception("STUB");
    }

    public final boolean get_IsAssembly() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFamily() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFamilyAndAssembly() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFamilyOrAssembly() {
        throw new Exception("STUB");
    }

    public final boolean get_IsPrivate() {
        throw new Exception("STUB");
    }

    public final boolean get_IsPublic() {
        throw new Exception("STUB");
    }

    public boolean get_IsSecurityCritical() {
        throw new Exception("STUB");
    }

    public boolean get_IsSecuritySafeCritical() {
        throw new Exception("STUB");
    }

    public boolean get_IsSecurityTransparent() {
        throw new Exception("STUB");
    }

    public System.RuntimeFieldHandle get_FieldHandle() {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Reflection.FieldInfo left, System.Reflection.FieldInfo right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Reflection.FieldInfo left, System.Reflection.FieldInfo right) {
        throw new Exception("STUB");
    }

    public Object GetValue(Object obj) {
        throw new Exception("STUB");
    }

    public final void SetValue(Object obj, Object value) {
        throw new Exception("STUB");
    }

    public void SetValue(Object obj, Object value, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

    public void SetValueDirect(System.TypedReference obj, Object value) {
        throw new Exception("STUB");
    }

    public Object GetValueDirect(System.TypedReference obj) {
        throw new Exception("STUB");
    }

    public Object GetRawConstantValue() {
        throw new Exception("STUB");
    }

    public System.Type[] GetOptionalCustomModifiers() {
        throw new Exception("STUB");
    }

    public System.Type[] GetRequiredCustomModifiers() {
        throw new Exception("STUB");
    }

}
