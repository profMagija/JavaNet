package System.IO;
public class File {
    public static final System.IO.StreamReader OpenText(String path) {
        throw new Exception("STUB");
    }

    public static final System.IO.StreamWriter CreateText(String path) {
        throw new Exception("STUB");
    }

    public static final System.IO.StreamWriter AppendText(String path) {
        throw new Exception("STUB");
    }

    public static final void Copy(String sourceFileName, String destFileName) {
        throw new Exception("STUB");
    }

    public static final void Copy(String sourceFileName, String destFileName, boolean overwrite) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileStream Create(String path) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileStream Create(String path, int bufferSize) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileStream Create(String path, int bufferSize, System.IO.FileOptions options) {
        throw new Exception("STUB");
    }

    public static final void Delete(String path) {
        throw new Exception("STUB");
    }

    public static final boolean Exists(String path) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileStream Open(String path, System.IO.FileMode mode) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileStream Open(String path, System.IO.FileMode mode, System.IO.FileAccess access) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileStream Open(String path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share) {
        throw new Exception("STUB");
    }

    public static final void SetCreationTime(String path, System.DateTime creationTime) {
        throw new Exception("STUB");
    }

    public static final void SetCreationTimeUtc(String path, System.DateTime creationTimeUtc) {
        throw new Exception("STUB");
    }

    public static final System.DateTime GetCreationTime(String path) {
        throw new Exception("STUB");
    }

    public static final System.DateTime GetCreationTimeUtc(String path) {
        throw new Exception("STUB");
    }

    public static final void SetLastAccessTime(String path, System.DateTime lastAccessTime) {
        throw new Exception("STUB");
    }

    public static final void SetLastAccessTimeUtc(String path, System.DateTime lastAccessTimeUtc) {
        throw new Exception("STUB");
    }

    public static final System.DateTime GetLastAccessTime(String path) {
        throw new Exception("STUB");
    }

    public static final System.DateTime GetLastAccessTimeUtc(String path) {
        throw new Exception("STUB");
    }

    public static final void SetLastWriteTime(String path, System.DateTime lastWriteTime) {
        throw new Exception("STUB");
    }

    public static final void SetLastWriteTimeUtc(String path, System.DateTime lastWriteTimeUtc) {
        throw new Exception("STUB");
    }

    public static final System.DateTime GetLastWriteTime(String path) {
        throw new Exception("STUB");
    }

    public static final System.DateTime GetLastWriteTimeUtc(String path) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileAttributes GetAttributes(String path) {
        throw new Exception("STUB");
    }

    public static final void SetAttributes(String path, System.IO.FileAttributes fileAttributes) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileStream OpenRead(String path) {
        throw new Exception("STUB");
    }

    public static final System.IO.FileStream OpenWrite(String path) {
        throw new Exception("STUB");
    }

    public static final String ReadAllText(String path) {
        throw new Exception("STUB");
    }

    public static final String ReadAllText(String path, System.Text.Encoding encoding) {
        throw new Exception("STUB");
    }

    public static final void WriteAllText(String path, String contents) {
        throw new Exception("STUB");
    }

    public static final void WriteAllText(String path, String contents, System.Text.Encoding encoding) {
        throw new Exception("STUB");
    }

    public static final System.Byte[] ReadAllBytes(String path) {
        throw new Exception("STUB");
    }

    public static final void WriteAllBytes(String path, System.Byte[] bytes) {
        throw new Exception("STUB");
    }

    public static final System.String[] ReadAllLines(String path) {
        throw new Exception("STUB");
    }

    public static final System.String[] ReadAllLines(String path, System.Text.Encoding encoding) {
        throw new Exception("STUB");
    }

    public static final void WriteAllLines(String path, System.String[] contents) {
        throw new Exception("STUB");
    }

    public static final void WriteAllLines(String path, System.String[] contents, System.Text.Encoding encoding) {
        throw new Exception("STUB");
    }

    public static final void AppendAllText(String path, String contents) {
        throw new Exception("STUB");
    }

    public static final void AppendAllText(String path, String contents, System.Text.Encoding encoding) {
        throw new Exception("STUB");
    }

    public static final void Replace(String sourceFileName, String destinationFileName, String destinationBackupFileName) {
        throw new Exception("STUB");
    }

    public static final void Replace(String sourceFileName, String destinationFileName, String destinationBackupFileName, boolean ignoreMetadataErrors) {
        throw new Exception("STUB");
    }

    public static final void Move(String sourceFileName, String destFileName) {
        throw new Exception("STUB");
    }

    public static final void Encrypt(String path) {
        throw new Exception("STUB");
    }

    public static final void Decrypt(String path) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task WriteAllTextAsync(String path, String contents, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task WriteAllTextAsync(String path, String contents, System.Text.Encoding encoding, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task WriteAllBytesAsync(String path, System.Byte[] bytes, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task AppendAllTextAsync(String path, String contents, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public static final System.Threading.Tasks.Task AppendAllTextAsync(String path, String contents, System.Text.Encoding encoding, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

}
