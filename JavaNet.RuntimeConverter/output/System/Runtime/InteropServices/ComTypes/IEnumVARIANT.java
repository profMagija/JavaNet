package System.Runtime.InteropServices.ComTypes;
public interface IEnumVARIANT {
    public int Next(int celt, System.Object[] rgVar, System.IntPtr pceltFetched) {
        throw new Exception("STUB");
    }

    public int Skip(int celt) {
        throw new Exception("STUB");
    }

    public int Reset() {
        throw new Exception("STUB");
    }

    public System.Runtime.InteropServices.ComTypes.IEnumVARIANT Clone() {
        throw new Exception("STUB");
    }

}
