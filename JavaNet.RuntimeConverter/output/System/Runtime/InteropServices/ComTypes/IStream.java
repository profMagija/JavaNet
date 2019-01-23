package System.Runtime.InteropServices.ComTypes;
public interface IStream {
    public void Read(System.Byte[] pv, int cb, System.IntPtr pcbRead) {
        throw new Exception("STUB");
    }

    public void Write(System.Byte[] pv, int cb, System.IntPtr pcbWritten) {
        throw new Exception("STUB");
    }

    public void Seek(long dlibMove, int dwOrigin, System.IntPtr plibNewPosition) {
        throw new Exception("STUB");
    }

    public void SetSize(long libNewSize) {
        throw new Exception("STUB");
    }

    public void CopyTo(System.Runtime.InteropServices.ComTypes.IStream pstm, long cb, System.IntPtr pcbRead, System.IntPtr pcbWritten) {
        throw new Exception("STUB");
    }

    public void Commit(int grfCommitFlags) {
        throw new Exception("STUB");
    }

    public void Revert() {
        throw new Exception("STUB");
    }

    public void LockRegion(long libOffset, long cb, int dwLockType) {
        throw new Exception("STUB");
    }

    public void UnlockRegion(long libOffset, long cb, int dwLockType) {
        throw new Exception("STUB");
    }

    public void Stat(System.Runtime.InteropServices.ComTypes.STATSTG& pstatstg, int grfStatFlag) {
        throw new Exception("STUB");
    }

    public void Clone(System.Runtime.InteropServices.ComTypes.IStream& ppstm) {
        throw new Exception("STUB");
    }

}
