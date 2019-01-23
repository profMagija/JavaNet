package System;
public class DateTime {
    public static final System.DateTime MinValue;

    public static final System.DateTime MaxValue;

    public static final System.DateTime UnixEpoch;

    public static final System.DateTime get_UtcNow() {
        throw new Exception("STUB");
    }

    public final System.DateTime Add(System.TimeSpan value) {
        throw new Exception("STUB");
    }

    public final System.DateTime AddDays(double value) {
        throw new Exception("STUB");
    }

    public final System.DateTime AddHours(double value) {
        throw new Exception("STUB");
    }

    public final System.DateTime AddMilliseconds(double value) {
        throw new Exception("STUB");
    }

    public final System.DateTime AddMinutes(double value) {
        throw new Exception("STUB");
    }

    public final System.DateTime AddMonths(int months) {
        throw new Exception("STUB");
    }

    public final System.DateTime AddSeconds(double value) {
        throw new Exception("STUB");
    }

    public final System.DateTime AddTicks(long value) {
        throw new Exception("STUB");
    }

    public final System.DateTime AddYears(int value) {
        throw new Exception("STUB");
    }

    public static final int Compare(System.DateTime t1, System.DateTime t2) {
        throw new Exception("STUB");
    }

    public int CompareTo(Object value) {
        throw new Exception("STUB");
    }

    public int CompareTo(System.DateTime value) {
        throw new Exception("STUB");
    }

    public static final int DaysInMonth(int year, int month) {
        throw new Exception("STUB");
    }

    public boolean Equals(System.DateTime value) {
        throw new Exception("STUB");
    }

    public static final boolean Equals(System.DateTime t1, System.DateTime t2) {
        throw new Exception("STUB");
    }

    public static final System.DateTime FromBinary(long dateData) {
        throw new Exception("STUB");
    }

    public static final System.DateTime FromFileTime(long fileTime) {
        throw new Exception("STUB");
    }

    public static final System.DateTime FromFileTimeUtc(long fileTime) {
        throw new Exception("STUB");
    }

    public static final System.DateTime FromOADate(double d) {
        throw new Exception("STUB");
    }

    public final boolean IsDaylightSavingTime() {
        throw new Exception("STUB");
    }

    public static final System.DateTime SpecifyKind(System.DateTime value, System.DateTimeKind kind) {
        throw new Exception("STUB");
    }

    public final long ToBinary() {
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

    public final System.DateTimeKind get_Kind() {
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

    public static final System.DateTime get_Now() {
        throw new Exception("STUB");
    }

    public final int get_Second() {
        throw new Exception("STUB");
    }

    public final long get_Ticks() {
        throw new Exception("STUB");
    }

    public final System.TimeSpan get_TimeOfDay() {
        throw new Exception("STUB");
    }

    public static final System.DateTime get_Today() {
        throw new Exception("STUB");
    }

    public final int get_Year() {
        throw new Exception("STUB");
    }

    public static final boolean IsLeapYear(int year) {
        throw new Exception("STUB");
    }

    public static final System.DateTime Parse(String s) {
        throw new Exception("STUB");
    }

    public static final System.DateTime Parse(String s, System.IFormatProvider provider) {
        throw new Exception("STUB");
    }

    public static final System.DateTime Parse(String s, System.IFormatProvider provider, System.Globalization.DateTimeStyles styles) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ParseExact(String s, String format, System.IFormatProvider provider) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ParseExact(String s, String format, System.IFormatProvider provider, System.Globalization.DateTimeStyles style) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ParseExact(String s, System.String[] formats, System.IFormatProvider provider, System.Globalization.DateTimeStyles style) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan Subtract(System.DateTime value) {
        throw new Exception("STUB");
    }

    public final System.DateTime Subtract(System.TimeSpan value) {
        throw new Exception("STUB");
    }

    public final double ToOADate() {
        throw new Exception("STUB");
    }

    public final long ToFileTime() {
        throw new Exception("STUB");
    }

    public final long ToFileTimeUtc() {
        throw new Exception("STUB");
    }

    public final System.DateTime ToLocalTime() {
        throw new Exception("STUB");
    }

    public final String ToLongDateString() {
        throw new Exception("STUB");
    }

    public final String ToLongTimeString() {
        throw new Exception("STUB");
    }

    public final String ToShortDateString() {
        throw new Exception("STUB");
    }

    public final String ToShortTimeString() {
        throw new Exception("STUB");
    }

    public final String ToString(String format) {
        throw new Exception("STUB");
    }

    public String ToString(System.IFormatProvider provider) {
        throw new Exception("STUB");
    }

    public String ToString(String format, System.IFormatProvider provider) {
        throw new Exception("STUB");
    }

    public final System.DateTime ToUniversalTime() {
        throw new Exception("STUB");
    }

    public static final boolean TryParse(String s, System.DateTime& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParse(String s, System.IFormatProvider provider, System.Globalization.DateTimeStyles styles, System.DateTime& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParseExact(String s, String format, System.IFormatProvider provider, System.Globalization.DateTimeStyles style, System.DateTime& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryParseExact(String s, System.String[] formats, System.IFormatProvider provider, System.Globalization.DateTimeStyles style, System.DateTime& result) {
        throw new Exception("STUB");
    }

    public static final System.DateTime op_Addition(System.DateTime d, System.TimeSpan t) {
        throw new Exception("STUB");
    }

    public static final System.DateTime op_Subtraction(System.DateTime d, System.TimeSpan t) {
        throw new Exception("STUB");
    }

    public static final System.TimeSpan op_Subtraction(System.DateTime d1, System.DateTime d2) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.DateTime d1, System.DateTime d2) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.DateTime d1, System.DateTime d2) {
        throw new Exception("STUB");
    }

    public static final boolean op_LessThan(System.DateTime t1, System.DateTime t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_LessThanOrEqual(System.DateTime t1, System.DateTime t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_GreaterThan(System.DateTime t1, System.DateTime t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_GreaterThanOrEqual(System.DateTime t1, System.DateTime t2) {
        throw new Exception("STUB");
    }

    public final System.String[] GetDateTimeFormats() {
        throw new Exception("STUB");
    }

    public final System.String[] GetDateTimeFormats(System.IFormatProvider provider) {
        throw new Exception("STUB");
    }

    public final System.String[] GetDateTimeFormats(char format) {
        throw new Exception("STUB");
    }

    public final System.String[] GetDateTimeFormats(char format, System.IFormatProvider provider) {
        throw new Exception("STUB");
    }

    public System.TypeCode GetTypeCode() {
        throw new Exception("STUB");
    }

}
