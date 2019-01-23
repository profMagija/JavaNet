package System.Threading;
public class Mutex {
    public static final System.Threading.Mutex OpenExisting(String name) {
        throw new Exception("STUB");
    }

    public static final boolean TryOpenExisting(String name, System.Threading.Mutex& result) {
        throw new Exception("STUB");
    }

    public final void ReleaseMutex() {
        throw new Exception("STUB");
    }

}
