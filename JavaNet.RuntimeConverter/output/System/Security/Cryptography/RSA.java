package System.Security.Cryptography;
public class RSA {
    public static final System.Security.Cryptography.RSA Create(String algName) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.RSA Create(int keySizeInBits) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.RSA Create(System.Security.Cryptography.RSAParameters parameters) {
        throw new Exception("STUB");
    }

    public System.Security.Cryptography.RSAParameters ExportParameters(boolean includePrivateParameters) {
        throw new Exception("STUB");
    }

    public void ImportParameters(System.Security.Cryptography.RSAParameters parameters) {
        throw new Exception("STUB");
    }

    public System.Byte[] Encrypt(System.Byte[] data, System.Security.Cryptography.RSAEncryptionPadding padding) {
        throw new Exception("STUB");
    }

    public System.Byte[] Decrypt(System.Byte[] data, System.Security.Cryptography.RSAEncryptionPadding padding) {
        throw new Exception("STUB");
    }

    public System.Byte[] SignHash(System.Byte[] hash, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Security.Cryptography.RSASignaturePadding padding) {
        throw new Exception("STUB");
    }

    public boolean VerifyHash(System.Byte[] hash, System.Byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Security.Cryptography.RSASignaturePadding padding) {
        throw new Exception("STUB");
    }

    public System.Byte[] DecryptValue(System.Byte[] rgb) {
        throw new Exception("STUB");
    }

    public System.Byte[] EncryptValue(System.Byte[] rgb) {
        throw new Exception("STUB");
    }

    public final System.Byte[] SignData(System.Byte[] data, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Security.Cryptography.RSASignaturePadding padding) {
        throw new Exception("STUB");
    }

    public System.Byte[] SignData(System.Byte[] data, int offset, int count, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Security.Cryptography.RSASignaturePadding padding) {
        throw new Exception("STUB");
    }

    public System.Byte[] SignData(System.IO.Stream data, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Security.Cryptography.RSASignaturePadding padding) {
        throw new Exception("STUB");
    }

    public final boolean VerifyData(System.Byte[] data, System.Byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Security.Cryptography.RSASignaturePadding padding) {
        throw new Exception("STUB");
    }

    public boolean VerifyData(System.Byte[] data, int offset, int count, System.Byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Security.Cryptography.RSASignaturePadding padding) {
        throw new Exception("STUB");
    }

    public final boolean VerifyData(System.IO.Stream data, System.Byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Security.Cryptography.RSASignaturePadding padding) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.RSA Create() {
        throw new Exception("STUB");
    }

}
