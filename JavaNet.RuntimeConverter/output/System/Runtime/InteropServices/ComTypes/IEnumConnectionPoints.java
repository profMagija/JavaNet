package System.Runtime.InteropServices.ComTypes;
public interface IEnumConnectionPoints {
    public int Next(int celt, System.Runtime.InteropServices.ComTypes.IConnectionPoint[] rgelt, System.IntPtr pceltFetched) {
        throw new Exception("STUB");
    }

    public int Skip(int celt) {
        throw new Exception("STUB");
    }

    public void Reset() {
        throw new Exception("STUB");
    }

    public void Clone(System.Runtime.InteropServices.ComTypes.IEnumConnectionPoints& ppenum) {
        throw new Exception("STUB");
    }

}
