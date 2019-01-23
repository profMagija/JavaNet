package System.Security.Cryptography;
public class ECDsa {
    public static final System.Security.Cryptography.ECDsa Create(String algorithm) {
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

    public System.Byte[] SignData(System.Byte[] data, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public System.Byte[] SignData(System.Byte[] data, int offset, int count, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public System.Byte[] SignData(System.IO.Stream data, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public final boolean VerifyData(System.Byte[] data, System.Byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public boolean VerifyData(System.Byte[] data, int offset, int count, System.Byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public final boolean VerifyData(System.IO.Stream data, System.Byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public System.Byte[] SignHash(System.Byte[] hash) {
        throw new Exception("STUB");
    }

    public boolean VerifyHash(System.Byte[] hash, System.Byte[] signature) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECDsa Create() {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECDsa Create(System.Security.Cryptography.ECCurve curve) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECDsa Create(System.Security.Cryptography.ECParameters parameters) {
        throw new Exception("STUB");
    }

}
