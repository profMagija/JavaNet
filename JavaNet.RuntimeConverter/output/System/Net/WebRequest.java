package System.Net;
public class WebRequest {
    public String get_ConnectionGroupName() {
        throw new Exception("STUB");
    }

    public void set_ConnectionGroupName(String value) {
        throw new Exception("STUB");
    }

    public long get_ContentLength() {
        throw new Exception("STUB");
    }

    public void set_ContentLength(long value) {
        throw new Exception("STUB");
    }

    public String get_ContentType() {
        throw new Exception("STUB");
    }

    public void set_ContentType(String value) {
        throw new Exception("STUB");
    }

    public System.Net.ICredentials get_Credentials() {
        throw new Exception("STUB");
    }

    public void set_Credentials(System.Net.ICredentials value) {
        throw new Exception("STUB");
    }

    public System.Net.WebHeaderCollection get_Headers() {
        throw new Exception("STUB");
    }

    public String get_Method() {
        throw new Exception("STUB");
    }

    public void set_Method(String value) {
        throw new Exception("STUB");
    }

    public boolean get_PreAuthenticate() {
        throw new Exception("STUB");
    }

    public void set_PreAuthenticate(boolean value) {
        throw new Exception("STUB");
    }

    public System.Net.IWebProxy get_Proxy() {
        throw new Exception("STUB");
    }

    public void set_Proxy(System.Net.IWebProxy value) {
        throw new Exception("STUB");
    }

    public int get_Timeout() {
        throw new Exception("STUB");
    }

    public void set_Timeout(int value) {
        throw new Exception("STUB");
    }

    public System.Uri get_RequestUri() {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginGetRequestStream(System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public System.IAsyncResult BeginGetResponse(System.AsyncCallback callback, Object state) {
        throw new Exception("STUB");
    }

    public System.IO.Stream EndGetRequestStream(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public System.Net.WebResponse EndGetResponse(System.IAsyncResult asyncResult) {
        throw new Exception("STUB");
    }

    public System.IO.Stream GetRequestStream() {
        throw new Exception("STUB");
    }

    public System.Net.WebResponse GetResponse() {
        throw new Exception("STUB");
    }

    public boolean get_UseDefaultCredentials() {
        throw new Exception("STUB");
    }

    public void set_UseDefaultCredentials(boolean value) {
        throw new Exception("STUB");
    }

    public void Abort() {
        throw new Exception("STUB");
    }

    public System.Net.Cache.RequestCachePolicy get_CachePolicy() {
        throw new Exception("STUB");
    }

    public void set_CachePolicy(System.Net.Cache.RequestCachePolicy value) {
        throw new Exception("STUB");
    }

    public void set_Headers(System.Net.WebHeaderCollection value) {
        throw new Exception("STUB");
    }

    public static final System.Net.WebRequest Create(String requestUriString) {
        throw new Exception("STUB");
    }

    public static final System.Net.WebRequest Create(System.Uri requestUri) {
        throw new Exception("STUB");
    }

    public static final System.Net.WebRequest CreateDefault(System.Uri requestUri) {
        throw new Exception("STUB");
    }

    public static final System.Net.HttpWebRequest CreateHttp(String requestUriString) {
        throw new Exception("STUB");
    }

    public static final System.Net.HttpWebRequest CreateHttp(System.Uri requestUri) {
        throw new Exception("STUB");
    }

    public static final boolean RegisterPrefix(String prefix, System.Net.IWebRequestCreate creator) {
        throw new Exception("STUB");
    }

    public static final System.Net.Cache.RequestCachePolicy get_DefaultCachePolicy() {
        throw new Exception("STUB");
    }

    public static final void set_DefaultCachePolicy(System.Net.Cache.RequestCachePolicy value) {
        throw new Exception("STUB");
    }

    public final System.Net.Security.AuthenticationLevel get_AuthenticationLevel() {
        throw new Exception("STUB");
    }

    public final void set_AuthenticationLevel(System.Net.Security.AuthenticationLevel value) {
        throw new Exception("STUB");
    }

    public final System.Security.Principal.TokenImpersonationLevel get_ImpersonationLevel() {
        throw new Exception("STUB");
    }

    public final void set_ImpersonationLevel(System.Security.Principal.TokenImpersonationLevel value) {
        throw new Exception("STUB");
    }

    public static final System.Net.IWebProxy GetSystemWebProxy() {
        throw new Exception("STUB");
    }

    public static final System.Net.IWebProxy get_DefaultWebProxy() {
        throw new Exception("STUB");
    }

    public static final void set_DefaultWebProxy(System.Net.IWebProxy value) {
        throw new Exception("STUB");
    }

}
