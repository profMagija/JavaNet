package System.Diagnostics.Tracing;
public class EventListener {
    public void Dispose() {
        throw new Exception("STUB");
    }

    public final void EnableEvents(System.Diagnostics.Tracing.EventSource eventSource, System.Diagnostics.Tracing.EventLevel level) {
        throw new Exception("STUB");
    }

    public final void EnableEvents(System.Diagnostics.Tracing.EventSource eventSource, System.Diagnostics.Tracing.EventLevel level, System.Diagnostics.Tracing.EventKeywords matchAnyKeyword) {
        throw new Exception("STUB");
    }

    public final void DisableEvents(System.Diagnostics.Tracing.EventSource eventSource) {
        throw new Exception("STUB");
    }

    public static final int EventSourceIndex(System.Diagnostics.Tracing.EventSource eventSource) {
        throw new Exception("STUB");
    }

}
