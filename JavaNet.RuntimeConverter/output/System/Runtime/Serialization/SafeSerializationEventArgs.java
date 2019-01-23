package System.Runtime.Serialization;
public class SafeSerializationEventArgs {
    public final void AddSerializedState(System.Runtime.Serialization.ISafeSerializationData serializedState) {
        throw new Exception("STUB");
    }

    public final System.Runtime.Serialization.StreamingContext get_StreamingContext() {
        throw new Exception("STUB");
    }

}
