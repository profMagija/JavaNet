package System.Security.Cryptography.X509Certificates;
public class X509SignatureGenerator {
    public final System.Security.Cryptography.X509Certificates.PublicKey get_PublicKey() {
        throw new Exception("STUB");
    }

    public System.Byte[] GetSignatureAlgorithmIdentifier(System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public System.Byte[] SignData(System.Byte[] data, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.X509Certificates.X509SignatureGenerator CreateForECDsa(System.Security.Cryptography.ECDsa key) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.X509Certificates.X509SignatureGenerator CreateForRSA(System.Security.Cryptography.RSA key, System.Security.Cryptography.RSASignaturePadding signaturePadding) {
        throw new Exception("STUB");
    }

}
