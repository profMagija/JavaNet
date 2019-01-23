package System.Reflection;
public class MethodBase {
    public System.Reflection.ParameterInfo[] GetParameters() {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodAttributes get_Attributes() {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodImplAttributes get_MethodImplementationFlags() {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodImplAttributes GetMethodImplementationFlags() {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodBody GetMethodBody() {
        throw new Exception("STUB");
    }

    public System.Reflection.CallingConventions get_CallingConvention() {
        throw new Exception("STUB");
    }

    public boolean get_IsConstructedGenericMethod() {
        throw new Exception("STUB");
    }

    public boolean get_IsGenericMethod() {
        throw new Exception("STUB");
    }

    public boolean get_IsGenericMethodDefinition() {
        throw new Exception("STUB");
    }

    public System.Type[] GetGenericArguments() {
        throw new Exception("STUB");
    }

    public boolean get_ContainsGenericParameters() {
        throw new Exception("STUB");
    }

    public Object Invoke(Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

    public System.RuntimeMethodHandle get_MethodHandle() {
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

    public static final System.Reflection.MethodBase GetMethodFromHandle(System.RuntimeMethodHandle handle) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.MethodBase GetMethodFromHandle(System.RuntimeMethodHandle handle, System.RuntimeTypeHandle declaringType) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.MethodBase GetCurrentMethod() {
        throw new Exception("STUB");
    }

    public final boolean get_IsAbstract() {
        throw new Exception("STUB");
    }

    public final boolean get_IsConstructor() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFinal() {
        throw new Exception("STUB");
    }

    public final boolean get_IsHideBySig() {
        throw new Exception("STUB");
    }

    public final boolean get_IsSpecialName() {
        throw new Exception("STUB");
    }

    public final boolean get_IsStatic() {
        throw new Exception("STUB");
    }

    public final boolean get_IsVirtual() {
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

    public final Object Invoke(Object obj, System.Object[] parameters) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Reflection.MethodBase left, System.Reflection.MethodBase right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Reflection.MethodBase left, System.Reflection.MethodBase right) {
        throw new Exception("STUB");
    }

}
