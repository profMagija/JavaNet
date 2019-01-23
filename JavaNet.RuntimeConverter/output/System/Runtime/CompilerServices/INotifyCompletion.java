package System.Runtime.CompilerServices;
public interface INotifyCompletion {
    public void OnCompleted(System.Action continuation) {
        throw new Exception("STUB");
    }

}
