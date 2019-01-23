package System.Threading.Tasks.Sources;
public interface IValueTaskSource {
    public System.Threading.Tasks.Sources.ValueTaskSourceStatus GetStatus(short token) {
        throw new Exception("STUB");
    }

    public void GetResult(short token) {
        throw new Exception("STUB");
    }

}
