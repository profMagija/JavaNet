package System.Runtime.InteropServices;
public interface ICustomFactory {
    public System.MarshalByRefObject CreateInstance(System.Type serverType) {
        throw new Exception("STUB");
    }

}
