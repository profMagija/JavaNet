package System.Security;
public interface IPermission {
    public System.Security.IPermission Copy() {
        throw new Exception("STUB");
    }

    public void Demand() {
        throw new Exception("STUB");
    }

    public System.Security.IPermission Intersect(System.Security.IPermission target) {
        throw new Exception("STUB");
    }

    public boolean IsSubsetOf(System.Security.IPermission target) {
        throw new Exception("STUB");
    }

    public System.Security.IPermission Union(System.Security.IPermission target) {
        throw new Exception("STUB");
    }

}
