package System.Net;
public class CookieContainer {
    public static final int DefaultCookieLimit = 300;

    public static final int DefaultPerDomainCookieLimit = 20;

    public static final int DefaultCookieLengthLimit = 4096;

    public final int get_Capacity() {
        throw new Exception("STUB");
    }

    public final void set_Capacity(int value) {
        throw new Exception("STUB");
    }

    public final int get_Count() {
        throw new Exception("STUB");
    }

    public final int get_MaxCookieSize() {
        throw new Exception("STUB");
    }

    public final void set_MaxCookieSize(int value) {
        throw new Exception("STUB");
    }

    public final int get_PerDomainCapacity() {
        throw new Exception("STUB");
    }

    public final void set_PerDomainCapacity(int value) {
        throw new Exception("STUB");
    }

    public final void Add(System.Net.Cookie cookie) {
        throw new Exception("STUB");
    }

    public final void Add(System.Net.CookieCollection cookies) {
        throw new Exception("STUB");
    }

    public final void Add(System.Uri uri, System.Net.Cookie cookie) {
        throw new Exception("STUB");
    }

    public final void Add(System.Uri uri, System.Net.CookieCollection cookies) {
        throw new Exception("STUB");
    }

    public final System.Net.CookieCollection GetCookies(System.Uri uri) {
        throw new Exception("STUB");
    }

    public final String GetCookieHeader(System.Uri uri) {
        throw new Exception("STUB");
    }

    public final void SetCookies(System.Uri uri, String cookieHeader) {
        throw new Exception("STUB");
    }

}
