package System;
public class TransitionTime {
    public final System.DateTime get_TimeOfDay() {
        throw new Exception("STUB");
    }

    public final int get_Month() {
        throw new Exception("STUB");
    }

    public final int get_Week() {
        throw new Exception("STUB");
    }

    public final int get_Day() {
        throw new Exception("STUB");
    }

    public final System.DayOfWeek get_DayOfWeek() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFixedDateRule() {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.TimeZoneInfo+TransitionTime t1, System.TimeZoneInfo+TransitionTime t2) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.TimeZoneInfo+TransitionTime t1, System.TimeZoneInfo+TransitionTime t2) {
        throw new Exception("STUB");
    }

    public boolean Equals(System.TimeZoneInfo+TransitionTime other) {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo+TransitionTime CreateFixedDateRule(System.DateTime timeOfDay, int month, int day) {
        throw new Exception("STUB");
    }

    public static final System.TimeZoneInfo+TransitionTime CreateFloatingDateRule(System.DateTime timeOfDay, int month, int week, System.DayOfWeek dayOfWeek) {
        throw new Exception("STUB");
    }

}
