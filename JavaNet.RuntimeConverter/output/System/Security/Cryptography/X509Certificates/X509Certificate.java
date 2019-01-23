package System.Security.Cryptography.X509Certificates;
public class X509Certificate {
    public void Reset() {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.X509Certificates.X509Certificate CreateFromCertFile(String filename) {
        throw new Exception("STUB");
    }

    public static final System.Security.Cryptography.X509Certificates.X509Certificate CreateFromSignedFile(String filename) {
        throw new Exception("STUB");
    }

    public final System.IntPtr get_Handle() {
        throw new Exception("STUB");
    }

    public final String get_Issuer() {
        throw new Exception("STUB");
    }

    public final String get_Subject() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public boolean Equals(System.Security.Cryptography.X509Certificates.X509Certificate other) {
        throw new Exception("STUB");
    }

    public System.Byte[] Export(System.Security.Cryptography.X509Certificates.X509ContentType contentType) {
        throw new Exception("STUB");
    }

    public System.Byte[] Export(System.Security.Cryptography.X509Certificates.X509ContentType contentType, String password) {
        throw new Exception("STUB");
    }

    public System.Byte[] Export(System.Security.Cryptography.X509Certificates.X509ContentType contentType, System.Security.SecureString password) {
        throw new Exception("STUB");
    }

    public String GetRawCertDataString() {
        throw new Exception("STUB");
    }

    public System.Byte[] GetCertHash() {
        throw new Exception("STUB");
    }

    public System.Byte[] GetCertHash(System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public String GetCertHashString() {
        throw new Exception("STUB");
    }

    public String GetCertHashString(System.Security.Cryptography.HashAlgorithmName hashAlgorithm) {
        throw new Exception("STUB");
    }

    public String GetEffectiveDateString() {
        throw new Exception("STUB");
    }

    public String GetExpirationDateString() {
        throw new Exception("STUB");
    }

    public String GetFormat() {
        throw new Exception("STUB");
    }

    public String GetPublicKeyString() {
        throw new Exception("STUB");
    }

    public System.Byte[] GetRawCertData() {
        throw new Exception("STUB");
    }

    public String GetKeyAlgorithm() {
        throw new Exception("STUB");
    }

    public System.Byte[] GetKeyAlgorithmParameters() {
        throw new Exception("STUB");
    }

    public String GetKeyAlgorithmParametersString() {
        throw new Exception("STUB");
    }

    public System.Byte[] GetPublicKey() {
        throw new Exception("STUB");
    }

    public System.Byte[] GetSerialNumber() {
        throw new Exception("STUB");
    }

    public String GetSerialNumberString() {
        throw new Exception("STUB");
    }

    public String GetName() {
        throw new Exception("STUB");
    }

    public String GetIssuerName() {
        throw new Exception("STUB");
    }

    public String ToString(boolean fVerbose) {
        throw new Exception("STUB");
    }

    public void Import(System.Byte[] rawData) {
        throw new Exception("STUB");
    }

    public void Import(System.Byte[] rawData, String password, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags keyStorageFlags) {
        throw new Exception("STUB");
    }

    public void Import(System.Byte[] rawData, System.Security.SecureString password, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags keyStorageFlags) {
        throw new Exception("STUB");
    }

    public void Import(String fileName) {
        throw new Exception("STUB");
    }

    public void Import(String fileName, String password, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags keyStorageFlags) {
        throw new Exception("STUB");
    }

    public void Import(String fileName, System.Security.SecureString password, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags keyStorageFlags) {
        throw new Exception("STUB");
    }

}
