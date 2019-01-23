package System;
public class TimeSpan {
    public static final System.TimeSpan Zero;

    public static final System.TimeSpan MaxValue;

    public static final System.TimeSpan MinValue;

    public static final long TicksPerMillisecond = 10000;

    public static final long TicksPerSecond = 10000000;

    public static final long TicksPerMinute = 600000000;

    public static final long TicksPerHour = 36000000000;

    public static final long TicksPerDay = 864000000000;

    public final long get_Ticks() {
        throw new Exception("STUB");
    }

    public final int get_Days() {
        throw new Exception("STUB");
    }

    public final int get_Hours() {
        throw new Exception("STUB");
    }

    public final int get_Milliseconds() {
        throw new Exception("STUB");
    }

    public final int get_Minutes() {
        throw new Exception("STUB");
    }

    public final int get_Seconds() {
        throw new Exception("STUB");
    }

    public final double get_TotalDays() {
        throw new Exception("STUB");
    }

    public final double get_TotalHours() {
        throw new Exception("STUB");
    }

    public final double get_TotalMilliseconds() {
        throw new Exception("STUB");
    }

    public final double get_TotalMinutes() {
        throw new Exception("STUB");
    }

    public final double get_TotalSeconds() {
        throw new Exception("STUB");
    }

    public final System.TimeSpan Add(System.TimeSpan ts) {
        throw new Exception("STUB");
    }

    public static final int Compare(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public int CompareTo(Object value) {
        throw new Exception("STUB");
    }

    public int CompareTo(System.TimeSpan value) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan FromDays(double value) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan Duration() {
        throw new Exception("STUB");
    }

    public boolean Equals(System.TimeSpan obj) {
        throw new Exception("STUB");
    }

    public static final boolean Equals(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan FromHours(double value) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan FromMilliseconds(double value) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan FromMinutes(double value) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan Negate() {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan FromSeconds(double value) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan Subtract(System.TimeSpan ts) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan Multiply(double factor) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan Divide(double divisor) {
        throw new Exception("STUB");
    }

    public final double Divide(System.TimeSpan ts) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan FromTicks(long value) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan Parse(String s) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan Parse(String input, System.IFormatProvider formatProvider) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan ParseExact(String input, String format, System.IFormatProvider formatProvider) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan ParseExact(String input, System.String[] formats, System.IFormatProvider formatProvider) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan ParseExact(String input, String format, System.IFormatProvider formatProvider, System.Globalization.TimeSpanStyles styles) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan ParseExact(String input, System.String[] formats, System.IFormatProvider formatProvider, System.Globalization.TimeSpanStyles styles) {
        throw new Exception("STUB");
    }

    public static final boolean TryParse(String s, System.TimeSpan& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParse(String input, System.IFormatProvider formatProvider, System.TimeSpan& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParseExact(String input, String format, System.IFormatProvider formatProvider, System.TimeSpan& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParseExact(String input, System.String[] formats, System.IFormatProvider formatProvider, System.TimeSpan& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParseExact(String input, String format, System.IFormatProvider formatProvider, System.Globalization.TimeSpanStyles styles, System.TimeSpan& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParseExact(String input, System.String[] formats, System.IFormatProvider formatProvider, System.Globalization.TimeSpanStyles styles, System.TimeSpan& result) {
        throw new Exception("STUB");
    }

    public final String ToString(String format) {
        throw new Exception("STUB");
    }

    public String ToString(String format, System.IFormatProvider formatProvider) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_UnaryNegation(System.TimeSpan t) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_Subtraction(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_UnaryPlus(System.TimeSpan t) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_Addition(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_Multiply(System.TimeSpan timeSpan, double factor) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_Multiply(double factor, System.TimeSpan timeSpan) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_Division(System.TimeSpan timeSpan, double divisor) {
        throw new Exception("STUB");
    }

    public static final double op_Division(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_LessThan(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_LessThanOrEqual(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_GreaterThan(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_GreaterThanOrEqual(System.TimeSpan t1, System.TimeSpan t2) {
        throw new Exception("STUB");
    }

}
