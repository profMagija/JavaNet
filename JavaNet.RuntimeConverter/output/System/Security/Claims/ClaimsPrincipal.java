package System.Security.Claims;
public class ClaimsPrincipal {
    public System.Security.Principal.IIdentity get_Identity() {
        throw new Exception("STUB");
    }

    public boolean IsInRole(String role) {
        throw new Exception("STUB");
    }

    public void AddIdentity(System.Security.Claims.ClaimsIdentity identity) {
        throw new Exception("STUB");
    }

    public System.Security.Claims.ClaimsPrincipal Clone() {
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

    public static final System.Security.Claims.ClaimsPrincipal get_Current() {
        throw new Exception("STUB");
    }

}
