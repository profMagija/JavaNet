package System.Threading.Tasks;
public class TaskFactory {
    public final System.Threading.CancellationToken get_CancellationToken() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.TaskScheduler get_Scheduler() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.TaskCreationOptions get_CreationOptions() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.TaskContinuationOptions get_ContinuationOptions() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task StartNew(System.Action action) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task StartNew(System.Action action, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task StartNew(System.Action action, System.Threading.Tasks.TaskCreationOptions creationOptions) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task StartNew(System.Action action, System.Threading.CancellationToken cancellationToken, System.Threading.Tasks.TaskCreationOptions creationOptions, System.Threading.Tasks.TaskScheduler scheduler) {
        throw new Exception("STUB");
    }

}
