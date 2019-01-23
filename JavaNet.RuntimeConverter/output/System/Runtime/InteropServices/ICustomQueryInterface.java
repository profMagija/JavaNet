package System.Runtime.InteropServices;
public interface ICustomQueryInterface {
    public System.Runtime.InteropServices.CustomQueryInterfaceResult GetInterface(System.Guid& iid, System.IntPtr& ppv) {
        throw new Exception("STUB");
    }

}
