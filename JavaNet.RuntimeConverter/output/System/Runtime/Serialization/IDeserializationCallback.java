package System.Runtime.Serialization;
public interface IDeserializationCallback {
    public void OnDeserialization(Object sender) {
        throw new Exception("STUB");
    }

}
