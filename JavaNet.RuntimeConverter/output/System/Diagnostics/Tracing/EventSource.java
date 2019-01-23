package System.Diagnostics.Tracing;
public class EventSource {
    public static final void SetCurrentThreadActivityId(System.Guid activityId) {
        throw new Exception("STUB");
    }

    public static final void SetCurrentThreadActivityId(System.Guid activityId, System.Guid& oldActivityThatWillContinue) {
        throw new Exception("STUB");
    }

    public static final System.Guid get_CurrentThreadActivityId() {
        throw new Exception("STUB");
    }

    public final String get_Name() {
        throw new Exception("STUB");
    }

    public final System.Guid get_Guid() {
        throw new Exception("STUB");
    }

    public final boolean IsEnabled() {
        throw new Exception("STUB");
    }

    public final boolean IsEnabled(System.Diagnostics.Tracing.EventLevel level, System.Diagnostics.Tracing.EventKeywords keywords) {
        throw new Exception("STUB");
    }

    public final boolean IsEnabled(System.Diagnostics.Tracing.EventLevel level, System.Diagnostics.Tracing.EventKeywords keywords, System.Diagnostics.Tracing.EventChannel channel) {
        throw new Exception("STUB");
    }

    public final System.Diagnostics.Tracing.EventSourceSettings get_Settings() {
        throw new Exception("STUB");
    }

    public static final System.Guid GetGuid(System.Type eventSourceType) {
        throw new Exception("STUB");
    }

    public static final String GetName(System.Type eventSourceType) {
        throw new Exception("STUB");
    }

    public static final String GenerateManifest(System.Type eventSourceType, String assemblyPathToIncludeInManifest) {
        throw new Exception("STUB");
    }

    public static final String GenerateManifest(System.Type eventSourceType, String assemblyPathToIncludeInManifest, System.Diagnostics.Tracing.EventManifestOptions flags) {
        throw new Exception("STUB");
    }

    public final System.Exception get_ConstructionException() {
        throw new Exception("STUB");
    }

    public final String GetTrait(String key) {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

    public final void Write(String eventName) {
        throw new Exception("STUB");
    }

    public final void Write(String eventName, System.Diagnostics.Tracing.EventSourceOptions options) {
        throw new Exception("STUB");
    }

}
