package System.Net;
public class AuthenticationManager {
    public static final System.Net.ICredentialPolicy get_CredentialPolicy() {
        throw new Exception("STUB");
    }

    public static final void set_CredentialPolicy(System.Net.ICredentialPolicy value) {
        throw new Exception("STUB");
    }

    public static final System.Collections.Specialized.StringDictionary get_CustomTargetNameDictionary() {
        throw new Exception("STUB");
    }

    public static final System.Net.Authorization Authenticate(String challenge, System.Net.WebRequest request, System.Net.ICredentials credentials) {
        throw new Exception("STUB");
    }

    public static final System.Net.Authorization PreAuthenticate(System.Net.WebRequest request, System.Net.ICredentials credentials) {
        throw new Exception("STUB");
    }

    public static final void Register(System.Net.IAuthenticationModule authenticationModule) {
        throw new Exception("STUB");
    }

    public static final void Unregister(System.Net.IAuthenticationModule authenticationModule) {
        throw new Exception("STUB");
    }

    public static final void Unregister(String authenticationScheme) {
        throw new Exception("STUB");
    }

    public static final System.Collections.IEnumerator get_RegisteredModules() {
        throw new Exception("STUB");
    }

}
