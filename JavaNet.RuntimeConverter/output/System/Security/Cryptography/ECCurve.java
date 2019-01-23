package System.Security.Cryptography;
public class ECCurve {
    public System.Byte[] A;

    public System.Byte[] B;

    public System.Security.Cryptography.ECPoint G;

    public System.Byte[] Order;

    public System.Byte[] Cofactor;

    public System.Byte[] Seed;

    public System.Security.Cryptography.ECCurve+ECCurveType CurveType;

    public System.Byte[] Polynomial;

    public System.Byte[] Prime;

    public final System.Security.Cryptography.Oid get_Oid() {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECCurve CreateFromOid(System.Security.Cryptography.Oid curveOid) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECCurve CreateFromFriendlyName(String oidFriendlyName) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.ECCurve CreateFromValue(String oidValue) {
        throw new Exception("STUB");
    }

    public final boolean get_IsPrime() {
        throw new Exception("STUB");
    }

    public final boolean get_IsCharacteristic2() {
        throw new Exception("STUB");
    }

    public final boolean get_IsExplicit() {
        throw new Exception("STUB");
    }

    public final boolean get_IsNamed() {
        throw new Exception("STUB");
    }

    public final void Validate() {
        throw new Exception("STUB");
    }

}
