package System.Threading;
public class ParameterizedThreadStart {
    public void Invoke(Object obj) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object obj, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
