package System.Security.Cryptography.X509Certificates;
public class X509ChainStatusFlags {
    public int value__;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags NoError = 0;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags NotTimeValid = 1;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags NotTimeNested = 2;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags Revoked = 4;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags NotSignatureValid = 8;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags NotValidForUsage = 16;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags UntrustedRoot = 32;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags RevocationStatusUnknown = 64;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags Cyclic = 128;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags InvalidExtension = 256;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags InvalidPolicyConstraints = 512;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags InvalidBasicConstraints = 1024;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags InvalidNameConstraints = 2048;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags HasNotSupportedNameConstraint = 4096;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags HasNotDefinedNameConstraint = 8192;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags HasNotPermittedNameConstraint = 16384;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags HasExcludedNameConstraint = 32768;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags PartialChain = 65536;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags CtlNotTimeValid = 131072;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags CtlNotSignatureValid = 262144;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags CtlNotValidForUsage = 524288;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags OfflineRevocation = 16777216;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags NoIssuanceChainPolicy = 33554432;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags ExplicitDistrust = 67108864;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags HasNotSupportedCriticalExtension = 134217728;

    public static final System.Security.Cryptography.X509Certificates.X509ChainStatusFlags HasWeakSignature = 1048576;

}
