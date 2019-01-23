package System;
public class Uri {
    public static final String UriSchemeFile;

    public static final String UriSchemeFtp;

    public static final String UriSchemeGopher;

    public static final String UriSchemeHttp;

    public static final String UriSchemeHttps;

    public static final String UriSchemeMailto;

    public static final String UriSchemeNews;

    public static final String UriSchemeNntp;

    public static final String UriSchemeNetTcp;

    public static final String UriSchemeNetPipe;

    public static final String SchemeDelimiter;

    public final boolean IsBaseOf(System.Uri uri) {
        throw new Exception("STUB");
    }

    public final String get_AbsolutePath() {
        throw new Exception("STUB");
    }

    public final String get_AbsoluteUri() {
        throw new Exception("STUB");
    }

    public final String get_LocalPath() {
        throw new Exception("STUB");
    }

    public final String get_Authority() {
        throw new Exception("STUB");
    }

    public final System.UriHostNameType get_HostNameType() {
        throw new Exception("STUB");
    }

    public final boolean get_IsDefaultPort() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFile() {
        throw new Exception("STUB");
    }

    public final boolean get_IsLoopback() {
        throw new Exception("STUB");
    }

    public final String get_PathAndQuery() {
        throw new Exception("STUB");
    }

    public final System.String[] get_Segments() {
        throw new Exception("STUB");
    }

    public final boolean get_IsUnc() {
        throw new Exception("STUB");
    }

    public final String get_Host() {
        throw new Exception("STUB");
    }

    public final int get_Port() {
        throw new Exception("STUB");
    }

    public final String get_Query() {
        throw new Exception("STUB");
    }

    public final String get_Fragment() {
        throw new Exception("STUB");
    }

    public final String get_Scheme() {
        throw new Exception("STUB");
    }

    public final String get_OriginalString() {
        throw new Exception("STUB");
    }

    public final String get_DnsSafeHost() {
        throw new Exception("STUB");
    }

    public final String get_IdnHost() {
        throw new Exception("STUB");
    }

    public final boolean get_IsAbsoluteUri() {
        throw new Exception("STUB");
    }

    public final boolean get_UserEscaped() {
        throw new Exception("STUB");
    }

    public final String get_UserInfo() {
        throw new Exception("STUB");
    }

    public static final System.UriHostNameType CheckHostName(String name) {
        throw new Exception("STUB");
    }

    public final String GetLeftPart(System.UriPartial part) {
        throw new Exception("STUB");
    }

    public static final String HexEscape(char character) {
        throw new Exception("STUB");
    }

    public static final char HexUnescape(String pattern, System.Int32& index) {
        throw new Exception("STUB");
    }

    public static final boolean IsHexEncoding(String pattern, int index) {
        throw new Exception("STUB");
    }

    public static final boolean CheckSchemeName(String schemeName) {
        throw new Exception("STUB");
    }

    public static final boolean IsHexDigit(char character) {
        throw new Exception("STUB");
    }

    public static final int FromHex(char digit) {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Uri uri1, System.Uri uri2) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Uri uri1, System.Uri uri2) {
        throw new Exception("STUB");
    }

    public final System.Uri MakeRelativeUri(System.Uri uri) {
        throw new Exception("STUB");
    }

    public final String MakeRelative(System.Uri toUri) {
        throw new Exception("STUB");
    }

    public static final boolean TryCreate(String uriString, System.UriKind uriKind, System.Uri& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryCreate(System.Uri baseUri, String relativeUri, System.Uri& result) {
        throw new Exception("STUB");
    }

    public static final boolean TryCreate(System.Uri baseUri, System.Uri relativeUri, System.Uri& result) {
        throw new Exception("STUB");
    }

    public final String GetComponents(System.UriComponents components, System.UriFormat format) {
        throw new Exception("STUB");
    }

    public static final int Compare(System.Uri uri1, System.Uri uri2, System.UriComponents partsToCompare, System.UriFormat compareFormat, System.StringComparison comparisonType) {
        throw new Exception("STUB");
    }

    public final boolean IsWellFormedOriginalString() {
        throw new Exception("STUB");
    }

    public static final boolean IsWellFormedUriString(String uriString, System.UriKind uriKind) {
        throw new Exception("STUB");
    }

    public static final String UnescapeDataString(String stringToUnescape) {
        throw new Exception("STUB");
    }

    public static final String EscapeUriString(String stringToEscape) {
        throw new Exception("STUB");
    }

    public static final String EscapeDataString(String stringToEscape) {
        throw new Exception("STUB");
    }

}
