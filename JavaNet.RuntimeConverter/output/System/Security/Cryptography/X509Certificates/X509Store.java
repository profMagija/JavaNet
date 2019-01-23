package System.Security.Cryptography.X509Certificates;
public class X509Store {
    public final System.IntPtr get_StoreHandle() {
        throw new Exception("STUB");
    }

    public final System.Security.Cryptography.X509Certificates.StoreLocation get_Location() {
        throw new Exception("STUB");
    }

    public final String get_Name() {
        throw new Exception("STUB");
    }

    public final void Open(System.Security.Cryptography.X509Certificates.OpenFlags flags) {
        throw new Exception("STUB");
    }

    public final System.Security.Cryptography.X509Certificates.X509Certificate2Collection get_Certificates() {
        throw new Exception("STUB");
    }

    public final boolean get_IsOpen() {
        throw new Exception("STUB");
    }

    public final void Add(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate) {
        throw new Exception("STUB");
    }

    public final void AddRange(System.Security.Cryptography.X509Certificates.X509Certificate2Collection certificates) {
        throw new Exception("STUB");
    }

    public final void Remove(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate) {
        throw new Exception("STUB");
    }

    public final void RemoveRange(System.Security.Cryptography.X509Certificates.X509Certificate2Collection certificates) {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final void Close() {
        throw new Exception("STUB");
    }

}
