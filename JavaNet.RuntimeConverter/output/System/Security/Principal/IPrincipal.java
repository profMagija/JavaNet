package System.Security.Principal;
public interface IPrincipal {
    public System.Security.Principal.IIdentity get_Identity() {
        throw new Exception("STUB");
    }

    public boolean IsInRole(String role) {
        throw new Exception("STUB");
    }

}
