package System.Net.Sockets;
public class TcpClient {
    public final int get_Available() {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.Socket get_Client() {
        throw new Exception("STUB");
    }

    public final void set_Client(System.Net.Sockets.Socket value) {
        throw new Exception("STUB");
    }

    public final boolean get_Connected() {
        throw new Exception("STUB");
    }

    public final boolean get_ExclusiveAddressUse() {
        throw new Exception("STUB");
    }

    public final void set_ExclusiveAddressUse(boolean value) {
        throw new Exception("STUB");
    }

    public final void Connect(String hostname, int port) {
        throw new Exception("STUB");
    }

    public final void Connect(System.Net.IPAddress address, int port) {
        throw new Exception("STUB");
    }

    public final void Connect(System.Net.IPEndPoint remoteEP) {
        throw new Exception("STUB");
    }

    public final void Connect(System.Net.IPAddress[] ipAddresses, int port) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task ConnectAsync(System.Net.IPAddress address, int port) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task ConnectAsync(String host, int port) {
        throw new Exception("STUB");
    }

    public final System.Threading.Tasks.Task ConnectAsync(System.Net.IPAddress[] addresses, int port) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginConnect(System.Net.IPAddress address, int port, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginConnect(String host, int port, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginConnect(System.Net.IPAddress[] addresses, int port, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final void EndConnect(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.NetworkStream GetStream() {
        throw new Exception("STUB");
    }

    public final void Close() {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final int get_ReceiveBufferSize() {
        throw new Exception("STUB");
    }

    public final void set_ReceiveBufferSize(int value) {
        throw new Exception("STUB");
    }

    public final int get_SendBufferSize() {
        throw new Exception("STUB");
    }

    public final void set_SendBufferSize(int value) {
        throw new Exception("STUB");
    }

    public final int get_ReceiveTimeout() {
        throw new Exception("STUB");
    }

    public final void set_ReceiveTimeout(int value) {
        throw new Exception("STUB");
    }

    public final int get_SendTimeout() {
        throw new Exception("STUB");
    }

    public final void set_SendTimeout(int value) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.LingerOption get_LingerState() {
        throw new Exception("STUB");
    }

    public final void set_LingerState(System.Net.Sockets.LingerOption value) {
        throw new Exception("STUB");
    }

    public final boolean get_NoDelay() {
        throw new Exception("STUB");
    }

    public final void set_NoDelay(boolean value) {
        throw new Exception("STUB");
    }

}
