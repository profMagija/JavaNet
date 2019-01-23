package System.Resources;
public class ResourceManager {
    public static final int MagicNumber;

    public static final int HeaderVersionNumber;

    public String get_BaseName() {
        throw new Exception("STUB");
    }

    public boolean get_IgnoreCase() {
        throw new Exception("STUB");
    }

    public void set_IgnoreCase(boolean value) {
        throw new Exception("STUB");
    }

    public System.Type get_ResourceSetType() {
        throw new Exception("STUB");
    }

    public void ReleaseAllResources() {
        throw new Exception("STUB");
    }

    public static final System.Resources.ResourceManager CreateFileBasedResourceManager(String baseName, String resourceDir, System.Type usingResourceSet) {
        throw new Exception("STUB");
    }

    public System.Resources.ResourceSet GetResourceSet(System.Globalization.CultureInfo culture, boolean createIfNotExists, boolean tryParents) {
        throw new Exception("STUB");
    }

    public String GetString(String name) {
        throw new Exception("STUB");
    }

    public String GetString(String name, System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

    public Object GetObject(String name) {
        throw new Exception("STUB");
    }

    public Object GetObject(String name, System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

    public final System.IO.UnmanagedMemoryStream GetStream(String name) {
        throw new Exception("STUB");
    }

    public final System.IO.UnmanagedMemoryStream GetStream(String name, System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

}
