package System.Threading;
public class ThreadState {
    public int value__;

    public static final System.Threading.ThreadState Running = 0;

    public static final System.Threading.ThreadState StopRequested = 1;

    public static final System.Threading.ThreadState SuspendRequested = 2;

    public static final System.Threading.ThreadState Background = 4;

    public static final System.Threading.ThreadState Unstarted = 8;

    public static final System.Threading.ThreadState Stopped = 16;

    public static final System.Threading.ThreadState WaitSleepJoin = 32;

    public static final System.Threading.ThreadState Suspended = 64;

    public static final System.Threading.ThreadState AbortRequested = 128;

    public static final System.Threading.ThreadState Aborted = 256;

}
