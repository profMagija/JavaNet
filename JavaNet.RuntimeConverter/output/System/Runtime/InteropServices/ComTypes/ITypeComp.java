package System.Runtime.InteropServices.ComTypes;
public interface ITypeComp {
    public void Bind(String szName, int lHashVal, short wFlags, System.Runtime.InteropServices.ComTypes.ITypeInfo& ppTInfo, System.Runtime.InteropServices.ComTypes.DESCKIND& pDescKind, System.Runtime.InteropServices.ComTypes.BINDPTR& pBindPtr) {
        throw new Exception("STUB");
    }

    public void BindType(String szName, int lHashVal, System.Runtime.InteropServices.ComTypes.ITypeInfo& ppTInfo, System.Runtime.InteropServices.ComTypes.ITypeComp& ppTComp) {
        throw new Exception("STUB");
    }

}
