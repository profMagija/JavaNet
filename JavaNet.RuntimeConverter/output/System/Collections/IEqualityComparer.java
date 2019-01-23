package System.Collections;
public interface IEqualityComparer {
    public boolean Equals(Object x, Object y) {
        throw new Exception("STUB");
    }

    public int GetHashCode(Object obj) {
        throw new Exception("STUB");
    }

}
