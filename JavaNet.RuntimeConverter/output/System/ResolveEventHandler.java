package System;
public class ResolveEventHandler {
    public System.Reflection.Assembly Invoke(Object sender, System.ResolveEventArgs args) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, System.ResolveEventArgs args, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public System.Reflection.Assembly EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
