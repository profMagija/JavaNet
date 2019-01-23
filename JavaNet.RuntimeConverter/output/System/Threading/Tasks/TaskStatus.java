package System.Threading.Tasks;
public class TaskStatus {
    public int value__;

    public static final System.Threading.Tasks.TaskStatus Created = 0;

    public static final System.Threading.Tasks.TaskStatus WaitingForActivation = 1;

    public static final System.Threading.Tasks.TaskStatus WaitingToRun = 2;

    public static final System.Threading.Tasks.TaskStatus Running = 3;

    public static final System.Threading.Tasks.TaskStatus WaitingForChildrenToComplete = 4;

    public static final System.Threading.Tasks.TaskStatus RanToCompletion = 5;

    public static final System.Threading.Tasks.TaskStatus Canceled = 6;

    public static final System.Threading.Tasks.TaskStatus Faulted = 7;

}
