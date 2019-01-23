package System.Threading;
public class Monitor {
    public static final void Enter(Object obj) {
        throw new Exception("STUB");
    }

    public static final void Enter(Object obj, System.Boolean& lockTaken) {
        throw new Exception("STUB");
    }

    public static final void Exit(Object obj) {
        throw new Exception("STUB");
    }

    public static final boolean TryEnter(Object obj) {
        throw new Exception("STUB");
    }

    public static final void TryEnter(Object obj, System.Boolean& lockTaken) {
        throw new Exception("STUB");
    }

    public static final boolean TryEnter(Object obj, int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final boolean TryEnter(Object obj, System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public static final void TryEnter(Object obj, int millisecondsTimeout, System.Boolean& lockTaken) {
        throw new Exception("STUB");
    }

    public static final void TryEnter(Object obj, System.TimeSpan timeout, System.Boolean& lockTaken) {
        throw new Exception("STUB");
    }

    public static final boolean IsEntered(Object obj) {
        throw new Exception("STUB");
    }

    public static final boolean Wait(Object obj, int millisecondsTimeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public static final boolean Wait(Object obj, System.TimeSpan timeout, boolean exitContext) {
        throw new Exception("STUB");
    }

    public static final boolean Wait(Object obj, int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final boolean Wait(Object obj, System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public static final boolean Wait(Object obj) {
        throw new Exception("STUB");
    }

    public static final void Pulse(Object obj) {
        throw new Exception("STUB");
    }

    public static final void PulseAll(Object obj) {
        throw new Exception("STUB");
    }

}
