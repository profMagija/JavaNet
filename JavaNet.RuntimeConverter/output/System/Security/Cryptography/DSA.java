package System.Security.Cryptography;
public class DSA {
    public System.Security.Cryptography.DSAParameters ExportParameters(boolean includePrivateParameters) {
        throw new Exception("STUB");
    }

    public void ImportParameters(System.Security.Cryptography.DSAParameters parameters) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.DSA Create(String algName) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.DSA Create(int keySizeInBits) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.DSA Create(System.Security.Cryptography.DSAParameters parameters) {
        throw new Exception("STUB");
    }

    public System.Byte[] CreateSignature(System.Byte[] rgbHash) {
        throw new Exception("STUB");
    }

    public boolean VerifySignature(System.Byte[] rgbHash, System.Byte[] rgbSignature) {
        throw new Exception("STUB");
    }

    public final System.Byte[] SignData(System.Byte[] data, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
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

    public boolean VerifyData(System.IO.Stream data, System.Byte[] signature, System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.DSA Create() {
        throw new Exception("STUB");
    }

}
