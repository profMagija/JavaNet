package System;
public class TimeZone {
    public static final System.TimeZone get_CurrentTimeZone() {
        throw new Exception("STUB");
    }

    public String get_StandardName() {
        throw new Exception("STUB");
    }

    public String get_DaylightName() {
        throw new Exception("STUB");
    }

    public System.TimeSpan GetUtcOffset(System.DateTime time) {
        throw new Exception("STUB");
    }

    public System.DateTime ToUniversalTime(System.DateTime time) {
        throw new Exception("STUB");
    }

    public System.DateTime ToLocalTime(System.DateTime time) {
        throw new Exception("STUB");
    }

    public System.Globalization.DaylightTime GetDaylightChanges(int year) {
        throw new Exception("STUB");
    }

    public boolean IsDaylightSavingTime(System.DateTime time) {
        throw new Exception("STUB");
    }

    public static final boolean IsDaylightSavingTime(System.DateTime time, System.Globalization.DaylightTime daylightTimes) {
        throw new Exception("STUB");
    }

}
