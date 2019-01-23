package System.Net;
public class Dns {
    public static final System.Net.IPHostEntry GetHostByName(String hostName) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPHostEntry GetHostByAddress(String address) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPHostEntry GetHostByAddress(System.Net.IPAddress address) {
        throw new Exception("STUB");
    }

    public static final String GetHostName() {
        throw new Exception("STUB");
    }

    public static final System.Net.IPHostEntry Resolve(String hostName) {
        throw new Exception("STUB");
    }

    public static final System.IAsyncResult BeginGetHostByName(String hostName, System.AsyncCallback requestCallback, Object stateObject) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPHostEntry EndGetHostByName(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPHostEntry GetHostEntry(String hostNameOrAddress) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPHostEntry GetHostEntry(System.Net.IPAddress address) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPAddress[] GetHostAddresses(String hostNameOrAddress) {
        throw new Exception("STUB");
    }

    public static final System.IAsyncResult BeginGetHostEntry(String hostNameOrAddress, System.AsyncCallback requestCallback, Object stateObject) {
        throw new Exception("STUB");
    }

    public static final System.IAsyncResult BeginGetHostEntry(System.Net.IPAddress address, System.AsyncCallback requestCallback, Object stateObject) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPHostEntry EndGetHostEntry(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public static final System.IAsyncResult BeginGetHostAddresses(String hostNameOrAddress, System.AsyncCallback requestCallback, Object state) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPAddress[] EndGetHostAddresses(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public static final System.IAsyncResult BeginResolve(String hostName, System.AsyncCallback requestCallback, Object stateObject) {
        throw new Exception("STUB");
    }

    public static final System.Net.IPHostEntry EndResolve(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

}
