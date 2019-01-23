package System.Security.Cryptography;
public class ECParameters {
    public System.Security.Cryptography.ECPoint Q;

    public System.Byte[] D;

    public System.Security.Cryptography.ECCurve Curve;

    public final void Validate() {
        throw new Exception("STUB");
    }

}
