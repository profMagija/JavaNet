package System.Text;
public class Encoding {
    public int GetByteCount(System.Char[] chars, int index, int count) {
        throw new Exception("STUB");
    }

    public int GetByteCount(String s) {
        throw new Exception("STUB");
    }

    public int GetBytes(String s, int charIndex, int charCount, System.Byte[] bytes, int byteIndex) {
        throw new Exception("STUB");
    }

    public int GetBytes(System.Char[] chars, int charIndex, int charCount, System.Byte[] bytes, int byteIndex) {
        throw new Exception("STUB");
    }

    public int GetCharCount(System.Byte[] bytes, int index, int count) {
        throw new Exception("STUB");
    }

    public int GetChars(System.Byte[] bytes, int byteIndex, int byteCount, System.Char[] chars, int charIndex) {
        throw new Exception("STUB");
    }

    public String GetString(System.Byte[] bytes, int index, int count) {
        throw new Exception("STUB");
    }

    public int GetMaxByteCount(int charCount) {
        throw new Exception("STUB");
    }

    public int GetMaxCharCount(int byteCount) {
        throw new Exception("STUB");
    }

    public boolean get_IsSingleByte() {
        throw new Exception("STUB");
    }

    public System.Text.Decoder GetDecoder() {
        throw new Exception("STUB");
    }

    public System.Text.Encoder GetEncoder() {
        throw new Exception("STUB");
    }

    public System.Byte[] GetPreamble() {
        throw new Exception("STUB");
    }

    public String get_BodyName() {
        throw new Exception("STUB");
    }

    public String get_EncodingName() {
        throw new Exception("STUB");
    }

    public String get_HeaderName() {
        throw new Exception("STUB");
    }

    public String get_WebName() {
        throw new Exception("STUB");
    }

    public int get_WindowsCodePage() {
        throw new Exception("STUB");
    }

    public boolean get_IsBrowserDisplay() {
        throw new Exception("STUB");
    }

    public boolean get_IsBrowserSave() {
        throw new Exception("STUB");
    }

    public boolean get_IsMailNewsDisplay() {
        throw new Exception("STUB");
    }

    public boolean get_IsMailNewsSave() {
        throw new Exception("STUB");
    }

    public Object Clone() {
        throw new Exception("STUB");
    }

    public int GetByteCount(System.Char[] chars) {
        throw new Exception("STUB");
    }

    public System.Byte[] GetBytes(System.Char[] chars) {
        throw new Exception("STUB");
    }

    public System.Byte[] GetBytes(System.Char[] chars, int index, int count) {
        throw new Exception("STUB");
    }

    public System.Byte[] GetBytes(String s) {
        throw new Exception("STUB");
    }

    public int GetCharCount(System.Byte[] bytes) {
        throw new Exception("STUB");
    }

    public System.Char[] GetChars(System.Byte[] bytes) {
        throw new Exception("STUB");
    }

    public System.Char[] GetChars(System.Byte[] bytes, int index, int count) {
        throw new Exception("STUB");
    }

    public int get_CodePage() {
        throw new Exception("STUB");
    }

    public boolean IsAlwaysNormalized(System.Text.NormalizationForm form) {
        throw new Exception("STUB");
    }

    public String GetString(System.Byte[] bytes) {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding get_Default() {
        throw new Exception("STUB");
    }

    public static final System.Byte[] Convert(System.Text.Encoding srcEncoding, System.Text.Encoding dstEncoding, System.Byte[] bytes) {
        throw new Exception("STUB");
    }

    public static final System.Byte[] Convert(System.Text.Encoding srcEncoding, System.Text.Encoding dstEncoding, System.Byte[] bytes, int index, int count) {
        throw new Exception("STUB");
    }

    public static final void RegisterProvider(System.Text.EncodingProvider provider) {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding GetEncoding(int codepage) {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding GetEncoding(int codepage, System.Text.EncoderFallback encoderFallback, System.Text.DecoderFallback decoderFallback) {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding GetEncoding(String name) {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding GetEncoding(String name, System.Text.EncoderFallback encoderFallback, System.Text.DecoderFallback decoderFallback) {
        throw new Exception("STUB");
    }

    public static final System.Text.EncodingInfo[] GetEncodings() {
        throw new Exception("STUB");
    }

    public final System.Text.EncoderFallback get_EncoderFallback() {
        throw new Exception("STUB");
    }

    public final void set_EncoderFallback(System.Text.EncoderFallback value) {
        throw new Exception("STUB");
    }

    public final System.Text.DecoderFallback get_DecoderFallback() {
        throw new Exception("STUB");
    }

    public final void set_DecoderFallback(System.Text.DecoderFallback value) {
        throw new Exception("STUB");
    }

    public final boolean get_IsReadOnly() {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding get_ASCII() {
        throw new Exception("STUB");
    }

    public final int GetByteCount(String s, int index, int count) {
        throw new Exception("STUB");
    }

    public final System.Byte[] GetBytes(String s, int index, int count) {
        throw new Exception("STUB");
    }

    public final boolean IsAlwaysNormalized() {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding get_Unicode() {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding get_BigEndianUnicode() {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding get_UTF7() {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding get_UTF8() {
        throw new Exception("STUB");
    }

    public static final System.Text.Encoding get_UTF32() {
        throw new Exception("STUB");
    }

}
