package System.Security.Cryptography;
public interface ICryptoTransform {
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

}
