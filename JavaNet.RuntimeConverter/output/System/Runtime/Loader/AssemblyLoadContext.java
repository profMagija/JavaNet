package System.Runtime.Loader;
public class AssemblyLoadContext {
    public static final System.Reflection.Assembly[] GetLoadedAssemblies() {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly LoadFromAssemblyPath(String assemblyPath) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly LoadFromNativeImagePath(String nativeImagePath, String assemblyPath) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly LoadFromStream(System.IO.Stream assembly) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly LoadFromStream(System.IO.Stream assembly, System.IO.Stream assemblySymbols) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly LoadFromAssemblyName(System.Reflection.AssemblyName assemblyName) {
        throw new Exception("STUB");
    }

    public static final System.Runtime.Loader.AssemblyLoadContext get_Default() {
        throw new Exception("STUB");
    }

    public static final System.Reflection.AssemblyName GetAssemblyName(String assemblyPath) {
        throw new Exception("STUB");
    }

    public static final System.Runtime.Loader.AssemblyLoadContext GetLoadContext(System.Reflection.Assembly assembly) {
        throw new Exception("STUB");
    }

    public final void SetProfileOptimizationRoot(String directoryPath) {
        throw new Exception("STUB");
    }

    public final void StartProfileOptimization(String profile) {
        throw new Exception("STUB");
    }

    public static final void add_AssemblyLoad(System.AssemblyLoadEventHandler value) {
        throw new Exception("STUB");
    }

    public static final void remove_AssemblyLoad(System.AssemblyLoadEventHandler value) {
        throw new Exception("STUB");
    }

    public static final void add_TypeResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public static final void remove_TypeResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public static final void add_ResourceResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public static final void remove_ResourceResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public static final void add_AssemblyResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public static final void remove_AssemblyResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

}
