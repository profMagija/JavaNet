package System.Reflection;
public interface ICustomAttributeProvider {
    public System.Object[] GetCustomAttributes(boolean inherit) {
        throw new Exception("STUB");
    }

    public System.Object[] GetCustomAttributes(System.Type attributeType, boolean inherit) {
        throw new Exception("STUB");
    }

    public boolean IsDefined(System.Type attributeType, boolean inherit) {
        throw new Exception("STUB");
    }

}
