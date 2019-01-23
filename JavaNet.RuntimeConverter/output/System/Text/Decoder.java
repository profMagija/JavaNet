package System.Text;
public class Decoder {
    public final System.Text.DecoderFallback get_Fallback() {
        throw new Exception("STUB");
    }

    public final void set_Fallback(System.Text.DecoderFallback value) {
        throw new Exception("STUB");
    }

    public final System.Text.DecoderFallbackBuffer get_FallbackBuffer() {
        throw new Exception("STUB");
    }

    public void Reset() {
        throw new Exception("STUB");
    }

    public int GetCharCount(System.Byte[] bytes, int index, int count) {
        throw new Exception("STUB");
    }

    public int GetCharCount(System.Byte[] bytes, int index, int count, boolean flush) {
        throw new Exception("STUB");
    }

    public int GetChars(System.Byte[] bytes, int byteIndex, int byteCount, System.Char[] chars, int charIndex) {
        throw new Exception("STUB");
    }

    public int GetChars(System.Byte[] bytes, int byteIndex, int byteCount, System.Char[] chars, int charIndex, boolean flush) {
        throw new Exception("STUB");
    }

    public void Convert(System.Byte[] bytes, int byteIndex, int byteCount, System.Char[] chars, int charIndex, int charCount, boolean flush, System.Int32& bytesUsed, System.Int32& charsUsed, System.Boolean& completed) {
        throw new Exception("STUB");
    }

}
