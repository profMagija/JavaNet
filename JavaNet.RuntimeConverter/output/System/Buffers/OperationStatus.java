package System.Buffers;
public class OperationStatus {
    public int value__;

    public static final System.Buffers.OperationStatus Done = 0;

    public static final System.Buffers.OperationStatus DestinationTooSmall = 1;

    public static final System.Buffers.OperationStatus NeedMoreData = 2;

    public static final System.Buffers.OperationStatus InvalidData = 3;

}
