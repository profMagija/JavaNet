package System.Threading.Tasks;
public class TaskCreationOptions {
    public int value__;

    public static final System.Threading.Tasks.TaskCreationOptions None = 0;

    public static final System.Threading.Tasks.TaskCreationOptions PreferFairness = 1;

    public static final System.Threading.Tasks.TaskCreationOptions LongRunning = 2;

    public static final System.Threading.Tasks.TaskCreationOptions AttachedToParent = 4;

    public static final System.Threading.Tasks.TaskCreationOptions DenyChildAttach = 8;

    public static final System.Threading.Tasks.TaskCreationOptions HideScheduler = 16;

    public static final System.Threading.Tasks.TaskCreationOptions RunContinuationsAsynchronously = 64;

}
