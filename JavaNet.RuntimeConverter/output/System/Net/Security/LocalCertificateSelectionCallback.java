package System.Net.Security;
public class LocalCertificateSelectionCallback {
    public System.Security.Cryptography.X509Certificates.X509Certificate Invoke(Object sender, String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection localCertificates, System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, System.String[] acceptableIssuers) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginInvoke(Object sender, String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection localCertificates, System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, System.String[] acceptableIssuers, System.AsyncCallback callback, Object object) {
        throw new Exception("STUB");
    }

    public System.Security.Cryptography.X509Certificates.X509Certificate EndInvoke(System.IAsyncResult result) {
        throw new Exception("STUB");
    }

}
