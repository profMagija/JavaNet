package System.Net;
public class CredentialCache {
    public final void Add(System.Uri uriPrefix, String authType, System.Net.NetworkCredential cred) {
        throw new Exception("STUB");
    }

    public final void Add(String host, int port, String authenticationType, System.Net.NetworkCredential credential) {
        throw new Exception("STUB");
    }

    public final void Remove(System.Uri uriPrefix, String authType) {
        throw new Exception("STUB");
    }

    public final void Remove(String host, int port, String authenticationType) {
        throw new Exception("STUB");
    }

    public System.Net.NetworkCredential GetCredential(System.Uri uriPrefix, String authType) {
        throw new Exception("STUB");
    }

    public System.Net.NetworkCredential GetCredential(String host, int port, String authenticationType) {
        throw new Exception("STUB");
    }

    public System.Collections.IEnumerator GetEnumerator() {
        throw new Exception("STUB");
    }

    public static final System.Net.ICredentials get_DefaultCredentials() {
        throw new Exception("STUB");
    }

    public static final System.Net.NetworkCredential get_DefaultNetworkCredentials() {
        throw new Exception("STUB");
    }

}
