package System.Runtime.InteropServices;
public interface ICustomMarshaler {
    public Object MarshalNativeToManaged(System.IntPtr pNativeData) {
        throw new Exception("STUB");
    }

    public System.IntPtr MarshalManagedToNative(Object ManagedObj) {
        throw new Exception("STUB");
    }

    public void CleanUpNativeData(System.IntPtr pNativeData) {
        throw new Exception("STUB");
    }

    public void CleanUpManagedData(Object ManagedObj) {
        throw new Exception("STUB");
    }

    public int GetNativeDataSize() {
        throw new Exception("STUB");
    }

}
