package System.Threading;
public class ThreadExceptionEventHandler {
    public void Invoke(Object sender, System.Threading.ThreadExceptionEventArgs e) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, System.Threading.ThreadExceptionEventArgs e, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
