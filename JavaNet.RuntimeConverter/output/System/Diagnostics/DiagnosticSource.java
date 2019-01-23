package System.Diagnostics;
public class DiagnosticSource {
    public void Write(String name, Object value) {
        throw new Exception("STUB");
    }

    public boolean IsEnabled(String name) {
        throw new Exception("STUB");
    }

    public boolean IsEnabled(String name, Object arg1, Object arg2) {
        throw new Exception("STUB");
    }

    public final System.Diagnostics.Activity StartActivity(System.Diagnostics.Activity activity, Object args) {
        throw new Exception("STUB");
    }

    public final void StopActivity(System.Diagnostics.Activity activity, Object args) {
        throw new Exception("STUB");
    }

}
