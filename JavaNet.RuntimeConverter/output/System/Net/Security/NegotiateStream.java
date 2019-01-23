package System.Net.Security;
public class NegotiateStream {
    public System.IAsyncResult BeginAuthenticateAsClient(System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsClient(System.Net.NetworkCredential credential, String targetName, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsClient(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, String targetName, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsClient(System.Net.NetworkCredential credential, String targetName, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsClient(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, String targetName, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public void EndAuthenticateAsClient(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsServer() {
        throw new Exception("STUB");
    }

    public void AuthenticateAsServer(System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsServer(System.Net.NetworkCredential credential, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsServer(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsServer(System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsServer(System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsServer(System.Net.NetworkCredential credential, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginAuthenticateAsServer(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel, System.AsyncCallback asyncCallback, Object asyncState) {
        throw new Exception("STUB");
    }

    public void EndAuthenticateAsServer(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsClient() {
        throw new Exception("STUB");
    }

    public void AuthenticateAsClient(System.Net.NetworkCredential credential, String targetName) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsClient(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, String targetName) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsClient(System.Net.NetworkCredential credential, String targetName, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel) {
        throw new Exception("STUB");
    }

    public void AuthenticateAsClient(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, String targetName, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsClientAsync() {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsClientAsync(System.Net.NetworkCredential credential, String targetName) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsClientAsync(System.Net.NetworkCredential credential, String targetName, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsClientAsync(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, String targetName) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsClientAsync(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ChannelBinding binding, String targetName, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel allowedImpersonationLevel) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsServerAsync() {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsServerAsync(System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsServerAsync(System.Net.NetworkCredential credential, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel) {
        throw new Exception("STUB");
    }

    public System.Threading.Tasks.Task AuthenticateAsServerAsync(System.Net.NetworkCredential credential, System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy policy, System.Net.Security.ProtectionLevel requiredProtectionLevel, System.Security.Principal.TokenImpersonationLevel requiredImpersonationLevel) {
        throw new Exception("STUB");
    }

    public System.Security.Principal.TokenImpersonationLevel get_ImpersonationLevel() {
        throw new Exception("STUB");
    }

    public System.Security.Principal.IIdentity get_RemoteIdentity() {
        throw new Exception("STUB");
    }

}
