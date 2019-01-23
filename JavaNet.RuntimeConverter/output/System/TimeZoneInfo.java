package System;
public class TimeZoneInfo {
    public final String get_Id() {
        throw new Exception("STUB");
    }

    public final String get_DisplayName() {
        throw new Exception("STUB");
    }

    public final String get_StandardName() {
        throw new Exception("STUB");
    }

    public final String get_DaylightName() {
        throw new Exception("STUB");
    }

    public final System.TimeSpan get_BaseUtcOffset() {
        throw new Exception("STUB");
    }

    public final boolean get_SupportsDaylightSavingTime() {
        throw new Exception("STUB");
    }

    public final System.TimeSpan[] GetAmbiguousTimeOffsets(System.DateTimeOffset dateTimeOffset) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan[] GetAmbiguousTimeOffsets(System.DateTime dateTime) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan GetUtcOffset(System.DateTimeOffset dateTimeOffset) {
        throw new Exception("STUB");
    }

    public final System.TimeSpan GetUtcOffset(System.DateTime dateTime) {
        throw new Exception("STUB");
    }

    public final boolean IsAmbiguousTime(System.DateTimeOffset dateTimeOffset) {
        throw new Exception("STUB");
    }

    public final boolean IsAmbiguousTime(System.DateTime dateTime) {
        throw new Exception("STUB");
    }

    public final boolean IsDaylightSavingTime(System.DateTimeOffset dateTimeOffset) {
        throw new Exception("STUB");
    }

    public final boolean IsDaylightSavingTime(System.DateTime dateTime) {
        throw new Exception("STUB");
    }

    public final boolean IsInvalidTime(System.DateTime dateTime) {
        throw new Exception("STUB");
    }

    public static final void ClearCachedData() {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset ConvertTimeBySystemTimeZoneId(System.DateTimeOffset dateTimeOffset, String destinationTimeZoneId) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ConvertTimeBySystemTimeZoneId(System.DateTime dateTime, String destinationTimeZoneId) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ConvertTimeBySystemTimeZoneId(System.DateTime dateTime, String sourceTimeZoneId, String destinationTimeZoneId) {
        throw new Exception("STUB");
    }

    public static final System.DateTimeOffset ConvertTime(System.DateTimeOffset dateTimeOffset, System.TimeZoneInfo destinationTimeZone) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ConvertTime(System.DateTime dateTime, System.TimeZoneInfo destinationTimeZone) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ConvertTime(System.DateTime dateTime, System.TimeZoneInfo sourceTimeZone, System.TimeZoneInfo destinationTimeZone) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ConvertTimeFromUtc(System.DateTime dateTime, System.TimeZoneInfo destinationTimeZone) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ConvertTimeToUtc(System.DateTime dateTime) {
        throw new Exception("STUB");
    }

    public static final System.DateTime ConvertTimeToUtc(System.DateTime dateTime, System.TimeZoneInfo sourceTimeZone) {
        throw new Exception("STUB");
    }

    public boolean Equals(System.TimeZoneInfo other) {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo FromSerializedString(String source) {
        throw new Exception("STUB");
    }

    public final boolean HasSameRules(System.TimeZoneInfo other) {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo get_Local() {
        throw new Exception("STUB");
    }

    public final String ToSerializedString() {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo get_Utc() {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo CreateCustomTimeZone(String id, System.TimeSpan baseUtcOffset, String displayName, String standardDisplayName) {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo CreateCustomTimeZone(String id, System.TimeSpan baseUtcOffset, String displayName, String standardDisplayName, String daylightDisplayName, System.TimeZoneInfo+AdjustmentRule[] adjustmentRules) {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo CreateCustomTimeZone(String id, System.TimeSpan baseUtcOffset, String displayName, String standardDisplayName, String daylightDisplayName, System.TimeZoneInfo+AdjustmentRule[] adjustmentRules, boolean disableDaylightSavingTime) {
        throw new Exception("STUB");
    }

    public final System.TimeZoneInfo+AdjustmentRule[] GetAdjustmentRules() {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo FindSystemTimeZoneById(String id) {
        throw new Exception("STUB");
    }

}
