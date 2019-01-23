package System.Runtime.CompilerServices;
public class CleanupCode {
    public void Invoke(Object userData, boolean exceptionThrown) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object userData, boolean exceptionThrown, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
