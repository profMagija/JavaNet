package System.Threading.Tasks;
public class Task {
    public final void Start() {
        throw new Exception("STUB");
    }

    public final void Start(System.Threading.Tasks.TaskScheduler scheduler) {
        throw new Exception("STUB");
    }

    public final void RunSynchronously() {
        throw new Exception("STUB");
    }

    public final void RunSynchronously(System.Threading.Tasks.TaskScheduler scheduler) {
        throw new Exception("STUB");
    }

    public final int get_Id() {
        throw new Exception("STUB");
    }

    public final System.AggregateException get_Exception() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.TaskStatus get_Status() {
        throw new Exception("STUB");
    }

    public final boolean get_IsCanceled() {
        throw new Exception("STUB");
    }

    public boolean get_IsCompleted() {
        throw new Exception("STUB");
    }

    public final boolean get_IsCompletedSuccessfully() {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.TaskCreationOptions get_CreationOptions() {
        throw new Exception("STUB");
    }

    public Object get_AsyncState() {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.TaskFactory get_Factory() {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task get_CompletedTask() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFaulted() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final System.Runtime.CompilerServices.TaskAwaiter GetAwaiter() {
        throw new Exception("STUB");
    }

    public final System.Runtime.CompilerServices.ConfiguredTaskAwaitable ConfigureAwait(boolean continueOnCapturedContext) {
        throw new Exception("STUB");
    }

    public static final System.Runtime.CompilerServices.YieldAwaitable Yield() {
        throw new Exception("STUB");
    }

    public final void Wait() {
        throw new Exception("STUB");
    }

    public final boolean Wait(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public final void Wait(System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public final boolean Wait(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public final boolean Wait(int millisecondsTimeout, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final void WaitAll(System.Threading.Tasks.Task[] tasks) {
        throw new Exception("STUB");
    }

    public static final boolean WaitAll(System.Threading.Tasks.Task[] tasks, System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public static final boolean WaitAll(System.Threading.Tasks.Task[] tasks, int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final void WaitAll(System.Threading.Tasks.Task[] tasks, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final boolean WaitAll(System.Threading.Tasks.Task[] tasks, int millisecondsTimeout, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.Tasks.Task[] tasks) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.Tasks.Task[] tasks, System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.Tasks.Task[] tasks, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.Tasks.Task[] tasks, int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final int WaitAny(System.Threading.Tasks.Task[] tasks, int millisecondsTimeout, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task FromException(System.Exception exception) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task FromCanceled(System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task Run(System.Action action) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task Run(System.Action action, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task Delay(System.TimeSpan delay) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task Delay(System.TimeSpan delay, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task Delay(int millisecondsDelay) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task Delay(int millisecondsDelay, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task WhenAll(System.Threading.Tasks.Task[] tasks) {
        throw new Exception("STUB");
    }

}
