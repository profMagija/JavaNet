package System.Security.Principal;
public interface IIdentity {
    public String get_Name() {
        throw new Exception("STUB");
    }

    public String get_AuthenticationType() {
        throw new Exception("STUB");
    }

    public boolean get_IsAuthenticated() {
        throw new Exception("STUB");
    }

}
