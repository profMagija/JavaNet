package System.Threading;
public class Barrier {
    public final int get_ParticipantsRemaining() {
        throw new Exception("STUB");
    }

    public final int get_ParticipantCount() {
        throw new Exception("STUB");
    }

    public final long get_CurrentPhaseNumber() {
        throw new Exception("STUB");
    }

    public final long AddParticipant() {
        throw new Exception("STUB");
    }

    public final long AddParticipants(int participantCount) {
        throw new Exception("STUB");
    }

    public final void RemoveParticipant() {
        throw new Exception("STUB");
    }

    public final void RemoveParticipants(int participantCount) {
        throw new Exception("STUB");
    }

    public final void SignalAndWait() {
        throw new Exception("STUB");
    }

    public final void SignalAndWait(System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public final boolean SignalAndWait(System.TimeSpan timeout) {
        throw new Exception("STUB");
    }

    public final boolean SignalAndWait(System.TimeSpan timeout, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public final boolean SignalAndWait(int millisecondsTimeout) {
        throw new Exception("STUB");
    }

    public final boolean SignalAndWait(int millisecondsTimeout, System.Threading.CancellationToken cancellationToken) {
        throw new Exception("STUB");
    }

    public void Dispose() {
        throw new Exception("STUB");
    }

}
