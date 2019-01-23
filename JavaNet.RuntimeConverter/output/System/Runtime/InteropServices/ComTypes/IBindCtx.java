package System.Runtime.InteropServices.ComTypes;
public interface IBindCtx {
    public void RegisterObjectBound(Object punk) {
        throw new Exception("STUB");
    }

    public void RevokeObjectBound(Object punk) {
        throw new Exception("STUB");
    }

    public void ReleaseBoundObjects() {
        throw new Exception("STUB");
    }

    public void SetBindOptions(System.Runtime.InteropServices.ComTypes.BIND_OPTS& pbindopts) {
        throw new Exception("STUB");
    }

    public void GetBindOptions(System.Runtime.InteropServices.ComTypes.BIND_OPTS& pbindopts) {
        throw new Exception("STUB");
    }

    public void GetRunningObjectTable(System.Runtime.InteropServices.ComTypes.IRunningObjectTable& pprot) {
        throw new Exception("STUB");
    }

    public void RegisterObjectParam(String pszKey, Object punk) {
        throw new Exception("STUB");
    }

    public void GetObjectParam(String pszKey, System.Object& ppunk) {
        throw new Exception("STUB");
    }

    public void EnumObjectParam(System.Runtime.InteropServices.ComTypes.IEnumString& ppenum) {
        throw new Exception("STUB");
    }

    public int RevokeObjectParam(String pszKey) {
        throw new Exception("STUB");
    }

}
