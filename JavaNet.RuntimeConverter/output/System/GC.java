package System;
public class GC {
    public static final void AddMemoryPressure(long bytesAllocated) {
        throw new Exception("STUB");
    }

    public static final void RemoveMemoryPressure(long bytesAllocated) {
        throw new Exception("STUB");
    }

    public static final int GetGeneration(Object obj) {
        throw new Exception("STUB");
    }

    public static final void Collect(int generation) {
        throw new Exception("STUB");
    }

    public static final void Collect() {
        throw new Exception("STUB");
    }

    public static final void Collect(int generation, System.GCCollectionMode mode) {
        throw new Exception("STUB");
    }

    public static final void Collect(int generation, System.GCCollectionMode mode, boolean blocking) {
        throw new Exception("STUB");
    }

    public static final void Collect(int generation, System.GCCollectionMode mode, boolean blocking, boolean compacting) {
        throw new Exception("STUB");
    }

    public static final int CollectionCount(int generation) {
        throw new Exception("STUB");
    }

    public static final void KeepAlive(Object obj) {
        throw new Exception("STUB");
    }

    public static final int GetGeneration(System.WeakReference wo) {
        throw new Exception("STUB");
    }

    public static final int get_MaxGeneration() {
        throw new Exception("STUB");
    }

    public static final void WaitForPendingFinalizers() {
        throw new Exception("STUB");
    }

    public static final void SuppressFinalize(Object obj) {
        throw new Exception("STUB");
    }

    public static final void ReRegisterForFinalize(Object obj) {
        throw new Exception("STUB");
    }

    public static final long GetTotalMemory(boolean forceFullCollection) {
        throw new Exception("STUB");
    }

    public static final long GetAllocatedBytesForCurrentThread() {
        throw new Exception("STUB");
    }

    public static final void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold) {
        throw new Exception("STUB");
    }

    public static final void CancelFullGCNotification() {
        throw new Exception("STUB");
    }

    public static final System.GCNotificationStatus WaitForFullGCApproach() {
        throw new Exception("STUB");
    }

    public static final System.GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final System.GCNotificationStatus WaitForFullGCComplete() {
        throw new Exception("STUB");
    }

    public static final System.GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public static final boolean TryStartNoGCRegion(long totalSize) {
        throw new Exception("STUB");
    }

    public static final boolean TryStartNoGCRegion(long totalSize, long lohSize) {
        throw new Exception("STUB");
    }

    public static final boolean TryStartNoGCRegion(long totalSize, boolean disallowFullBlockingGC) {
        throw new Exception("STUB");
    }

    public static final boolean TryStartNoGCRegion(long totalSize, long lohSize, boolean disallowFullBlockingGC) {
        throw new Exception("STUB");
    }

    public static final void EndNoGCRegion() {
        throw new Exception("STUB");
    }

}
