package System;
public class AppDomain {
    public static final System.AppDomain get_CurrentDomain() {
        throw new Exception("STUB");
    }

    public final String get_BaseDirectory() {
        throw new Exception("STUB");
    }

    public final String get_RelativeSearchPath() {
        throw new Exception("STUB");
    }

    public final void add_UnhandledException(System.UnhandledExceptionEventHandler value) {
        throw new Exception("STUB");
    }

    public final void remove_UnhandledException(System.UnhandledExceptionEventHandler value) {
        throw new Exception("STUB");
    }

    public final String get_DynamicDirectory() {
        throw new Exception("STUB");
    }

    public final void SetDynamicBase(String path) {
        throw new Exception("STUB");
    }

    public final String get_FriendlyName() {
        throw new Exception("STUB");
    }

    public final int get_Id() {
        throw new Exception("STUB");
    }

    public final boolean get_IsFullyTrusted() {
        throw new Exception("STUB");
    }

    public final boolean get_IsHomogenous() {
        throw new Exception("STUB");
    }

    public final void add_DomainUnload(System.EventHandler value) {
        throw new Exception("STUB");
    }

    public final void remove_DomainUnload(System.EventHandler value) {
        throw new Exception("STUB");
    }

    public final void add_ProcessExit(System.EventHandler value) {
        throw new Exception("STUB");
    }

    public final void remove_ProcessExit(System.EventHandler value) {
        throw new Exception("STUB");
    }

    public final String ApplyPolicy(String assemblyName) {
        throw new Exception("STUB");
    }

    public static final System.AppDomain CreateDomain(String friendlyName) {
        throw new Exception("STUB");
    }

    public final int ExecuteAssembly(String assemblyFile) {
        throw new Exception("STUB");
    }

    public final int ExecuteAssembly(String assemblyFile, System.String[] args) {
        throw new Exception("STUB");
    }

    public final int ExecuteAssembly(String assemblyFile, System.String[] args, System.Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm) {
        throw new Exception("STUB");
    }

    public final int ExecuteAssemblyByName(System.Reflection.AssemblyName assemblyName, System.String[] args) {
        throw new Exception("STUB");
    }

    public final int ExecuteAssemblyByName(String assemblyName) {
        throw new Exception("STUB");
    }

    public final int ExecuteAssemblyByName(String assemblyName, System.String[] args) {
        throw new Exception("STUB");
    }

    public final Object GetData(String name) {
        throw new Exception("STUB");
    }

    public final void SetData(String name, Object data) {
        throw new Exception("STUB");
    }

    public final boolean IsDefaultAppDomain() {
        throw new Exception("STUB");
    }

    public final boolean IsFinalizingForUnload() {
        throw new Exception("STUB");
    }

    public static final void Unload(System.AppDomain domain) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly Load(System.Byte[] rawAssembly) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly Load(System.Byte[] rawAssembly, System.Byte[] rawSymbolStore) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyRef) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly Load(String assemblyString) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly[] ReflectionOnlyGetAssemblies() {
        throw new Exception("STUB");
    }

    public static final boolean get_MonitoringIsEnabled() {
        throw new Exception("STUB");
    }

    public static final void set_MonitoringIsEnabled(boolean value) {
        throw new Exception("STUB");
    }

    public final long get_MonitoringSurvivedMemorySize() {
        throw new Exception("STUB");
    }

    public static final long get_MonitoringSurvivedProcessMemorySize() {
        throw new Exception("STUB");
    }

    public final long get_MonitoringTotalAllocatedMemorySize() {
        throw new Exception("STUB");
    }

    public final System.TimeSpan get_MonitoringTotalProcessorTime() {
        throw new Exception("STUB");
    }

    public static final int GetCurrentThreadId() {
        throw new Exception("STUB");
    }

    public final boolean get_ShadowCopyFiles() {
        throw new Exception("STUB");
    }

    public final void AppendPrivatePath(String path) {
        throw new Exception("STUB");
    }

    public final void ClearPrivatePath() {
        throw new Exception("STUB");
    }

    public final void ClearShadowCopyPath() {
        throw new Exception("STUB");
    }

    public final void SetCachePath(String path) {
        throw new Exception("STUB");
    }

    public final void SetShadowCopyFiles() {
        throw new Exception("STUB");
    }

    public final void SetShadowCopyPath(String path) {
        throw new Exception("STUB");
    }

    public final System.Reflection.Assembly[] GetAssemblies() {
        throw new Exception("STUB");
    }

    public final void add_AssemblyLoad(System.AssemblyLoadEventHandler value) {
        throw new Exception("STUB");
    }

    public final void remove_AssemblyLoad(System.AssemblyLoadEventHandler value) {
        throw new Exception("STUB");
    }

    public final void add_AssemblyResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public final void remove_AssemblyResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public final void add_ReflectionOnlyAssemblyResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public final void remove_ReflectionOnlyAssemblyResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public final void add_TypeResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public final void remove_TypeResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public final void add_ResourceResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public final void remove_ResourceResolve(System.ResolveEventHandler value) {
        throw new Exception("STUB");
    }

    public final void SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy policy) {
        throw new Exception("STUB");
    }

    public final void SetThreadPrincipal(System.Security.Principal.IPrincipal principal) {
        throw new Exception("STUB");
    }

}
