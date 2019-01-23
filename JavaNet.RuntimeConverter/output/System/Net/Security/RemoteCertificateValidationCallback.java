package System.Net.Security;
public class RemoteCertificateValidationCallback {
    public boolean Invoke(Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public boolean EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
