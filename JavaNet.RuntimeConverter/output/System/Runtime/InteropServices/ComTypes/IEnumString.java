package System.Runtime.InteropServices.ComTypes;
public interface IEnumString {
    public int Next(int celt, System.String[] rgelt, System.IntPtr pceltFetched) {
        throw new Exception("STUB");
    }

    public int Skip(int celt) {
        throw new Exception("STUB");
    }

    public void Reset() {
        throw new Exception("STUB");
    }

    public void Clone(System.Runtime.InteropServices.ComTypes.IEnumString& ppenum) {
        throw new Exception("STUB");
    }

}
