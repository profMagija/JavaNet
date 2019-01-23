package System.Reflection;
public class Binder {
    public System.Reflection.FieldInfo BindToField(System.Reflection.BindingFlags bindingAttr, System.Reflection.FieldInfo[] match, Object value, System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodBase BindToMethod(System.Reflection.BindingFlags bindingAttr, System.Reflection.MethodBase[] match, System.Object[]& args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, System.String[] names, System.Object& state) {
        throw new Exception("STUB");
    }

    public Object ChangeType(Object value, System.Type type, System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

    public void ReorderArgumentArray(System.Object[]& args, Object state) {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodBase SelectMethod(System.Reflection.BindingFlags bindingAttr, System.Reflection.MethodBase[] match, System.Type[] types, System.Reflection.ParameterModifier[] modifiers) {
        throw new Exception("STUB");
    }

    public System.Reflection.PropertyInfo SelectProperty(System.Reflection.BindingFlags bindingAttr, System.Reflection.PropertyInfo[] match, System.Type returnType, System.Type[] indexes, System.Reflection.ParameterModifier[] modifiers) {
        throw new Exception("STUB");
    }

}
