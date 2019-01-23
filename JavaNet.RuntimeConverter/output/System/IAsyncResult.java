package System;
public interface IAsyncResult {
    public boolean get_IsCompleted() {
        throw new Exception("STUB");
    }

    public System.Threading.WaitHandle get_AsyncWaitHandle() {
        throw new Exception("STUB");
    }

    public Object get_AsyncState() {
        throw new Exception("STUB");
    }

    public boolean get_CompletedSynchronously() {
        throw new Exception("STUB");
    }

}
