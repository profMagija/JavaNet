package System;
public class DateTimeOffset {
    public static final System.DateTimeOffset MinValue;

    public static final System.DateTimeOffset MaxValue;

    public static final System.DateTimeOffset UnixEpoch;

    public static final System.DateTimeOffset get_Now() {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset get_UtcNow() {
        throw new Exception("STUB");
    }

    public final System.DateTime get_DateTime() {
        throw new Exception("STUB");
    }

    public final System.DateTime get_UtcDateTime() {
        throw new Exception("STUB");
    }

    public final System.DateTime get_LocalDateTime() {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset ToOffset(System.TimeSpan offset) {
        throw new Exception("STUB");
    }

    public final System.DateTime get_Date() {
        throw new Exception("STUB");
    }

    public final int get_Day() {
        throw new Exception("STUB");
    }

    public final System.DayOfWeek get_DayOfWeek() {
        throw new Exception("STUB");
    }

    public final int get_DayOfYear() {
        throw new Exception("STUB");
    }

    public final int get_Hour() {
        throw new Exception("STUB");
    }

    public final int get_Millisecond() {
        throw new Exception("STUB");
    }

    public final int get_Minute() {
        throw new Exception("STUB");
    }

    public final int get_Month() {
        throw new Exception("STUB");
    }

    public final System.TimeSpan get_Offset() {
        throw new Exception("STUB");
    }

    public final int get_Second() {
        throw new Exception("STUB");
    }

    public final long get_Ticks() {
        throw new Exception("STUB");
    }

    public final long get_UtcTicks() {
        throw new Exception("STUB");
    }

    public final System.TimeSpan get_TimeOfDay() {
        throw new Exception("STUB");
    }

    public final int get_Year() {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset Add(System.TimeSpan timeSpan) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset AddDays(double days) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset AddHours(double hours) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset AddMilliseconds(double milliseconds) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset AddMinutes(double minutes) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset AddMonths(int months) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset AddSeconds(double seconds) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset AddTicks(long ticks) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset AddYears(int years) {
        throw new Exception("STUB");
    }

    public static final int Compare(System.DateTimeOffset first, System.DateTimeOffset second) {
        throw new Exception("STUB");
    }

    public int CompareTo(System.DateTimeOffset other) {
        throw new Exception("STUB");
    }

    public boolean Equals(System.DateTimeOffset other) {
        throw new Exception("STUB");
    }

    public final boolean EqualsExact(System.DateTimeOffset other) {
        throw new Exception("STUB");
    }

    public static final boolean Equals(System.DateTimeOffset first, System.DateTimeOffset second) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset FromFileTime(long fileTime) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset FromUnixTimeSeconds(long seconds) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset FromUnixTimeMilliseconds(long milliseconds) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset Parse(String input) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset Parse(String input, System.IFormatProvider formatProvider) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset Parse(String input, System.IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset ParseExact(String input, String format, System.IFormatProvider formatProvider) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset ParseExact(String input, String format, System.IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset ParseExact(String input, System.String[] formats, System.IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan Subtract(System.DateTimeOffset value) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset Subtract(System.TimeSpan value) {
        throw new Exception("STUB");
    }

    public final long ToFileTime() {
        throw new Exception("STUB");
    }

    public final long ToUnixTimeSeconds() {
        throw new Exception("STUB");
    }

    public final long ToUnixTimeMilliseconds() {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset ToLocalTime() {
        throw new Exception("STUB");
    }

    public final String ToString(String format) {
        throw new Exception("STUB");
    }

    public final String ToString(System.IFormatProvider formatProvider) {
        throw new Exception("STUB");
    }

    public String ToString(String format, System.IFormatProvider formatProvider) {
        throw new Exception("STUB");
    }

    public final System.DateTimeOffset ToUniversalTime() {
        throw new Exception("STUB");
    }

    public static final boolean TryParse(String input, System.DateTimeOffset& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParse(String input, System.IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles, System.DateTimeOffset& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParseExact(String input, String format, System.IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles, System.DateTimeOffset& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParseExact(String input, System.String[] formats, System.IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles, System.DateTimeOffset& result) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset op_Implicit(System.DateTime dateTime) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset op_Addition(System.DateTimeOffset dateTimeOffset, System.TimeSpan timeSpan) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset op_Subtraction(System.DateTimeOffset dateTimeOffset, System.TimeSpan timeSpan) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_Subtraction(System.DateTimeOffset left, System.DateTimeOffset right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.DateTimeOffset left, System.DateTimeOffset right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.DateTimeOffset left, System.DateTimeOffset right) {
        throw new Exception("STUB");
    }

    public static final boolean op_LessThan(System.DateTimeOffset left, System.DateTimeOffset right) {
        throw new Exception("STUB");
    }

    public static final boolean op_LessThanOrEqual(System.DateTimeOffset left, System.DateTimeOffset right) {
        throw new Exception("STUB");
    }

    public static final boolean op_GreaterThan(System.DateTimeOffset left, System.DateTimeOffset right) {
        throw new Exception("STUB");
    }

    public static final boolean op_GreaterThanOrEqual(System.DateTimeOffset left, System.DateTimeOffset right) {
        throw new Exception("STUB");
    }

}
