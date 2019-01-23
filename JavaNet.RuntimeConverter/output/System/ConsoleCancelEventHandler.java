package System;
public class ConsoleCancelEventHandler {
    public void Invoke(Object sender, System.ConsoleCancelEventArgs e) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, System.ConsoleCancelEventArgs e, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
