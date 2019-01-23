package System.Runtime.InteropServices;
public class DllImportAttribute {
    public String EntryPoint;

    public System.Runtime.InteropServices.CharSet CharSet;

    public boolean SetLastError;

    public boolean ExactSpelling;

    public System.Runtime.InteropServices.CallingConvention CallingConvention;

    public boolean BestFitMapping;

    public boolean PreserveSig;

    public boolean ThrowOnUnmappableChar;

    public final String get_Value() {
        throw new Exception("STUB");
    }

}
