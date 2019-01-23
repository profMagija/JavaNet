package System.Net;
public interface IWebProxy {
    public System.Uri GetProxy(System.Uri destination) {
        throw new Exception("STUB");
    }

    public boolean IsBypassed(System.Uri host) {
        throw new Exception("STUB");
    }

    public System.Net.ICredentials get_Credentials() {
        throw new Exception("STUB");
    }

    public void set_Credentials(System.Net.ICredentials value) {
        throw new Exception("STUB");
    }

}
