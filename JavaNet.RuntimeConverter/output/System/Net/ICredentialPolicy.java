package System.Net;
public interface ICredentialPolicy {
    public boolean ShouldSendCredential(System.Uri challengeUri, System.Net.WebRequest request, System.Net.NetworkCredential credential, System.Net.IAuthenticationModule authenticationModule) {
        throw new Exception("STUB");
    }

}
