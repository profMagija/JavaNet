package System.Reflection.Emit;
public class ILGenerator {
    public void Emit(System.Reflection.Emit.OpCode opcode) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, byte arg) {
        throw new Exception("STUB");
    }

    public final void Emit(System.Reflection.Emit.OpCode opcode, System.SByte arg) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, short arg) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, int arg) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, System.Reflection.MethodInfo meth) {
        throw new Exception("STUB");
    }

    public void EmitCalli(System.Reflection.Emit.OpCode opcode, System.Reflection.CallingConventions callingConvention, System.Type returnType, System.Type[] parameterTypes, System.Type[] optionalParameterTypes) {
        throw new Exception("STUB");
    }

    public void EmitCalli(System.Reflection.Emit.OpCode opcode, System.Runtime.InteropServices.CallingConvention unmanagedCallConv, System.Type returnType, System.Type[] parameterTypes) {
        throw new Exception("STUB");
    }

    public void EmitCall(System.Reflection.Emit.OpCode opcode, System.Reflection.MethodInfo methodInfo, System.Type[] optionalParameterTypes) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, System.Reflection.Emit.SignatureHelper signature) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, System.Reflection.ConstructorInfo con) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, System.Type cls) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, long arg) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, float arg) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, double arg) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, System.Reflection.Emit.Label label) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, System.Reflection.Emit.Label[] labels) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, System.Reflection.FieldInfo field) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, String str) {
        throw new Exception("STUB");
    }

    public void Emit(System.Reflection.Emit.OpCode opcode, System.Reflection.Emit.LocalBuilder local) {
        throw new Exception("STUB");
    }

    public System.Reflection.Emit.Label BeginExceptionBlock() {
        throw new Exception("STUB");
    }

    public void EndExceptionBlock() {
        throw new Exception("STUB");
    }

    public void BeginExceptFilterBlock() {
        throw new Exception("STUB");
    }

    public void BeginCatchBlock(System.Type exceptionType) {
        throw new Exception("STUB");
    }

    public void BeginFaultBlock() {
        throw new Exception("STUB");
    }

    public void BeginFinallyBlock() {
        throw new Exception("STUB");
    }

    public System.Reflection.Emit.Label DefineLabel() {
        throw new Exception("STUB");
    }

    public void MarkLabel(System.Reflection.Emit.Label loc) {
        throw new Exception("STUB");
    }

    public void ThrowException(System.Type excType) {
        throw new Exception("STUB");
    }

    public void EmitWriteLine(String value) {
        throw new Exception("STUB");
    }

    public void EmitWriteLine(System.Reflection.Emit.LocalBuilder localBuilder) {
        throw new Exception("STUB");
    }

    public void EmitWriteLine(System.Reflection.FieldInfo fld) {
        throw new Exception("STUB");
    }

    public System.Reflection.Emit.LocalBuilder DeclareLocal(System.Type localType) {
        throw new Exception("STUB");
    }

    public System.Reflection.Emit.LocalBuilder DeclareLocal(System.Type localType, boolean pinned) {
        throw new Exception("STUB");
    }

    public void UsingNamespace(String usingNamespace) {
        throw new Exception("STUB");
    }

    public void MarkSequencePoint(System.Diagnostics.SymbolStore.ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn) {
        throw new Exception("STUB");
    }

    public void BeginScope() {
        throw new Exception("STUB");
    }

    public void EndScope() {
        throw new Exception("STUB");
    }

    public int get_ILOffset() {
        throw new Exception("STUB");
    }

}
