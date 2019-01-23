package System.Buffers;
public interface IPinnable {
    public System.Buffers.MemoryHandle Pin(int elementIndex) {
        throw new Exception("STUB");
    }

    public void Unpin() {
        throw new Exception("STUB");
    }

}
