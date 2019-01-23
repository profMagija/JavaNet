package System.Runtime.InteropServices.ComTypes;
public interface ITypeInfo {
    public void GetTypeAttr(System.IntPtr& ppTypeAttr) {
        throw new Exception("STUB");
    }

    public void GetTypeComp(System.Runtime.InteropServices.ComTypes.ITypeComp& ppTComp) {
        throw new Exception("STUB");
    }

    public void GetFuncDesc(int index, System.IntPtr& ppFuncDesc) {
        throw new Exception("STUB");
    }

    public void GetVarDesc(int index, System.IntPtr& ppVarDesc) {
        throw new Exception("STUB");
    }

    public void GetNames(int memid, System.String[] rgBstrNames, int cMaxNames, System.Int32& pcNames) {
        throw new Exception("STUB");
    }

    public void GetRefTypeOfImplType(int index, System.Int32& href) {
        throw new Exception("STUB");
    }

    public void GetImplTypeFlags(int index, System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS& pImplTypeFlags) {
        throw new Exception("STUB");
    }

    public void GetIDsOfNames(System.String[] rgszNames, int cNames, System.Int32[] pMemId) {
        throw new Exception("STUB");
    }

    public void Invoke(Object pvInstance, int memid, short wFlags, System.Runtime.InteropServices.ComTypes.DISPPARAMS& pDispParams, System.IntPtr pVarResult, System.IntPtr pExcepInfo, System.Int32& puArgErr) {
        throw new Exception("STUB");
    }

    public void GetDocumentation(int index, System.String& strName, System.String& strDocString, System.Int32& dwHelpContext, System.String& strHelpFile) {
        throw new Exception("STUB");
    }

    public void GetDllEntry(int memid, System.Runtime.InteropServices.ComTypes.INVOKEKIND invKind, System.IntPtr pBstrDllName, System.IntPtr pBstrName, System.IntPtr pwOrdinal) {
        throw new Exception("STUB");
    }

    public void GetRefTypeInfo(int hRef, System.Runtime.InteropServices.ComTypes.ITypeInfo& ppTI) {
        throw new Exception("STUB");
    }

    public void AddressOfMember(int memid, System.Runtime.InteropServices.ComTypes.INVOKEKIND invKind, System.IntPtr& ppv) {
        throw new Exception("STUB");
    }

    public void CreateInstance(Object pUnkOuter, System.Guid& riid, System.Object& ppvObj) {
        throw new Exception("STUB");
    }

    public void GetMops(int memid, System.String& pBstrMops) {
        throw new Exception("STUB");
    }

    public void GetContainingTypeLib(System.Runtime.InteropServices.ComTypes.ITypeLib& ppTLB, System.Int32& pIndex) {
        throw new Exception("STUB");
    }

    public void ReleaseTypeAttr(System.IntPtr pTypeAttr) {
        throw new Exception("STUB");
    }

    public void ReleaseFuncDesc(System.IntPtr pFuncDesc) {
        throw new Exception("STUB");
    }

    public void ReleaseVarDesc(System.IntPtr pVarDesc) {
        throw new Exception("STUB");
    }

}
