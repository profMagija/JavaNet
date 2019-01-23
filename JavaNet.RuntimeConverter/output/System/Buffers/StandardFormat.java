package System.Buffers;
public class StandardFormat {
    public static final byte NoPrecision = 255;

    public static final byte MaxPrecision = 99;

    public final char get_Symbol() {
        throw new Exception("STUB");
    }

    public final byte get_Precision() {
        throw new Exception("STUB");
    }

    public final boolean get_HasPrecision() {
        throw new Exception("STUB");
    }

    public final boolean get_IsDefault() {
        throw new Exception("STUB");
    }

    public static final System.Buffers.StandardFormat op_Implicit(char symbol) {
        throw new Exception("STUB");
    }

    public static final System.Buffers.StandardFormat Parse(String format) {
        throw new Exception("STUB");
    }

    public boolean Equals(System.Buffers.StandardFormat other) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Buffers.StandardFormat left, System.Buffers.StandardFormat right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Buffers.StandardFormat left, System.Buffers.StandardFormat right) {
        throw new Exception("STUB");
    }

}
