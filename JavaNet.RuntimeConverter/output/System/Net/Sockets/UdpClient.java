package System.Net.Sockets;
public class UdpClient {
    public final int get_Available() {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.Socket get_Client() {
        throw new Exception("STUB");
    }

    public final void set_Client(System.Net.Sockets.Socket value) {
        throw new Exception("STUB");
    }

    public final short get_Ttl() {
        throw new Exception("STUB");
    }

    public final void set_Ttl(short value) {
        throw new Exception("STUB");
    }

    public final boolean get_DontFragment() {
        throw new Exception("STUB");
    }

    public final void set_DontFragment(boolean value) {
        throw new Exception("STUB");
    }

    public final boolean get_MulticastLoopback() {
        throw new Exception("STUB");
    }

    public final void set_MulticastLoopback(boolean value) {
        throw new Exception("STUB");
    }

    public final boolean get_EnableBroadcast() {
        throw new Exception("STUB");
    }

    public final void set_EnableBroadcast(boolean value) {
        throw new Exception("STUB");
    }

    public final boolean get_ExclusiveAddressUse() {
        throw new Exception("STUB");
    }

    public final void set_ExclusiveAddressUse(boolean value) {
        throw new Exception("STUB");
    }

    public final void AllowNatTraversal(boolean allowed) {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginSend(System.Byte[] datagram, int bytes, System.Net.IPEndPoint endPoint, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginSend(System.Byte[] datagram, int bytes, String hostname, int port, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginSend(System.Byte[] datagram, int bytes, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final int EndSend(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginReceive(System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final System.Byte[] EndReceive(System.IAsyncResult asyncResult, System.Net.IPEndPoint& remoteEP) {
        throw new Exception("STUB");
    }

    public final void JoinMulticastGroup(System.Net.IPAddress multicastAddr) {
        throw new Exception("STUB");
    }

    public final void JoinMulticastGroup(System.Net.IPAddress multicastAddr, System.Net.IPAddress localAddress) {
        throw new Exception("STUB");
    }

    public final void JoinMulticastGroup(int ifindex, System.Net.IPAddress multicastAddr) {
        throw new Exception("STUB");
    }

    public final void JoinMulticastGroup(System.Net.IPAddress multicastAddr, int timeToLive) {
        throw new Exception("STUB");
    }

    public final void DropMulticastGroup(System.Net.IPAddress multicastAddr) {
        throw new Exception("STUB");
    }

    public final void DropMulticastGroup(System.Net.IPAddress multicastAddr, int ifindex) {
        throw new Exception("STUB");
    }

    public final void Close() {
        throw new Exception("STUB");
    }

    public final void Connect(String hostname, int port) {
        throw new Exception("STUB");
    }

    public final void Connect(System.Net.IPAddress addr, int port) {
        throw new Exception("STUB");
    }

    public final void Connect(System.Net.IPEndPoint endPoint) {
        throw new Exception("STUB");
    }

    public final System.Byte[] Receive(System.Net.IPEndPoint& remoteEP) {
        throw new Exception("STUB");
    }

    public final int Send(System.Byte[] dgram, int bytes, System.Net.IPEndPoint endPoint) {
        throw new Exception("STUB");
    }

    public final int Send(System.Byte[] dgram, int bytes, String hostname, int port) {
        throw new Exception("STUB");
    }

    public final int Send(System.Byte[] dgram, int bytes) {
        throw new Exception("STUB");
    }

}
