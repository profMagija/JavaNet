package System.Diagnostics.Contracts;
public class ContractFailureKind {
    public int value__;

    public static final System.Diagnostics.Contracts.ContractFailureKind Precondition = 0;

    public static final System.Diagnostics.Contracts.ContractFailureKind Postcondition = 1;

    public static final System.Diagnostics.Contracts.ContractFailureKind PostconditionOnException = 2;

    public static final System.Diagnostics.Contracts.ContractFailureKind Invariant = 3;

    public static final System.Diagnostics.Contracts.ContractFailureKind Assert = 4;

    public static final System.Diagnostics.Contracts.ContractFailureKind Assume = 5;

}
