package System;
public class AssemblyLoadEventHandler {
    public void Invoke(Object sender, System.AssemblyLoadEventArgs args) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, System.AssemblyLoadEventArgs args, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public void EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
