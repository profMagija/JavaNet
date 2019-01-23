package System.Net;
public interface ICredentials {
    public System.Net.NetworkCredential GetCredential(System.Uri uri, String authType) {
        throw new Exception("STUB");
    }

}
