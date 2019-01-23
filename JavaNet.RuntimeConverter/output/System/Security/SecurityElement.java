package System.Security;
public class SecurityElement {
    public final String get_Tag() {
        throw new Exception("STUB");
    }

    public final void set_Tag(String value) {
        throw new Exception("STUB");
    }

    public final System.Collections.Hashtable get_Attributes() {
        throw new Exception("STUB");
    }

    public final void set_Attributes(System.Collections.Hashtable value) {
        throw new Exception("STUB");
    }

    public final String get_Text() {
        throw new Exception("STUB");
    }

    public final void set_Text(String value) {
        throw new Exception("STUB");
    }

    public final System.Collections.ArrayList get_Children() {
        throw new Exception("STUB");
    }

    public final void set_Children(System.Collections.ArrayList value) {
        throw new Exception("STUB");
    }

    public final void AddAttribute(String name, String value) {
        throw new Exception("STUB");
    }

    public final void AddChild(System.Security.SecurityElement child) {
        throw new Exception("STUB");
    }

    public final boolean Equal(System.Security.SecurityElement other) {
        throw new Exception("STUB");
    }

    public final System.Security.SecurityElement Copy() {
        throw new Exception("STUB");
    }

    public static final boolean IsValidTag(String tag) {
        throw new Exception("STUB");
    }

    public static final boolean IsValidText(String text) {
        throw new Exception("STUB");
    }

    public static final boolean IsValidAttributeName(String name) {
        throw new Exception("STUB");
    }

    public static final boolean IsValidAttributeValue(String value) {
        throw new Exception("STUB");
    }

    public static final String Escape(String str) {
        throw new Exception("STUB");
    }

    public final String Attribute(String name) {
        throw new Exception("STUB");
    }

    public final System.Security.SecurityElement SearchForChildByTag(String tag) {
        throw new Exception("STUB");
    }

    public final String SearchForTextOfTag(String tag) {
        throw new Exception("STUB");
    }

    public static final System.Security.SecurityElement FromString(String xml) {
        throw new Exception("STUB");
    }

}
