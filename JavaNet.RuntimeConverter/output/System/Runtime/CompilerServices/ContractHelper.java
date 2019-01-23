package System.Runtime.CompilerServices;
public class ContractHelper {
    public static final String RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind failureKind, String userMessage, String conditionText, System.Exception innerException) {
        throw new Exception("STUB");
    }

    public static final void TriggerFailure(System.Diagnostics.Contracts.ContractFailureKind kind, String displayMessage, String userMessage, String conditionText, System.Exception innerException) {
        throw new Exception("STUB");
    }

}
