package System.Runtime.Serialization;
public interface ISafeSerializationData {
    public void CompleteDeserialization(Object deserialized) {
        throw new Exception("STUB");
    }

}
