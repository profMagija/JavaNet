package System.Threading;
public class SendOrPostCallback {
    public void Invoke(Object state) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object state, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
