package System.Security.Cryptography;
public class IncrementalHash {
    public final System.Security.Cryptography.HashAlgorithmName get_AlgorithmName() {
        throw new Exception("STUB");
    }

    public final void AppendData(System.Byte[] data) {
        throw new Exception("STUB");
    }

    public final void AppendData(System.Byte[] data, int offset, int count) {
        throw new Exception("STUB");
    }

    public final System.Byte[] GetHashAndReset() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.IncrementalHash CreateHash(System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.IncrementalHash CreateHMAC(System.Security.Cryptography.HashAlgorithmName hashAlgorithm, System.Byte[] key) {
        throw new Exception("STUB");
    }

}
