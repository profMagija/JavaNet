package System.Net;
public class HttpContinueDelegate {
    public void Invoke(int StatusCode, System.Net.WebHeaderCollection httpHeaders) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(int StatusCode, System.Net.WebHeaderCollection httpHeaders, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
