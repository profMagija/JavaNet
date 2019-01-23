package System.Runtime.InteropServices.ComTypes;
public interface IEnumMoniker {
    public int Next(int celt, System.Runtime.InteropServices.ComTypes.IMoniker[] rgelt, System.IntPtr pceltFetched) {
        throw new Exception("STUB");
    }

    public int Skip(int celt) {
        throw new Exception("STUB");
    }

    public void Reset() {
        throw new Exception("STUB");
    }

    public void Clone(System.Runtime.InteropServices.ComTypes.IEnumMoniker& ppenum) {
        throw new Exception("STUB");
    }

}
