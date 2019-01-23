package System.Runtime.InteropServices;
public class SafeBuffer {
    public final void Initialize(System.UInt64 numBytes) {
        throw new Exception("STUB");
    }

    public final void Initialize(System.UInt32 numElements, System.UInt32 sizeOfEachElement) {
        throw new Exception("STUB");
    }

    public final void AcquirePointer(System.Byte*& pointer) {
        throw new Exception("STUB");
    }

    public final void ReleasePointer() {
        throw new Exception("STUB");
    }

    public final System.UInt64 get_ByteLength() {
        throw new Exception("STUB");
    }

}
