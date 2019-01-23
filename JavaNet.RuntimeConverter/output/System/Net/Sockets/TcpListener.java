package System.Net.Sockets;
public class TcpListener {
    public final System.Net.Sockets.Socket get_Server() {
        throw new Exception("STUB");
    }

    public final System.Net.EndPoint get_LocalEndpoint() {
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

    public final void Start() {
        throw new Exception("STUB");
    }

    public final void Start(int backlog) {
        throw new Exception("STUB");
    }

    public final void Stop() {
        throw new Exception("STUB");
    }

    public final boolean Pending() {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.Socket AcceptSocket() {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.TcpClient AcceptTcpClient() {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginAcceptSocket(System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.Socket EndAcceptSocket(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginAcceptTcpClient(System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.TcpClient EndAcceptTcpClient(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public static final System.Net.Sockets.TcpListener Create(int port) {
        throw new Exception("STUB");
    }

}
