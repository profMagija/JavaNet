package System.Runtime.InteropServices.ComTypes;
public interface ITypeLib {
    public int GetTypeInfoCount() {
        throw new Exception("STUB");
    }

    public void GetTypeInfo(int index, System.Runtime.InteropServices.ComTypes.ITypeInfo& ppTI) {
        throw new Exception("STUB");
    }

    public void GetTypeInfoType(int index, System.Runtime.InteropServices.ComTypes.TYPEKIND& pTKind) {
        throw new Exception("STUB");
    }

    public void GetTypeInfoOfGuid(System.Guid& guid, System.Runtime.InteropServices.ComTypes.ITypeInfo& ppTInfo) {
        throw new Exception("STUB");
    }

    public void GetLibAttr(System.IntPtr& ppTLibAttr) {
        throw new Exception("STUB");
    }

    public void GetTypeComp(System.Runtime.InteropServices.ComTypes.ITypeComp& ppTComp) {
        throw new Exception("STUB");
    }

    public void GetDocumentation(int index, System.String& strName, System.String& strDocString, System.Int32& dwHelpContext, System.String& strHelpFile) {
        throw new Exception("STUB");
    }

    public boolean IsName(String szNameBuf, int lHashVal) {
        throw new Exception("STUB");
    }

    public void FindName(String szNameBuf, int lHashVal, System.Runtime.InteropServices.ComTypes.ITypeInfo[] ppTInfo, System.Int32[] rgMemId, System.Int16& pcFound) {
        throw new Exception("STUB");
    }

    public void ReleaseTLibAttr(System.IntPtr pTLibAttr) {
        throw new Exception("STUB");
    }

}
