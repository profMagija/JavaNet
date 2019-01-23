package System.Runtime.CompilerServices;
public interface ICastable {
    public boolean IsInstanceOfInterface(System.RuntimeTypeHandle interfaceType, System.Exception& castError) {
        throw new Exception("STUB");
    }

    public System.RuntimeTypeHandle GetImplType(System.RuntimeTypeHandle interfaceType) {
        throw new Exception("STUB");
    }

}
