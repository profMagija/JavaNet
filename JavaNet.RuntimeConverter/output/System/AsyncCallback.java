package System;
public class AsyncCallback {
    public void Invoke(System.IAsyncResult ar) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(System.IAsyncResult ar, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
