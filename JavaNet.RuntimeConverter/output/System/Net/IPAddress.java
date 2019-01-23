package System.Net;
public class IPAddress {
    public static final System.Net.IPAddress Any;

    public static final System.Net.IPAddress Loopback;

    public static final System.Net.IPAddress Broadcast;

    public static final System.Net.IPAddress None;

    public static final System.Net.IPAddress IPv6Any;

    public static final System.Net.IPAddress IPv6Loopback;

    public static final System.Net.IPAddress IPv6None;

    public static final boolean TryParse(String ipString, System.Net.IPAddress& address) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPAddress Parse(String ipString) {
        throw new Exception("STUB");
    }

    public final System.Byte[] GetAddressBytes() {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.AddressFamily get_AddressFamily() {
        throw new Exception("STUB");
    }

    public final long get_ScopeId() {
        throw new Exception("STUB");
    }

    public final void set_ScopeId(long value) {
        throw new Exception("STUB");
    }

    public static final long HostToNetworkOrder(long host) {
        throw new Exception("STUB");
    }

    public static final int HostToNetworkOrder(int host) {
        throw new Exception("STUB");
    }

    public static final short HostToNetworkOrder(short host) {
        throw new Exception("STUB");
    }

    public static final long NetworkToHostOrder(long network) {
        throw new Exception("STUB");
    }

    public static final int NetworkToHostOrder(int network) {
        throw new Exception("STUB");
    }

    public static final short NetworkToHostOrder(short network) {
        throw new Exception("STUB");
    }

    public static final boolean IsLoopback(System.Net.IPAddress address) {
        throw new Exception("STUB");
    }

    public final boolean get_IsIPv6Multicast() {
        throw new Exception("STUB");
    }

    public final boolean get_IsIPv6LinkLocal() {
        throw new Exception("STUB");
    }

    public final boolean get_IsIPv6SiteLocal() {
        throw new Exception("STUB");
    }

    public final boolean get_IsIPv6Teredo() {
        throw new Exception("STUB");
    }

    public final boolean get_IsIPv4MappedToIPv6() {
        throw new Exception("STUB");
    }

    public final long get_Address() {
        throw new Exception("STUB");
    }

    public final void set_Address(long value) {
        throw new Exception("STUB");
    }

    public final System.Net.IPAddress MapToIPv6() {
        throw new Exception("STUB");
    }

    public final System.Net.IPAddress MapToIPv4() {
        throw new Exception("STUB");
    }

}
