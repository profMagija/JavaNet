using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using java.lang;
using JavaNet.Runtime.Plugs;
using JavaNet.Runtime.Plugs.NativeImpl;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace JavaNet
{
    abstract class MethodAction
    {
        public abstract IEnumerable<JavaValue> RequiredValues { get; }
        public abstract bool Pure { get; }
        public abstract IEnumerable<Instruction> Generate(ActionBlock curBlock);
    }

    class ConstantSetAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public JavaValue Value { get; }

        public ConstantSetAction(CalculatedValue target, JavaValue value)
        {
            Target = target;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Target} = {Value}";
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] {Value};
        public override bool Pure => true;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock) => 
            Value.GetValue()
                .Concat(Target.StoreValue());
    }

    class ArrayIndexAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public JavaValue Array { get; }
        public JavaValue Index { get; }

        public ArrayIndexAction(CalculatedValue target, JavaValue array, JavaValue index)
        {
            Target = target;
            Array = array;
            Index = index;
        }

        public override string ToString() => $"{Target} = {Array}[ {Index} ]";
        public override IEnumerable<JavaValue> RequiredValues => new[] {Array, Index};

        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Array.GetValue());
            l.AddRange(Index.GetValue());
            l.Add(Instruction.Create(OpCodes.Ldelem_Any, Array.ActualType.GetElementType()));
            l.AddRange(Target.StoreValue());
            return l;
        }

    }

    class ArraySetAction : MethodAction
    {
        public JavaValue Array { get; }
        public JavaValue Index { get; }
        public JavaValue Value { get; }

        public ArraySetAction(JavaValue array, JavaValue index, JavaValue value)
        {
            Array = array;
            Index = index;
            Value = value;
        }

        public override string ToString() => $"{Array}[ {Index} ] = {Value}";

        public override IEnumerable<JavaValue> RequiredValues => new[] {Array, Index, Value};

        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Array.GetValue());
            l.AddRange(Index.GetValue());
            l.AddRange(Value.GetValue());
            l.Add(Instruction.Create(OpCodes.Stelem_Any, Array.ActualType.GetElementType()));
            return l;
        }
    }



    class ArrayLengthAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public JavaValue Array { get; }

        public ArrayLengthAction(CalculatedValue target, JavaValue array)
        {
            Target = target;
            Array = array;
        }

        public override string ToString() => $"{Target} = lengthOf {Array}";
        public override IEnumerable<JavaValue> RequiredValues => new[] { Array };

        public override bool Pure => false;


        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Array.GetValue());
            l.Add(Instruction.Create(OpCodes.Ldlen));
            l.AddRange(Target.StoreValue());
            return l;
        }

    }

    class BinaryOperationAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public Op Operation { get; }
        public JavaValue Value1 { get; }
        public JavaValue Value2 { get; }

        private readonly MethodInfo _fcmpl = typeof(Intrinsics).GetMethod("Fcmpl") ?? throw new NullReferenceException();
        private readonly MethodInfo _fcmpg = typeof(Intrinsics).GetMethod("Fcmpg") ?? throw new NullReferenceException();
        private readonly MethodInfo _lcmp = typeof(Intrinsics).GetMethod("Lcmp") ?? throw new NullReferenceException();

        public BinaryOperationAction(CalculatedValue target, Op operation, JavaValue value1, JavaValue value2)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Operation = operation;
            Value1 = value1 ?? throw new ArgumentNullException(nameof(value1));
            Value2 = value2 ?? throw new ArgumentNullException(nameof(value2));
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] {Value1, Value2};

        public static Func<CalculatedValue, JavaValue, JavaValue, BinaryOperationAction> Creator(Op operation) =>
            (a, b, c) => new BinaryOperationAction(a, operation, b, c);

        public override string ToString() => $"{Target} = {Value1} {Operation.ToString().ToLower()} {Value2}";

        public enum Op
        {
            Add, Sub, Mul, Div, Rem, Shl, Shr, Ushr, And, Or, Xor, Lcmp, Fcmpg, Fcmpl
        }

        public override bool Pure => true;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Value1.GetValue());
            l.AddRange(Value2.GetValue());
            switch (Operation)
            {
                case Op.Add:
                    l.Add(Instruction.Create(OpCodes.Add));
                    break;
                case Op.Sub:
                    l.Add(Instruction.Create(OpCodes.Sub));
                    break;
                case Op.Mul:
                    l.Add(Instruction.Create(OpCodes.Mul));
                    break;
                case Op.Div:
                    l.Add(Instruction.Create(OpCodes.Div));
                    break;
                case Op.Rem:
                    l.Add(Instruction.Create(OpCodes.Rem));
                    break;
                case Op.Shl:
                    l.Add(Instruction.Create(OpCodes.Shl));
                    break;
                case Op.Shr:
                    l.Add(Instruction.Create(OpCodes.Shr));
                    break;
                case Op.Ushr:
                    l.Add(Instruction.Create(OpCodes.Shr_Un));
                    break;
                case Op.And:
                    l.Add(Instruction.Create(OpCodes.And));
                    break;
                case Op.Or:
                    l.Add(Instruction.Create(OpCodes.Or));
                    break;
                case Op.Xor:
                    l.Add(Instruction.Create(OpCodes.Xor));
                    break;
                case Op.Lcmp:
                    l.Add(Instruction.Create(OpCodes.Call, JavaAssemblyBuilder.Instance.Import(_lcmp)));
                    break;
                case Op.Fcmpg:
                    l.Add(Instruction.Create(OpCodes.Call, JavaAssemblyBuilder.Instance.Import(_fcmpg)));
                    break;
                case Op.Fcmpl:
                    l.Add(Instruction.Create(OpCodes.Call, JavaAssemblyBuilder.Instance.Import(_fcmpl)));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            l.AddRange(Target.StoreValue());
            return l;
        }
    }
    class UnaryOperationAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public Op Operation { get; }
        public JavaValue Value { get; }

        public UnaryOperationAction(CalculatedValue target, Op operation, JavaValue value)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Operation = operation;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] { Value };

        public static Func<CalculatedValue, JavaValue, UnaryOperationAction> Creator(Op operation) =>
            (a, b) => new UnaryOperationAction(a, operation, b);

        public override string ToString() => $"{Target} = {Operation.ToString().ToLower()} {Value}";

        public enum Op
        {
            Neg, ToI, ToL, ToD, ToF, ToB, ToC, ToS
        }

        public override bool Pure => true;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Value.GetValue());
            switch (Operation)
            {
                case Op.Neg:
                    l.Add(Instruction.Create(OpCodes.Neg));
                    break;
                case Op.ToI:
                    l.Add(Instruction.Create(OpCodes.Conv_I4));
                    break;
                case Op.ToL:
                    l.Add(Instruction.Create(OpCodes.Conv_I8));
                    break;
                case Op.ToD:
                    l.Add(Instruction.Create(OpCodes.Conv_R8));
                    break;
                case Op.ToF:
                    l.Add(Instruction.Create(OpCodes.Conv_R4));
                    break;
                case Op.ToB:
                    l.Add(Instruction.Create(OpCodes.Conv_I1));
                    break;
                case Op.ToC:
                    l.Add(Instruction.Create(OpCodes.Conv_U2));
                    break;
                case Op.ToS:
                    l.Add(Instruction.Create(OpCodes.Conv_I2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            l.AddRange(Target.StoreValue());
            return l;
        }

    }

    class ReturnAction : MethodAction
    {
        public JavaValue Value { get; }

        public ReturnAction(JavaValue value = null)
        {
            Value = value;
        }

        public override IEnumerable<JavaValue> RequiredValues => Value != null ? new[] {Value} : new JavaValue[0];

        public override string ToString() => Value != null ? $"return {Value}" : "return";
        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            if (Value != null)
                return Value.GetValue().Concat(new[] {Instruction.Create(OpCodes.Ret)});
            return new[] {Instruction.Create(OpCodes.Ret)};
        }
    }

    class ThrowAction : MethodAction
    {
        public JavaValue Value { get; }

        public ThrowAction(JavaValue value)
        {
            Value = value;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] {Value};

        public override string ToString() => $"throw {Value}";
        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            return Value.GetValue().Concat(new[] {Instruction.Create(OpCodes.Throw)});
        }
    }

    class MonitorAction : MethodAction
    {
        public JavaValue Value { get; }
        public bool IsEnter { get; }

        public MonitorAction(JavaValue value, bool isEnter)
        {
            Value = value;
            IsEnter = isEnter;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] { Value };

        public override string ToString() => $"{(IsEnter ? "monitor_enter" : "monitor_exit")} {Value}";
        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            return Value.GetValue()
                .Concat(new[]
                {
                    Instruction.Create(OpCodes.Call,
                        JavaAssemblyBuilder.Instance.Import(typeof(Monitor).GetMethod(IsEnter ? "Enter" : "Exit",
                            new[] {typeof(object)})))
                });
        }
    }

    class NewArrayAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public TypeReference Type { get; }
        public JavaValue Count { get; }

        public NewArrayAction(CalculatedValue target, TypeReference type, JavaValue count)
        {
            Target = target;
            Type = type;
            Count = count;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] {Count};

        public override string ToString()
        {
            return $"{Target} = new {Type}[ {Count} ]";
        }
        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Count.GetValue());
            l.Add(Instruction.Create(OpCodes.Newarr, Type));
            l.AddRange(Target.StoreValue());
            return l;
        }
    }

    class ConditionalJumpAction : MethodAction
    {
        public JumpCondition Condition { get; }
        public JavaValue ValueCmp { get; }
        public JavaValue Value { get; }
        public ActionBlock Target { get; }

        public ConditionalJumpAction(JavaValue value, ActionBlock target, JumpCondition condition, JavaValue valueCmp = null)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Condition = condition;
            ValueCmp = valueCmp;
        }

        public override IEnumerable<JavaValue> RequiredValues => ValueCmp != null ?  new[] {Value, ValueCmp} : new[] {Value};

        public override string ToString() =>
            ValueCmp == null
                ? $"if {Value} {Condition.ToString().ToLower()} 0 goto {Target.JavaOffset:X4}"
                : $"if {Value} {Condition.ToString().ToLower()} {ValueCmp} goto {Target.JavaOffset:X4}";

        public enum JumpCondition
        {
            Eq,
            Ne,
            Lt,
            Le,
            Gt,
            Ge
        }
        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            var useLeave = Target.HandlerNum != curBlock.HandlerNum;
            var target = useLeave ? Instruction.Create(OpCodes.Leave, Target.GetFirstNetOp()) : Target.GetFirstNetOp();

            l.AddRange(Value.GetValue());
            if (ValueCmp == null)
            {
                switch (Condition)
                {
                    case JumpCondition.Eq:
                        l.Add(Instruction.Create(OpCodes.Brfalse, target));
                        return l;
                    case JumpCondition.Ne:
                        l.Add(Instruction.Create(OpCodes.Brtrue, target));
                        return l;
                }
            }
            l.AddRange(ValueCmp?.GetValue() ?? new[] {Instruction.Create(OpCodes.Ldc_I4_0)});
            switch (Condition)
            {
                case JumpCondition.Eq:
                    l.Add(Instruction.Create(OpCodes.Beq, target));
                    break;
                case JumpCondition.Ne:
                    l.Add(Instruction.Create(OpCodes.Bne_Un, target));
                    break;
                case JumpCondition.Lt:
                    l.Add(Instruction.Create(OpCodes.Blt, target));
                    break;
                case JumpCondition.Le:
                    l.Add(Instruction.Create(OpCodes.Ble, target));
                    break;
                case JumpCondition.Gt:
                    l.Add(Instruction.Create(OpCodes.Bgt, target));
                    break;
                case JumpCondition.Ge:
                    l.Add(Instruction.Create(OpCodes.Bge, target));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (useLeave)
            {
                var afterLeave = Instruction.Create(OpCodes.Nop);
                l.Add(Instruction.Create(OpCodes.Br, afterLeave));
                l.Add(target);
                l.Add(afterLeave);
            }

            return l;
        }
    }

    class UnconditionalJump : MethodAction
    {
        public ActionBlock Target { get; }

        public UnconditionalJump(ActionBlock target)
        {
            Target = target;
        }

        public override IEnumerable<JavaValue> RequiredValues => new JavaValue[0];

        public override string ToString() => $"goto {Target.JavaOffset:X4}";
        public override bool Pure => false;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            return new[] {Instruction.Create(curBlock.HandlerNum == Target.HandlerNum ? OpCodes.Br : OpCodes.Leave, Target.GetFirstNetOp())};
        }
    }

    class MultiNewArrayAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public TypeReference TargetType { get; }
        public JavaValue[] Dimensions { get; }

        public MultiNewArrayAction(CalculatedValue target, TypeReference targetType, JavaValue[] dimensions)
        {
            TargetType = targetType;
            Dimensions = dimensions;
            Target = target;
        }

        public override IEnumerable<JavaValue> RequiredValues => Dimensions;

        public override string ToString() => $"{Target} = new {TargetType.FullName} [ {string.Join(", ", Dimensions.AsEnumerable())} ]";
        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            return new[] {Instruction.Create(OpCodes.Ldnull)};
        }
    }

    class GetStaticAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public FieldReference Field { get; }

        public GetStaticAction(CalculatedValue target, FieldReference field)
        {
            Target = target;
            Field = field;
        }

        public override IEnumerable<JavaValue> RequiredValues => new JavaValue[0];
        public override string ToString()
        {
            return $"{Target} = {Field.FullName}";
        }
        public override bool Pure => false;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.Add(Instruction.Create(OpCodes.Ldsfld, Field));
            l.AddRange(Target.StoreValue());
            return l;
        }
    }

    class SetStaticAction : MethodAction
    {
        public JavaValue Value { get; }
        public FieldReference Field { get; }

        public SetStaticAction(JavaValue value, FieldReference field)
        {
            Value = value;
            Field = field;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] {Value};
        public override string ToString()
        {
            return $"{Field.FullName} = {Value}";
        }
        public override bool Pure => false;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Value.GetValue());
            l.Add(Instruction.Create(OpCodes.Stsfld, Field));
            return l;
        }
    }

    class GetFieldAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public JavaValue From { get; }
        public FieldReference Field { get; }

        public GetFieldAction(CalculatedValue target, JavaValue from, FieldReference field)
        {
            Target = target;
            From = @from;
            Field = field;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] {From};
        public override string ToString()
        {
            return $"{Target} = {From}.{Field.Name}";
        }
        public override bool Pure => false;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(From.GetValue());
            l.Add(Instruction.Create(OpCodes.Ldfld, Field));
            l.AddRange(Target.StoreValue());
            return l;
        }
    }

    class SetFieldAction : MethodAction
    {
        public JavaValue Target { get; }
        public JavaValue Value { get; }
        public FieldReference Field { get; }

        public SetFieldAction(JavaValue target, JavaValue value, FieldReference field)
        {
            Target = target;
            Value = value;
            Field = field;
        }

        public override IEnumerable<JavaValue> RequiredValues => new [] {Value, Target};
        public override string ToString()
        {
            return $"{Target}.{Field.Name} = {Value}";
        }
        public override bool Pure => false;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Target.GetValue());
            l.AddRange(Value.GetValue());
            l.Add(Instruction.Create(OpCodes.Stfld, Field));
            return l;
        }
    }

    class InvokeAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public JavaValue Instance { get; }
        public MethodReference Method { get; }
        public JavaValue[] Args { get; }
        public bool Virtual { get; }

        public InvokeAction(CalculatedValue target, JavaValue instance, MethodReference method, JavaValue[] args, bool virt = false)
        {
            Target = target;
            Instance = instance;
            Method = method;
            Args = args;
            Virtual = virt;

            if (Instance == null)
                Debug.Assert(!Virtual || !Method.HasThis);
        }

        public override IEnumerable<JavaValue> RequiredValues => (Instance == null ? new JavaValue[0] : new[] {Instance}).Concat(Args);

        public override string ToString() => (Target != null ? $"{Target} = " : "") +
                                             (Instance != null ? $"invoke {Instance} " : "static ") +
                                             (Virtual ? "virtual " : "") +
                                             $"{Method.FullName} ( " + string.Join(", ", Args.AsEnumerable()) + " )";
        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();

            var args = new List<JavaValue>();

            if (Instance != null)
                args.Add(Instance);
            
            args.AddRange(Args);

            if (!Method.HasThis)
                Debug.Assert(args.Count == Method.Parameters.Count);
            else
                Debug.Assert(args.Count == Method.Parameters.Count + 1);

            foreach (var value in args)
            {
                l.AddRange(value.GetValue());
            }

            var isVirt = Method.Resolve()?.IsVirtual ?? false;

            l.Add(Instruction.Create(Virtual && isVirt ? OpCodes.Callvirt : OpCodes.Call, Method));

            if (Target != null)
            {
                if (Method.MethodReturnType.CustomAttributes.FirstOrDefault(x => x.AttributeType.FullName == typeof(ActualTypeAttribute).FullName) is CustomAttribute atr)
                {
                    var s = (string) atr.ConstructorArguments[0].Value;
                    l.Add(Instruction.Create(OpCodes.Castclass, JavaAssemblyBuilder.Instance.ResolveTypeReference(s)));
                }
                l.AddRange(Target.StoreValue());
            }

            return l;
        }
    }

    class ConstructorAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public MethodReference Method { get; }
        public JavaValue[] Args { get; }

        public ConstructorAction(CalculatedValue target, MethodReference method, JavaValue[] args)
        {
            Target = target;
            Method = method;
            Args = args;
        }

        public override IEnumerable<JavaValue> RequiredValues => Args;
        public override string ToString()
        {
            return $"{Target} = new {Method.FullName} ( " + string.Join(", ", Args.AsEnumerable()) + " )";
        }
        public override bool Pure => false;

        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            foreach (var arg in Args)
            {
                l.AddRange(arg.GetValue());
            }

            l.Add(Instruction.Create(OpCodes.Newobj, Method));

            if (Target != null)
                l.AddRange(Target.StoreValue());
            return l;
        }
    }

    class CastAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public TypeReference Type { get; }
        public JavaValue Value { get; }

        public CastAction(CalculatedValue target, TypeReference type, JavaValue value)
        {
            Target = target;
            Type = type;
            Value = value;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] { Value };

        public override string ToString()
        {
            return $"{Target} = ({Type}) {Value}";
        }
        public override bool Pure => false;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Value.GetValue());
            if (JavaAssemblyBuilder.Instance.CastPlugs.TryGetValue(Type.FullName, out var method))
            {
                l.Add(Instruction.Create(OpCodes.Call, method));
            }
            else
            {
                l.Add(Instruction.Create(OpCodes.Castclass, Type));
            }

            l.AddRange(Target.StoreValue());
            return l;
        }
    }
    class InstanceOfAction : MethodAction
    {
        public CalculatedValue Target { get; }
        public TypeReference Type { get; }
        public JavaValue Value { get; }

        public InstanceOfAction(CalculatedValue target, TypeReference type, JavaValue value)
        {
            Target = target;
            Type = type;
            Value = value;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] { Value };

        public override string ToString()
        {
            return $"{Target} = {Value} instance_of {Type}";
        }
        public override bool Pure => true;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Value.GetValue());
            if (JavaAssemblyBuilder.Instance.InstanceOfPlugs.TryGetValue(Type.FullName, out var method))
            {
                l.Add(Instruction.Create(OpCodes.Call, method));
            }
            else
            {
                l.Add(Instruction.Create(OpCodes.Isinst, Type));
                l.Add(Instruction.Create(OpCodes.Ldnull));
                l.Add(Instruction.Create(OpCodes.Cgt_Un));
            }

            l.AddRange(Target.StoreValue());
            return l;
        }
    }

    class LookupSwitchAction : MethodAction
    {
        public JavaValue Value { get; }
        public ActionBlock DefaultOffset { get; }
        public KeyValuePair<int,ActionBlock>[] Table { get; }

        public LookupSwitchAction(JavaValue value, ActionBlock defaultOffset, KeyValuePair<int, ActionBlock>[] table)
        {
            Value = value;
            DefaultOffset = defaultOffset;
            Table = table;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] {Value};

        public override string ToString()
        {
            return $"switch.l [ {Value} ] {{ " + string.Join("; ", Table.Select(x => $"{x.Key} -> 0x{x.Value.JavaOffset:X4}")) + $"; * -> 0x{DefaultOffset:X4} }}";
        }
        public override bool Pure => false;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Value.GetValue());

            foreach (var (cmpValue, target) in Table)
            {
                l.Add(Instruction.Create(OpCodes.Dup));
                l.Add(Instruction.Create(OpCodes.Ldc_I4, cmpValue));
                l.Add(Instruction.Create(OpCodes.Beq, target.GetFirstNetOp()));
            }

            l.Add(Instruction.Create(OpCodes.Pop));
            l.Add(Instruction.Create(OpCodes.Br, DefaultOffset.GetFirstNetOp()));

            return l;
        }
    }

    class TableSwitchAction : MethodAction
    {
        public JavaValue Value { get; }
        public ActionBlock DefaultOffset { get; }
        public int Low { get; }
        public int High { get; }
        public ActionBlock[] Table { get; }

        public TableSwitchAction(JavaValue value, ActionBlock defaultOffset, int low, int high, ActionBlock[] table)
        {
            Value = value;
            DefaultOffset = defaultOffset;
            Low = low;
            High = high;
            Table = table;
        }

        public override IEnumerable<JavaValue> RequiredValues => new[] { Value };

        public override string ToString()
        {
            return $"switch.t [ {Value} ] {{ " + string.Join("; ", Table.Select((x, i)=> $"{i + Low} -> 0x{x:X4}")) + $"; * -> 0x{DefaultOffset:X4} }}";
        }
        public override bool Pure => false;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            l.AddRange(Value.GetValue());
            if (Low != 0)
            {
                l.Add(Instruction.Create(OpCodes.Ldc_I4, Low));
                l.Add(Instruction.Create(OpCodes.Add));
            }

            l.Add(Instruction.Create(OpCodes.Switch, Table.Select(x => x.GetFirstNetOp()).ToArray()));
            l.Add(Instruction.Create(OpCodes.Br, DefaultOffset.GetFirstNetOp()));
            return l;
        }


    }

    class MappingAction : MethodAction
    {
        public List<(JavaValue to, JavaValue from)> Mappings { get; }

        public MappingAction(List<(JavaValue to, JavaValue from)> mappings)
        {
            Debug.Assert(mappings.All(x => x.from != null && x.to != null));
            Mappings = mappings;
        }

        public override IEnumerable<JavaValue> RequiredValues => Mappings.Select(x => x.from);

        public override string ToString()
        {
            return "map { " + string.Join("; ", Mappings.Select(map => $"{map.to} <- {map.from}")) + " }";
        }
        public override bool Pure => true;
        public override IEnumerable<Instruction> Generate(ActionBlock curBlock)
        {
            var l = new List<Instruction>();
            foreach (var (lhs, rhs) in Mappings)
            {
                if (lhs is CalculatedValue)
                    l.AddRange(rhs.GetValue());
            }

            foreach (var (lhs, _) in Mappings.AsEnumerable().Reverse())
            {
                if (lhs is CalculatedValue cv)
                    l.AddRange(cv.StoreValue());
            }

            return l;
        }
    }
}
