package System.Security.Cryptography;
public class ECDiffieHellman {
    public static final System.Security.Cryptography.ECDiffieHellman Create(String algorithm) {
        throw new Exception("STUB");
    }

    public System.Security.Cryptography.ECDiffieHellmanPublicKey get_PublicKey() {
        throw new Exception("STUB");
    }

    public System.Byte[] DeriveKeyMaterial(System.Security.Cryptography.ECDiffieHellmanPublicKey otherPartyPublicKey) {
        throw new Exception("STUB");
    }

    public final System.Byte[] DeriveKeyFromHash(System.Security.Cryptography.ECDiffieHellmanPublicKey otherPartyPublicKey, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public System.Byte[] DeriveKeyFromHash(System.Security.Cryptography.ECDiffieHellmanPublicKey otherPartyPublicKey, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Byte[] secretPrepend, System.Byte[] secretAppend) {
        throw new Exception("STUB");
    }

    public final System.Byte[] DeriveKeyFromHmac(System.Security.Cryptography.ECDiffieHellmanPublicKey otherPartyPublicKey, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Byte[] hmacKey) {
        throw new Exception("STUB");
    }

    public System.Byte[] DeriveKeyFromHmac(System.Security.Cryptography.ECDiffieHellmanPublicKey otherPartyPublicKey, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Byte[] hmacKey, System.Byte[] secretPrepend, System.Byte[] secretAppend) {
        throw new Exception("STUB");
    }

    public System.Byte[] DeriveKeyTls(System.Security.Cryptography.ECDiffieHellmanPublicKey otherPartyPublicKey, System.Byte[] prfLabel, System.Byte[] prfSeed) {
        throw new Exception("STUB");
    }

    public System.Security.Cryptography.ECParameters ExportParameters(boolean includePrivateParameters) {
        throw new Exception("STUB");
    }

    public System.Security.Cryptography.ECParameters ExportExplicitParameters(boolean includePrivateParameters) {
        throw new Exception("STUB");
    }

    public void ImportParameters(System.Security.Cryptography.ECParameters parameters) {
        throw new Exception("STUB");
    }

    public void GenerateKey(System.Security.Cryptography.ECCurve curve) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECDiffieHellman Create() {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECDiffieHellman Create(System.Security.Cryptography.ECCurve curve) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECDiffieHellman Create(System.Security.Cryptography.ECParameters parameters) {
        throw new Exception("STUB");
    }

}
