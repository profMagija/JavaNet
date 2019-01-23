package System.Threading;
public class WaitOrTimerCallback {
    public void Invoke(Object state, boolean timedOut) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object state, boolean timedOut, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
