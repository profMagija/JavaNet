package System.Net.Http;
public class HttpContent {
    public void Dispose() {
        throw new Exception("STUB");
    }

    public final System.Net.Http.Headers.HttpContentHeaders get_Headers() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task CopyToAsync(System.IO.Stream stream, System.Net.TransportContext context) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task CopyToAsync(System.IO.Stream stream) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task LoadIntoBufferAsync() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task LoadIntoBufferAsync(long maxBufferSize) {
        throw new Exception("STUB");
    }

}
