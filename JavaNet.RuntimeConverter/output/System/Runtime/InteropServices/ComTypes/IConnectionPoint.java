package System.Runtime.InteropServices.ComTypes;
public interface IConnectionPoint {
    public void GetConnectionInterface(System.Guid& pIID) {
        throw new Exception("STUB");
    }

    public void GetConnectionPointContainer(System.Runtime.InteropServices.ComTypes.IConnectionPointContainer& ppCPC) {
        throw new Exception("STUB");
    }

    public void Advise(Object pUnkSink, System.Int32& pdwCookie) {
        throw new Exception("STUB");
    }

    public void Unadvise(int dwCookie) {
        throw new Exception("STUB");
    }

    public void EnumConnections(System.Runtime.InteropServices.ComTypes.IEnumConnections& ppEnum) {
        throw new Exception("STUB");
    }

}
