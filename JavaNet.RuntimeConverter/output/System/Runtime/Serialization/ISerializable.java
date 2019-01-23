package System.Runtime.Serialization;
public interface ISerializable {
    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {
        throw new Exception("STUB");
    }

}
