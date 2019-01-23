package System.Reflection;
public class ModuleResolveEventHandler {
    public System.Reflection.Module Invoke(Object sender, System.ResolveEventArgs e) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, System.ResolveEventArgs e, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public System.Reflection.Module EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
