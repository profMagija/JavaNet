package System.Net.Security;
public class SslStream {
    public final System.Net.Security.SslApplicationProtocol get_NegotiatedApplicationProtocol() {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsClient(String targetHost, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsClient(String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, boolean checkCertificateRevocation, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsClient(String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, System.Security.Authentication.SslProtocols enabledSslProtocols, boolean checkCertificateRevocation, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public void EndAuthenticateAsClient(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsServer(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsServer(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, boolean clientCertificateRequired, boolean checkCertificateRevocation, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsServer(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, boolean clientCertificateRequired, System.Security.Authentication.SslProtocols enabledSslProtocols, boolean checkCertificateRevocation, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public void EndAuthenticateAsServer(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.Net.TransportContext get_TransportContext() {
        throw new Exception("STUB");
    }

    public void AuthenticateAsClient(String targetHost) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsClient(String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, boolean checkCertificateRevocation) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsClient(String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, System.Security.Authentication.SslProtocols enabledSslProtocols, boolean checkCertificateRevocation) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsServer(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsServer(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, boolean clientCertificateRequired, boolean checkCertificateRevocation) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsServer(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, boolean clientCertificateRequired, System.Security.Authentication.SslProtocols enabledSslProtocols, boolean checkCertificateRevocation) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsClientAsync(String targetHost) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsClientAsync(String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, boolean checkCertificateRevocation) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsClientAsync(String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, System.Security.Authentication.SslProtocols enabledSslProtocols, boolean checkCertificateRevocation) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task AuthenticateAsClientAsync(System.Net.Security.SslClientAuthenticationOptions sslClientAuthenticationOptions, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsServerAsync(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsServerAsync(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, boolean clientCertificateRequired, boolean checkCertificateRevocation) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsServerAsync(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, boolean clientCertificateRequired, System.Security.Authentication.SslProtocols enabledSslProtocols, boolean checkCertificateRevocation) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task AuthenticateAsServerAsync(System.Net.Security.SslServerAuthenticationOptions sslServerAuthenticationOptions, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task ShutdownAsync() {
        throw new Exception("STUB");
    }

    public System.Security.Authentication.SslProtocols get_SslProtocol() {
        throw new Exception("STUB");
    }

    public boolean get_CheckCertRevocationStatus() {
        throw new Exception("STUB");
    }

    public System.Security.Cryptography.X509Certificates.X509Certificate get_LocalCertificate() {
        throw new Exception("STUB");
    }

    public System.Security.Cryptography.X509Certificates.X509Certificate get_RemoteCertificate() {
        throw new Exception("STUB");
    }

    public System.Security.Authentication.CipherAlgorithmType get_CipherAlgorithm() {
        throw new Exception("STUB");
    }

    public int get_CipherStrength() {
        throw new Exception("STUB");
    }

    public System.Security.Authentication.HashAlgorithmType get_HashAlgorithm() {
        throw new Exception("STUB");
    }

    public int get_HashStrength() {
        throw new Exception("STUB");
    }

    public System.Security.Authentication.ExchangeAlgorithmType get_KeyExchangeAlgorithm() {
        throw new Exception("STUB");
    }

    public int get_KeyExchangeStrength() {
        throw new Exception("STUB");
    }

    public final void Write(System.Byte[] buffer) {
        throw new Exception("STUB");
    }

}
