package System.Runtime.InteropServices.ComTypes;
public interface IEnumSTATDATA {
    public int Next(int celt, System.Runtime.InteropServices.ComTypes.STATDATA[] rgelt, System.Int32[] pceltFetched) {
        throw new Exception("STUB");
    }

    public int Skip(int celt) {
        throw new Exception("STUB");
    }

    public int Reset() {
        throw new Exception("STUB");
    }

    public void Clone(System.Runtime.InteropServices.ComTypes.IEnumSTATDATA& newEnum) {
        throw new Exception("STUB");
    }

}
