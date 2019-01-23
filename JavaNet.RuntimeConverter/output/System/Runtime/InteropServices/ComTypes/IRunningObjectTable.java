package System.Runtime.InteropServices.ComTypes;
public interface IRunningObjectTable {
    public int Register(int grfFlags, Object punkObject, System.Runtime.InteropServices.ComTypes.IMoniker pmkObjectName) {
        throw new Exception("STUB");
    }

    public void Revoke(int dwRegister) {
        throw new Exception("STUB");
    }

    public int IsRunning(System.Runtime.InteropServices.ComTypes.IMoniker pmkObjectName) {
        throw new Exception("STUB");
    }

    public int GetObject(System.Runtime.InteropServices.ComTypes.IMoniker pmkObjectName, System.Object& ppunkObject) {
        throw new Exception("STUB");
    }

    public void NoteChangeTime(int dwRegister, System.Runtime.InteropServices.ComTypes.FILETIME& pfiletime) {
        throw new Exception("STUB");
    }

    public int GetTimeOfLastChange(System.Runtime.InteropServices.ComTypes.IMoniker pmkObjectName, System.Runtime.InteropServices.ComTypes.FILETIME& pfiletime) {
        throw new Exception("STUB");
    }

    public void EnumRunning(System.Runtime.InteropServices.ComTypes.IEnumMoniker& ppenumMoniker) {
        throw new Exception("STUB");
    }

}
