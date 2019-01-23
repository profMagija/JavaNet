package System.Net;
public interface ICredentialsByHost {
    public System.Net.NetworkCredential GetCredential(String host, int port, String authenticationType) {
        throw new Exception("STUB");
    }

}
