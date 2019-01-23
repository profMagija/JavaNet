package System.Reflection;
public interface IReflect {
    public System.Reflection.MethodInfo GetMethod(String name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Type[] types, System.Reflection.ParameterModifier[] modifiers) {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodInfo GetMethod(String name, System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodInfo[] GetMethods(System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public System.Reflection.FieldInfo GetField(String name, System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public System.Reflection.PropertyInfo GetProperty(String name, System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public System.Reflection.PropertyInfo GetProperty(String name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Type returnType, System.Type[] types, System.Reflection.ParameterModifier[] modifiers) {
        throw new Exception("STUB");
    }

    public System.Reflection.PropertyInfo[] GetProperties(System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public System.Reflection.MemberInfo[] GetMember(String name, System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public System.Reflection.MemberInfo[] GetMembers(System.Reflection.BindingFlags bindingAttr) {
        throw new Exception("STUB");
    }

    public Object InvokeMember(String name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object target, System.Object[] args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, System.String[] namedParameters) {
        throw new Exception("STUB");
    }

    public System.Type get_UnderlyingSystemType() {
        throw new Exception("STUB");
    }

}
