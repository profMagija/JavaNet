package System.Security.Cryptography;
public class HashAlgorithm {
    public static final System.Security.Cryptography.HashAlgorithm Create() {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.HashAlgorithm Create(String hashName) {
        throw new Exception("STUB");
    }

    public int get_HashSize() {
        throw new Exception("STUB");
    }

    public System.Byte[] get_Hash() {
        throw new Exception("STUB");
    }

    public final System.Byte[] ComputeHash(System.Byte[] buffer) {
        throw new Exception("STUB");
    }

    public final System.Byte[] ComputeHash(System.Byte[] buffer, int offset, int count) {
        throw new Exception("STUB");
    }

    public final System.Byte[] ComputeHash(System.IO.Stream inputStream) {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final void Clear() {
        throw new Exception("STUB");
    }

    public int get_InputBlockSize() {
        throw new Exception("STUB");
    }

    public int get_OutputBlockSize() {
        throw new Exception("STUB");
    }

    public boolean get_CanTransformMultipleBlocks() {
        throw new Exception("STUB");
    }

    public boolean get_CanReuseTransform() {
        throw new Exception("STUB");
    }

    public int TransformBlock(System.Byte[] inputBuffer, int inputOffset, int inputCount, System.Byte[] outputBuffer, int outputOffset) {
        throw new Exception("STUB");
    }

    public System.Byte[] TransformFinalBlock(System.Byte[] inputBuffer, int inputOffset, int inputCount) {
        throw new Exception("STUB");
    }

    public void Initialize() {
        throw new Exception("STUB");
    }

}
