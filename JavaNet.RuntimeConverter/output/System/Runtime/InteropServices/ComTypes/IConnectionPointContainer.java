package System.Runtime.InteropServices.ComTypes;
public interface IConnectionPointContainer {
    public void EnumConnectionPoints(System.Runtime.InteropServices.ComTypes.IEnumConnectionPoints& ppEnum) {
        throw new Exception("STUB");
    }

    public void FindConnectionPoint(System.Guid& riid, System.Runtime.InteropServices.ComTypes.IConnectionPoint& ppCP) {
        throw new Exception("STUB");
    }

}
