package System.Runtime.ConstrainedExecution;
public class Consistency {
    public int value__;

    public static final System.Runtime.ConstrainedExecution.Consistency MayCorruptProcess = 0;

    public static final System.Runtime.ConstrainedExecution.Consistency MayCorruptAppDomain = 1;

    public static final System.Runtime.ConstrainedExecution.Consistency MayCorruptInstance = 2;

    public static final System.Runtime.ConstrainedExecution.Consistency WillNotCorruptState = 3;

}
