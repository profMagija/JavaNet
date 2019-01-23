package System.Security.Cryptography;
public class AsymmetricSignatureDeformatter {
    public void SetKey(System.Security.Cryptography.AsymmetricAlgorithm key) {
        throw new Exception("STUB");
    }

    public void SetHashAlgorithm(String strName) {
        throw new Exception("STUB");
    }

    public boolean VerifySignature(System.Security.Cryptography.HashAlgorithm hash, System.Byte[] rgbSignature) {
        throw new Exception("STUB");
    }

    public boolean VerifySignature(System.Byte[] rgbHash, System.Byte[] rgbSignature) {
        throw new Exception("STUB");
    }

}
