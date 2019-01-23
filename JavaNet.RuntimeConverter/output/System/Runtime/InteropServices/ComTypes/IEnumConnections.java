package System.Runtime.InteropServices.ComTypes;
public interface IEnumConnections {
    public int Next(int celt, System.Runtime.InteropServices.ComTypes.CONNECTDATA[] rgelt, System.IntPtr pceltFetched) {
        throw new Exception("STUB");
    }

    public int Skip(int celt) {
        throw new Exception("STUB");
    }

    public void Reset() {
        throw new Exception("STUB");
    }

    public void Clone(System.Runtime.InteropServices.ComTypes.IEnumConnections& ppenum) {
        throw new Exception("STUB");
    }

}
