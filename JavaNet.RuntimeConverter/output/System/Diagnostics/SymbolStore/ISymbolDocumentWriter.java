package System.Diagnostics.SymbolStore;
public interface ISymbolDocumentWriter {
    public void SetSource(System.Byte[] source) {
        throw new Exception("STUB");
    }

    public void SetCheckSum(System.Guid algorithmId, System.Byte[] checkSum) {
        throw new Exception("STUB");
    }

}
