package System.Security.Cryptography;
public class Rfc2898DeriveBytes {
    public final System.Security.Cryptography.HashAlgorithmName get_HashAlgorithm() {
        throw new Exception("STUB");
    }

    public final int get_IterationCount() {
        throw new Exception("STUB");
    }

    public final void set_IterationCount(int value) {
        throw new Exception("STUB");
    }

    public final System.Byte[] get_Salt() {
        throw new Exception("STUB");
    }

    public final void set_Salt(System.Byte[] value) {
        throw new Exception("STUB");
    }

    public final System.Byte[] CryptDeriveKey(String algname, String alghashname, int keySize, System.Byte[] rgbIV) {
        throw new Exception("STUB");
    }

}
