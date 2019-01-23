package System;
public class UnhandledExceptionEventHandler {
    public void Invoke(Object sender, System.UnhandledExceptionEventArgs e) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, System.UnhandledExceptionEventArgs e, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
