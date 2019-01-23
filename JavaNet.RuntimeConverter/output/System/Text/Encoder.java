package System.Text;
public class Encoder {
    public final System.Text.EncoderFallback get_Fallback() {
        throw new Exception("STUB");
    }

    public final void set_Fallback(System.Text.EncoderFallback value) {
        throw new Exception("STUB");
    }

    public final System.Text.EncoderFallbackBuffer get_FallbackBuffer() {
        throw new Exception("STUB");
    }

    public void Reset() {
        throw new Exception("STUB");
    }

    public int GetByteCount(System.Char[] chars, int index, int count, boolean flush) {
        throw new Exception("STUB");
    }

    public int GetBytes(System.Char[] chars, int charIndex, int charCount, System.Byte[] bytes, int byteIndex, boolean flush) {
        throw new Exception("STUB");
    }

    public void Convert(System.Char[] chars, int charIndex, int charCount, System.Byte[] bytes, int byteIndex, int byteCount, boolean flush, System.Int32& charsUsed, System.Int32& bytesUsed, System.Boolean& completed) {
        throw new Exception("STUB");
    }

}
