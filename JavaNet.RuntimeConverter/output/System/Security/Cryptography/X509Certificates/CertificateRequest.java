package System.Security.Cryptography.X509Certificates;
public class CertificateRequest {
    public final System.Security.Cryptography.X509Certificates.X500DistinguishedName get_SubjectName() {
        throw new Exception("STUB");
    }

    public final System.Security.Cryptography.X509Certificates.PublicKey get_PublicKey() {
        throw new Exception("STUB");
    }

    public final System.Security.Cryptography.HashAlgorithmName get_HashAlgorithm() {
        throw new Exception("STUB");
    }

    public final System.Byte[] CreateSigningRequest() {
        throw new Exception("STUB");
    }

    public final System.Byte[] CreateSigningRequest(System.Security.Cryptography.X509Certificates.X509SignatureGenerator signatureGenerator) {
        throw new Exception("STUB");
    }

    public final System.Security.Cryptography.X509Certificates.X509Certificate2 CreateSelfSigned(System.DateTimeOffset notBefore, System.DateTimeOffset notAfter) {
        throw new Exception("STUB");
    }

    public final System.Security.Cryptography.X509Certificates.X509Certificate2 Create(System.Security.Cryptography.X509Certificates.X509Certificate2 issuerCertificate, System.DateTimeOffset notBefore, System.DateTimeOffset notAfter, System.Byte[] serialNumber) {
        throw new Exception("STUB");
    }

    public final System.Security.Cryptography.X509Certificates.X509Certificate2 Create(System.Security.Cryptography.X509Certificates.X500DistinguishedName issuerName, System.Security.Cryptography.X509Certificates.X509SignatureGenerator generator, System.DateTimeOffset notBefore, System.DateTimeOffset notAfter, System.Byte[] serialNumber) {
        throw new Exception("STUB");
    }

}
