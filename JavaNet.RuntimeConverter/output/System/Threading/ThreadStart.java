package System.Threading;
public class ThreadStart {
    public void Invoke() {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
