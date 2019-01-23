package System.Net.Security;
public class ServerCertificateSelectionCallback {
    public System.Security.Cryptography.X509Certificates.X509Certificate Invoke(Object sender, String hostName) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, String hostName, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public System.Security.Cryptography.X509Certificates.X509Certificate EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
