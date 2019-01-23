package System.Runtime.InteropServices.ComTypes;
public interface IMoniker {
    public void GetClassID(System.Guid& pClassID) {
        throw new Exception("STUB");
    }

    public int IsDirty() {
        throw new Exception("STUB");
    }

    public void Load(System.Runtime.InteropServices.ComTypes.IStream pStm) {
        throw new Exception("STUB");
    }

    public void Save(System.Runtime.InteropServices.ComTypes.IStream pStm, boolean fClearDirty) {
        throw new Exception("STUB");
    }

    public void GetSizeMax(System.Int64& pcbSize) {
        throw new Exception("STUB");
    }

    public void BindToObject(System.Runtime.InteropServices.ComTypes.IBindCtx pbc, System.Runtime.InteropServices.ComTypes.IMoniker pmkToLeft, System.Guid& riidResult, System.Object& ppvResult) {
        throw new Exception("STUB");
    }

    public void BindToStorage(System.Runtime.InteropServices.ComTypes.IBindCtx pbc, System.Runtime.InteropServices.ComTypes.IMoniker pmkToLeft, System.Guid& riid, System.Object& ppvObj) {
        throw new Exception("STUB");
    }

    public void Reduce(System.Runtime.InteropServices.ComTypes.IBindCtx pbc, int dwReduceHowFar, System.Runtime.InteropServices.ComTypes.IMoniker& ppmkToLeft, System.Runtime.InteropServices.ComTypes.IMoniker& ppmkReduced) {
        throw new Exception("STUB");
    }

    public void ComposeWith(System.Runtime.InteropServices.ComTypes.IMoniker pmkRight, boolean fOnlyIfNotGeneric, System.Runtime.InteropServices.ComTypes.IMoniker& ppmkComposite) {
        throw new Exception("STUB");
    }

    public void Enum(boolean fForward, System.Runtime.InteropServices.ComTypes.IEnumMoniker& ppenumMoniker) {
        throw new Exception("STUB");
    }

    public int IsEqual(System.Runtime.InteropServices.ComTypes.IMoniker pmkOtherMoniker) {
        throw new Exception("STUB");
    }

    public void Hash(System.Int32& pdwHash) {
        throw new Exception("STUB");
    }

    public int IsRunning(System.Runtime.InteropServices.ComTypes.IBindCtx pbc, System.Runtime.InteropServices.ComTypes.IMoniker pmkToLeft, System.Runtime.InteropServices.ComTypes.IMoniker pmkNewlyRunning) {
        throw new Exception("STUB");
    }

    public void GetTimeOfLastChange(System.Runtime.InteropServices.ComTypes.IBindCtx pbc, System.Runtime.InteropServices.ComTypes.IMoniker pmkToLeft, System.Runtime.InteropServices.ComTypes.FILETIME& pFileTime) {
        throw new Exception("STUB");
    }

    public void Inverse(System.Runtime.InteropServices.ComTypes.IMoniker& ppmk) {
        throw new Exception("STUB");
    }

    public void CommonPrefixWith(System.Runtime.InteropServices.ComTypes.IMoniker pmkOther, System.Runtime.InteropServices.ComTypes.IMoniker& ppmkPrefix) {
        throw new Exception("STUB");
    }

    public void RelativePathTo(System.Runtime.InteropServices.ComTypes.IMoniker pmkOther, System.Runtime.InteropServices.ComTypes.IMoniker& ppmkRelPath) {
        throw new Exception("STUB");
    }

    public void GetDisplayName(System.Runtime.InteropServices.ComTypes.IBindCtx pbc, System.Runtime.InteropServices.ComTypes.IMoniker pmkToLeft, System.String& ppszDisplayName) {
        throw new Exception("STUB");
    }

    public void ParseDisplayName(System.Runtime.InteropServices.ComTypes.IBindCtx pbc, System.Runtime.InteropServices.ComTypes.IMoniker pmkToLeft, String pszDisplayName, System.Int32& pchEaten, System.Runtime.InteropServices.ComTypes.IMoniker& ppmkOut) {
        throw new Exception("STUB");
    }

    public int IsSystemMoniker(System.Int32& pdwMksys) {
        throw new Exception("STUB");
    }

}
