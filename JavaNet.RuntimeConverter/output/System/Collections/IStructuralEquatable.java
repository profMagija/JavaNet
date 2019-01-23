package System.Collections;
public interface IStructuralEquatable {
    public boolean Equals(Object other, System.Collections.IEqualityComparer comparer) {
        throw new Exception("STUB");
    }

    public int GetHashCode(System.Collections.IEqualityComparer comparer) {
        throw new Exception("STUB");
    }

}
