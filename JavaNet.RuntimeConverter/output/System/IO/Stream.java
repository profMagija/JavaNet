package System.IO;
public class Stream {
    public static final System.IO.Stream Null;

    public boolean get_CanRead() {
        throw new Exception("STUB");
    }

    public boolean get_CanSeek() {
        throw new Exception("STUB");
    }

    public boolean get_CanTimeout() {
        throw new Exception("STUB");
    }

    public boolean get_CanWrite() {
        throw new Exception("STUB");
    }

    public long get_Length() {
        throw new Exception("STUB");
    }

    public long get_Position() {
        throw new Exception("STUB");
    }

    public void set_Position(long value) {
        throw new Exception("STUB");
    }

    public int get_ReadTimeout() {
        throw new Exception("STUB");
    }

    public void set_ReadTimeout(int value) {
        throw new Exception("STUB");
    }

    public int get_WriteTimeout() {
        throw new Exception("STUB");
    }

    public void set_WriteTimeout(int value) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task CopyToAsync(System.IO.Stream destination) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task CopyToAsync(System.IO.Stream destination, int bufferSize) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task CopyToAsync(System.IO.Stream destination, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task CopyToAsync(System.IO.Stream destination, int bufferSize, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public final void CopyTo(System.IO.Stream destination) {
        throw new Exception("STUB");
    }

    public void CopyTo(System.IO.Stream destination, int bufferSize) {
        throw new Exception("STUB");
    }

    public void Close() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public void Flush() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task FlushAsync() {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task FlushAsync(System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginRead(System.Byte[] buffer, int offset, int count, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public int EndRead(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginWrite(System.Byte[] buffer, int offset, int count, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public void EndWrite(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task WriteAsync(System.Byte[] buffer, int offset, int count) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task WriteAsync(System.Byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public long Seek(long offset, System.IO.SeekOrigin origin) {
        throw new Exception("STUB");
    }

    public void SetLength(long value) {
        throw new Exception("STUB");
    }

    public int Read(System.Byte[] buffer, int offset, int count) {
        throw new Exception("STUB");
    }

    public int ReadByte() {
        throw new Exception("STUB");
    }

    public void Write(System.Byte[] buffer, int offset, int count) {
        throw new Exception("STUB");
    }

    public void WriteByte(byte value) {
        throw new Exception("STUB");
    }

    public static final System.IO.Stream Synchronized(System.IO.Stream stream) {
        throw new Exception("STUB");
    }

}
