package System.Runtime.InteropServices.ComTypes;
public interface IPersistFile {
    public void GetClassID(System.Guid& pClassID) {
        throw new Exception("STUB");
    }

    public int IsDirty() {
        throw new Exception("STUB");
    }

    public void Load(String pszFileName, int dwMode) {
        throw new Exception("STUB");
    }

    public void Save(String pszFileName, boolean fRemember) {
        throw new Exception("STUB");
    }

    public void SaveCompleted(String pszFileName) {
        throw new Exception("STUB");
    }

    public void GetCurFile(System.String& ppszFileName) {
        throw new Exception("STUB");
    }

}
