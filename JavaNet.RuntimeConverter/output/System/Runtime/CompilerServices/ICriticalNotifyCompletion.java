package System.Runtime.CompilerServices;
public interface ICriticalNotifyCompletion {
    public void UnsafeOnCompleted(System.Action continuation) {
        throw new Exception("STUB");
    }

}
