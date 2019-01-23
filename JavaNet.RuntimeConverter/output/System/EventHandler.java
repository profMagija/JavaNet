package System;
public class EventHandler {
    public void Invoke(Object sender, System.EventArgs e) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, System.EventArgs e, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
