package System.Net;
public interface IAuthenticationModule {
    public System.Net.Authorization Authenticate(String challenge, System.Net.WebRequest request, System.Net.ICredentials credentials) {
        throw new Exception("STUB");
    }

    public System.Net.Authorization PreAuthenticate(System.Net.WebRequest request, System.Net.ICredentials credentials) {
        throw new Exception("STUB");
    }

    public boolean get_CanPreAuthenticate() {
        throw new Exception("STUB");
    }

    public String get_AuthenticationType() {
        throw new Exception("STUB");
    }

}
