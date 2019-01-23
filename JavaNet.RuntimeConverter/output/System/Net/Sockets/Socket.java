package System.Net.Sockets;
public class Socket {
    public final int EndSendTo(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginReceive(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginReceive(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.Sockets.SocketError& errorCode, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final int EndReceive(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final int EndReceive(System.IAsyncResult asyncResult, System.Net.Sockets.SocketError& errorCode) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginReceiveMessageFrom(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint& remoteEP, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final int EndReceiveMessageFrom(System.IAsyncResult asyncResult, System.Net.Sockets.SocketFlags& socketFlags, System.Net.EndPoint& endPoint, System.Net.Sockets.IPPacketInformation& ipPacketInformation) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginReceiveFrom(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint& remoteEP, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final int EndReceiveFrom(System.IAsyncResult asyncResult, System.Net.EndPoint& endPoint) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginAccept(System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginAccept(int receiveSize, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginAccept(System.Net.Sockets.Socket acceptSocket, int receiveSize, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.Socket EndAccept(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.Socket EndAccept(System.Byte[]& buffer, System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.Socket EndAccept(System.Byte[]& buffer, System.Int32& bytesTransferred, System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final void Shutdown(System.Net.Sockets.SocketShutdown how) {
        throw new Exception("STUB");
    }

    public final boolean AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public final boolean ConnectAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public static final boolean ConnectAsync(System.Net.Sockets.SocketType socketType, System.Net.Sockets.ProtocolType protocolType, System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public static final void CancelConnectAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public final boolean DisconnectAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public final boolean ReceiveAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public final boolean ReceiveFromAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public final boolean ReceiveMessageFromAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public final boolean SendAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public final boolean SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public final boolean SendToAsync(System.Net.Sockets.SocketAsyncEventArgs e) {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public static final boolean get_SupportsIPv4() {
        throw new Exception("STUB");
    }

    public static final boolean get_SupportsIPv6() {
        throw new Exception("STUB");
    }

    public static final boolean get_OSSupportsIPv4() {
        throw new Exception("STUB");
    }

    public static final boolean get_OSSupportsIPv6() {
        throw new Exception("STUB");
    }

    public final int get_Available() {
        throw new Exception("STUB");
    }

    public final System.Net.EndPoint get_LocalEndPoint() {
        throw new Exception("STUB");
    }

    public final System.Net.EndPoint get_RemoteEndPoint() {
        throw new Exception("STUB");
    }

    public final System.IntPtr get_Handle() {
        throw new Exception("STUB");
    }

    public final boolean get_Blocking() {
        throw new Exception("STUB");
    }

    public final void set_Blocking(boolean value) {
        throw new Exception("STUB");
    }

    public final boolean get_UseOnlyOverlappedIO() {
        throw new Exception("STUB");
    }

    public final void set_UseOnlyOverlappedIO(boolean value) {
        throw new Exception("STUB");
    }

    public final boolean get_Connected() {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.AddressFamily get_AddressFamily() {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.SocketType get_SocketType() {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.ProtocolType get_ProtocolType() {
        throw new Exception("STUB");
    }

    public final boolean get_IsBound() {
        throw new Exception("STUB");
    }

    public final boolean get_ExclusiveAddressUse() {
        throw new Exception("STUB");
    }

    public final void set_ExclusiveAddressUse(boolean value) {
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

    public final boolean get_DualMode() {
        throw new Exception("STUB");
    }

    public final void set_DualMode(boolean value) {
        throw new Exception("STUB");
    }

    public final void Bind(System.Net.EndPoint localEP) {
        throw new Exception("STUB");
    }

    public final void Connect(System.Net.EndPoint remoteEP) {
        throw new Exception("STUB");
    }

    public final void Connect(System.Net.IPAddress address, int port) {
        throw new Exception("STUB");
    }

    public final void Connect(String host, int port) {
        throw new Exception("STUB");
    }

    public final void Connect(System.Net.IPAddress[] addresses, int port) {
        throw new Exception("STUB");
    }

    public final void Close() {
        throw new Exception("STUB");
    }

    public final void Close(int timeout) {
        throw new Exception("STUB");
    }

    public final void Listen(int backlog) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.Socket Accept() {
        throw new Exception("STUB");
    }

    public final int Send(System.Byte[] buffer, int size, System.Net.Sockets.SocketFlags socketFlags) {
        throw new Exception("STUB");
    }

    public final int Send(System.Byte[] buffer, System.Net.Sockets.SocketFlags socketFlags) {
        throw new Exception("STUB");
    }

    public final int Send(System.Byte[] buffer) {
        throw new Exception("STUB");
    }

    public final int Send(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags) {
        throw new Exception("STUB");
    }

    public final int Send(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.Sockets.SocketError& errorCode) {
        throw new Exception("STUB");
    }

    public final void SendFile(String fileName) {
        throw new Exception("STUB");
    }

    public final void SendFile(String fileName, System.Byte[] preBuffer, System.Byte[] postBuffer, System.Net.Sockets.TransmitFileOptions flags) {
        throw new Exception("STUB");
    }

    public final int SendTo(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint remoteEP) {
        throw new Exception("STUB");
    }

    public final int SendTo(System.Byte[] buffer, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint remoteEP) {
        throw new Exception("STUB");
    }

    public final int SendTo(System.Byte[] buffer, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint remoteEP) {
        throw new Exception("STUB");
    }

    public final int SendTo(System.Byte[] buffer, System.Net.EndPoint remoteEP) {
        throw new Exception("STUB");
    }

    public final int Receive(System.Byte[] buffer, int size, System.Net.Sockets.SocketFlags socketFlags) {
        throw new Exception("STUB");
    }

    public final int Receive(System.Byte[] buffer, System.Net.Sockets.SocketFlags socketFlags) {
        throw new Exception("STUB");
    }

    public final int Receive(System.Byte[] buffer) {
        throw new Exception("STUB");
    }

    public final int Receive(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags) {
        throw new Exception("STUB");
    }

    public final int Receive(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.Sockets.SocketError& errorCode) {
        throw new Exception("STUB");
    }

    public final int ReceiveMessageFrom(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags& socketFlags, System.Net.EndPoint& remoteEP, System.Net.Sockets.IPPacketInformation& ipPacketInformation) {
        throw new Exception("STUB");
    }

    public final int ReceiveFrom(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint& remoteEP) {
        throw new Exception("STUB");
    }

    public final int ReceiveFrom(System.Byte[] buffer, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint& remoteEP) {
        throw new Exception("STUB");
    }

    public final int ReceiveFrom(System.Byte[] buffer, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint& remoteEP) {
        throw new Exception("STUB");
    }

    public final int ReceiveFrom(System.Byte[] buffer, System.Net.EndPoint& remoteEP) {
        throw new Exception("STUB");
    }

    public final int IOControl(int ioControlCode, System.Byte[] optionInValue, System.Byte[] optionOutValue) {
        throw new Exception("STUB");
    }

    public final int IOControl(System.Net.Sockets.IOControlCode ioControlCode, System.Byte[] optionInValue, System.Byte[] optionOutValue) {
        throw new Exception("STUB");
    }

    public final void SetSocketOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName, int optionValue) {
        throw new Exception("STUB");
    }

    public final void SetSocketOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName, System.Byte[] optionValue) {
        throw new Exception("STUB");
    }

    public final void SetSocketOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName, boolean optionValue) {
        throw new Exception("STUB");
    }

    public final void SetSocketOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName, Object optionValue) {
        throw new Exception("STUB");
    }

    public final Object GetSocketOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName) {
        throw new Exception("STUB");
    }

    public final void GetSocketOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName, System.Byte[] optionValue) {
        throw new Exception("STUB");
    }

    public final System.Byte[] GetSocketOption(System.Net.Sockets.SocketOptionLevel optionLevel, System.Net.Sockets.SocketOptionName optionName, int optionLength) {
        throw new Exception("STUB");
    }

    public final void SetIPProtectionLevel(System.Net.Sockets.IPProtectionLevel level) {
        throw new Exception("STUB");
    }

    public final boolean Poll(int microSeconds, System.Net.Sockets.SelectMode mode) {
        throw new Exception("STUB");
    }

    public static final void Select(System.Collections.IList checkRead, System.Collections.IList checkWrite, System.Collections.IList checkError, int microSeconds) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginConnect(System.Net.EndPoint remoteEP, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.Net.Sockets.SocketInformation DuplicateAndClose(int targetProcessId) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginConnect(String host, int port, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginConnect(System.Net.IPAddress address, int port, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginConnect(System.Net.IPAddress[] addresses, int port, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginDisconnect(boolean reuseSocket, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final void Disconnect(boolean reuseSocket) {
        throw new Exception("STUB");
    }

    public final void EndConnect(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final void EndDisconnect(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginSend(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginSend(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.Sockets.SocketError& errorCode, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final int EndSend(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final int EndSend(System.IAsyncResult asyncResult, System.Net.Sockets.SocketError& errorCode) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginSendFile(String fileName, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginSendFile(String fileName, System.Byte[] preBuffer, System.Byte[] postBuffer, System.Net.Sockets.TransmitFileOptions flags, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public final void EndSendFile(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public final System.IAsyncResult BeginSendTo(System.Byte[] buffer, int offset, int size, System.Net.Sockets.SocketFlags socketFlags, System.Net.EndPoint remoteEP, System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

}
