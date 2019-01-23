package System.Threading.Tasks;
public class TaskContinuationOptions {
    public int value__;

    public static final System.Threading.Tasks.TaskContinuationOptions None = 0;

    public static final System.Threading.Tasks.TaskContinuationOptions PreferFairness = 1;

    public static final System.Threading.Tasks.TaskContinuationOptions LongRunning = 2;

    public static final System.Threading.Tasks.TaskContinuationOptions AttachedToParent = 4;

    public static final System.Threading.Tasks.TaskContinuationOptions DenyChildAttach = 8;

    public static final System.Threading.Tasks.TaskContinuationOptions HideScheduler = 16;

    public static final System.Threading.Tasks.TaskContinuationOptions LazyCancellation = 32;

    public static final System.Threading.Tasks.TaskContinuationOptions RunContinuationsAsynchronously = 64;

    public static final System.Threading.Tasks.TaskContinuationOptions NotOnRanToCompletion = 65536;

    public static final System.Threading.Tasks.TaskContinuationOptions NotOnFaulted = 131072;

    public static final System.Threading.Tasks.TaskContinuationOptions NotOnCanceled = 262144;

    public static final System.Threading.Tasks.TaskContinuationOptions OnlyOnRanToCompletion = 393216;

    public static final System.Threading.Tasks.TaskContinuationOptions OnlyOnFaulted = 327680;

    public static final System.Threading.Tasks.TaskContinuationOptions OnlyOnCanceled = 196608;

    public static final System.Threading.Tasks.TaskContinuationOptions ExecuteSynchronously = 524288;

}
