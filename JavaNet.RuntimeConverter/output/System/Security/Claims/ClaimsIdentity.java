package System.Security.Claims;
public class ClaimsIdentity {
    public static final String DefaultIssuer = LOCAL AUTHORITY;

    public static final String DefaultNameClaimType = http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name;

    public static final String DefaultRoleClaimType = http://schemas.microsoft.com/ws/2008/06/identity/claims/role;

    public System.Security.Claims.ClaimsIdentity Clone() {
        throw new Exception("STUB");
    }

    public String get_Name() {
        throw new Exception("STUB");
    }

    public String get_AuthenticationType() {
        throw new Exception("STUB");
    }

    public boolean get_IsAuthenticated() {
        throw new Exception("STUB");
    }

    public void AddClaim(System.Security.Claims.Claim claim) {
        throw new Exception("STUB");
    }

    public boolean TryRemoveClaim(System.Security.Claims.Claim claim) {
        throw new Exception("STUB");
    }

    public void RemoveClaim(System.Security.Claims.Claim claim) {
        throw new Exception("STUB");
    }

    public System.Security.Claims.Claim FindFirst(String type) {
        throw new Exception("STUB");
    }

    public boolean HasClaim(String type, String value) {
        throw new Exception("STUB");
    }

    public void WriteTo(System.IO.BinaryWriter writer) {
        throw new Exception("STUB");
    }

    public final System.Security.Claims.ClaimsIdentity get_Actor() {
        throw new Exception("STUB");
    }

    public final void set_Actor(System.Security.Claims.ClaimsIdentity value) {
        throw new Exception("STUB");
    }

    public final Object get_BootstrapContext() {
        throw new Exception("STUB");
    }

    public final void set_BootstrapContext(Object value) {
        throw new Exception("STUB");
    }

    public final String get_Label() {
        throw new Exception("STUB");
    }

    public final void set_Label(String value) {
        throw new Exception("STUB");
    }

    public final String get_NameClaimType() {
        throw new Exception("STUB");
    }

    public final String get_RoleClaimType() {
        throw new Exception("STUB");
    }

}
