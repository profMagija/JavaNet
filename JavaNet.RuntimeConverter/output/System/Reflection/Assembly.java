package System.Reflection;
public class Assembly {
    public static final System.Reflection.Assembly LoadFrom(String assemblyFile) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly LoadFrom(String assemblyFile, System.Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly Load(String assemblyString) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyRef) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly Load(System.Byte[] rawAssembly, System.Byte[] rawSymbolStore) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly LoadFile(String path) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly GetExecutingAssembly() {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly GetCallingAssembly() {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly GetEntryAssembly() {
        throw new Exception("STUB");
    }

    public System.Type[] GetTypes() {
        throw new Exception("STUB");
    }

    public System.Type[] GetExportedTypes() {
        throw new Exception("STUB");
    }

    public System.Type[] GetForwardedTypes() {
        throw new Exception("STUB");
    }

    public String get_CodeBase() {
        throw new Exception("STUB");
    }

    public System.Reflection.MethodInfo get_EntryPoint() {
        throw new Exception("STUB");
    }

    public String get_FullName() {
        throw new Exception("STUB");
    }

    public String get_ImageRuntimeVersion() {
        throw new Exception("STUB");
    }

    public boolean get_IsDynamic() {
        throw new Exception("STUB");
    }

    public String get_Location() {
        throw new Exception("STUB");
    }

    public boolean get_ReflectionOnly() {
        throw new Exception("STUB");
    }

    public System.Reflection.ManifestResourceInfo GetManifestResourceInfo(String resourceName) {
        throw new Exception("STUB");
    }

    public System.String[] GetManifestResourceNames() {
        throw new Exception("STUB");
    }

    public System.IO.Stream GetManifestResourceStream(String name) {
        throw new Exception("STUB");
    }

    public System.IO.Stream GetManifestResourceStream(System.Type type, String name) {
        throw new Exception("STUB");
    }

    public final boolean get_IsFullyTrusted() {
        throw new Exception("STUB");
    }

    public System.Reflection.AssemblyName GetName() {
        throw new Exception("STUB");
    }

    public System.Reflection.AssemblyName GetName(boolean copiedName) {
        throw new Exception("STUB");
    }

    public System.Type GetType(String name) {
        throw new Exception("STUB");
    }

    public System.Type GetType(String name, boolean throwOnError) {
        throw new Exception("STUB");
    }

    public System.Type GetType(String name, boolean throwOnError, boolean ignoreCase) {
        throw new Exception("STUB");
    }

    public boolean IsDefined(System.Type attributeType, boolean inherit) {
        throw new Exception("STUB");
    }

    public System.Object[] GetCustomAttributes(boolean inherit) {
        throw new Exception("STUB");
    }

    public System.Object[] GetCustomAttributes(System.Type attributeType, boolean inherit) {
        throw new Exception("STUB");
    }

    public String get_EscapedCodeBase() {
        throw new Exception("STUB");
    }

    public final Object CreateInstance(String typeName) {
        throw new Exception("STUB");
    }

    public final Object CreateInstance(String typeName, boolean ignoreCase) {
        throw new Exception("STUB");
    }

    public Object CreateInstance(String typeName, boolean ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Object[] args, System.Globalization.CultureInfo culture, System.Object[] activationAttributes) {
        throw new Exception("STUB");
    }

    public void add_ModuleResolve(System.Reflection.ModuleResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public void remove_ModuleResolve(System.Reflection.ModuleResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public System.Reflection.Module get_ManifestModule() {
        throw new Exception("STUB");
    }

    public System.Reflection.Module GetModule(String name) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Module[] GetModules() {
        throw new Exception("STUB");
    }

    public System.Reflection.Module[] GetModules(boolean getResourceModules) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Module[] GetLoadedModules() {
        throw new Exception("STUB");
    }

    public System.Reflection.Module[] GetLoadedModules(boolean getResourceModules) {
        throw new Exception("STUB");
    }

    public System.Reflection.AssemblyName[] GetReferencedAssemblies() {
        throw new Exception("STUB");
    }

    public System.Reflection.Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture) {
        throw new Exception("STUB");
    }

    public System.Reflection.Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture, System.Version version) {
        throw new Exception("STUB");
    }

    public System.IO.FileStream GetFile(String name) {
        throw new Exception("STUB");
    }

    public System.IO.FileStream[] GetFiles() {
        throw new Exception("STUB");
    }

    public System.IO.FileStream[] GetFiles(boolean getResourceModules) {
        throw new Exception("STUB");
    }

    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {
        throw new Exception("STUB");
    }

    public boolean get_GlobalAssemblyCache() {
        throw new Exception("STUB");
    }

    public long get_HostContext() {
        throw new Exception("STUB");
    }

    public static final boolean op_Equality(System.Reflection.Assembly left, System.Reflection.Assembly right) {
        throw new Exception("STUB");
    }

    public static final boolean op_Inequality(System.Reflection.Assembly left, System.Reflection.Assembly right) {
        throw new Exception("STUB");
    }

    public static final String CreateQualifiedName(String assemblyName, String typeName) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly GetAssembly(System.Type type) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly Load(System.Byte[] rawAssembly) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly LoadWithPartialName(String partialName) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly UnsafeLoadFrom(String assemblyFile) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Module LoadModule(String moduleName, System.Byte[] rawModule) {
        throw new Exception("STUB");
    }

    public System.Reflection.Module LoadModule(String moduleName, System.Byte[] rawModule, System.Byte[] rawSymbolStore) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly ReflectionOnlyLoad(System.Byte[] rawAssembly) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly ReflectionOnlyLoad(String assemblyString) {
        throw new Exception("STUB");
    }

    public static final System.Reflection.Assembly ReflectionOnlyLoadFrom(String assemblyFile) {
        throw new Exception("STUB");
    }

    public System.Security.SecurityRuleSet get_SecurityRuleSet() {
        throw new Exception("STUB");
    }

}
