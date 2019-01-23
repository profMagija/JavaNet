package System.Reflection;
public class ConstructorInfo {
    public static final String ConstructorName;

    public static final String TypeConstructorName;

    public final Object Invoke(System.Object[] parameters) {
        throw new Exception("STUB");
    }

    public Object Invoke(System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Reflection.ConstructorInfo left, System.Reflection.ConstructorInfo right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Reflection.ConstructorInfo left, System.Reflection.ConstructorInfo right) {
        throw new Exception("STUB");
    }

}
