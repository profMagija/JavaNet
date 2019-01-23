package System.Runtime.InteropServices.ComTypes;
public interface IAdviseSink {
    public void OnDataChange(System.Runtime.InteropServices.ComTypes.FORMATETC& format, System.Runtime.InteropServices.ComTypes.STGMEDIUM& stgmedium) {
        throw new Exception("STUB");
    }

    public void OnViewChange(int aspect, int index) {
        throw new Exception("STUB");
    }

    public void OnRename(System.Runtime.InteropServices.ComTypes.IMoniker moniker) {
        throw new Exception("STUB");
    }

    public void OnSave() {
        throw new Exception("STUB");
    }

    public void OnClose() {
        throw new Exception("STUB");
    }

}
