package System.Runtime.InteropServices.ComTypes;
public interface IDataObject {
    public void GetData(System.Runtime.InteropServices.ComTypes.FORMATETC& format, System.Runtime.InteropServices.ComTypes.STGMEDIUM& medium) {
        throw new Exception("STUB");
    }

    public void GetDataHere(System.Runtime.InteropServices.ComTypes.FORMATETC& format, System.Runtime.InteropServices.ComTypes.STGMEDIUM& medium) {
        throw new Exception("STUB");
    }

    public int QueryGetData(System.Runtime.InteropServices.ComTypes.FORMATETC& format) {
        throw new Exception("STUB");
    }

    public int GetCanonicalFormatEtc(System.Runtime.InteropServices.ComTypes.FORMATETC& formatIn, System.Runtime.InteropServices.ComTypes.FORMATETC& formatOut) {
        throw new Exception("STUB");
    }

    public void SetData(System.Runtime.InteropServices.ComTypes.FORMATETC& formatIn, System.Runtime.InteropServices.ComTypes.STGMEDIUM& medium, boolean release) {
        throw new Exception("STUB");
    }

    public System.Runtime.InteropServices.ComTypes.IEnumFORMATETC EnumFormatEtc(System.Runtime.InteropServices.ComTypes.DATADIR direction) {
        throw new Exception("STUB");
    }

    public int DAdvise(System.Runtime.InteropServices.ComTypes.FORMATETC& pFormatetc, System.Runtime.InteropServices.ComTypes.ADVF advf, System.Runtime.InteropServices.ComTypes.IAdviseSink adviseSink, System.Int32& connection) {
        throw new Exception("STUB");
    }

    public void DUnadvise(int connection) {
        throw new Exception("STUB");
    }

    public int EnumDAdvise(System.Runtime.InteropServices.ComTypes.IEnumSTATDATA& enumAdvise) {
        throw new Exception("STUB");
    }

}
