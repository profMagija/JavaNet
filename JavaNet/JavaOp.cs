using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace JavaNet
{
    public abstract class JavaOp
    {
        public int Offset { get; }
        public JavaInstruction Instr { get; }
        public abstract string ArgsString { get; }

        public override string ToString()
        {
            return $"0x{Offset:X4} {Instr} {ArgsString}";
        }

        protected JavaOp(int offset, JavaInstruction instr)
        {
            Offset = offset;
            Instr = instr;
        }

        public virtual IEnumerable<int> JumpTargets => Enumerable.Empty<int>();

        public class Niladic : JavaOp
        {
            public Niladic(int start, JavaInstruction instr) : base(start, instr)
            {

            }

            public override string ArgsString => "";

            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                var noActions = new MethodAction[0];
                var typeSystem = JavaAssemblyBuilder.Instance.TypeSystem;
                switch (Instr)
                {
                    case JavaInstruction.nop:
                        return (curState, noActions);
                    case JavaInstruction.aconst_null:
                        return (curState.Push(new ConstantValue(typeSystem.Object, null)), noActions);
                    case JavaInstruction.iconst_m1:
                    case JavaInstruction.iconst_0:
                    case JavaInstruction.iconst_1:
                    case JavaInstruction.iconst_2:
                    case JavaInstruction.iconst_3:
                    case JavaInstruction.iconst_4:
                    case JavaInstruction.iconst_5:
                        return (curState
                                .Push(new ConstantValue(typeSystem.Int32, Instr - JavaInstruction.iconst_0)),
                            noActions);
                    case JavaInstruction.lconst_0:
                    case JavaInstruction.lconst_1:
                        return (curState
                                .Push(new ConstantValue(typeSystem.Int64, (long) (Instr - JavaInstruction.lconst_0)))
                                .Push(null),
                            noActions);
                    case JavaInstruction.fconst_0:
                    case JavaInstruction.fconst_1:
                    case JavaInstruction.fconst_2:
                        return (curState
                                .Push(new ConstantValue(typeSystem.Single, (float) (Instr - JavaInstruction.fconst_0))),
                            noActions);
                    case JavaInstruction.dconst_0:
                    case JavaInstruction.dconst_1:
                        return (curState
                                .Push(new ConstantValue(typeSystem.Double, (double) (Instr - JavaInstruction.dconst_0)))
                                .Push(null),
                            noActions);
                    case JavaInstruction.iload_0:
                    case JavaInstruction.iload_1:
                    case JavaInstruction.iload_2:
                    case JavaInstruction.iload_3:
                    {
                        var toLoad = curState.Load(Instr - JavaInstruction.iload_0);
                        return (curState.Push(toLoad), noActions);
                    }
                    case JavaInstruction.lload_0:
                    case JavaInstruction.lload_1:
                    case JavaInstruction.lload_2:
                    case JavaInstruction.lload_3:
                    {
                        var toLoad = curState.Load(Instr - JavaInstruction.lload_0);
                        return (curState.Push(toLoad).Push(null), noActions);
                    }
                    case JavaInstruction.fload_0:
                    case JavaInstruction.fload_1:
                    case JavaInstruction.fload_2:
                    case JavaInstruction.fload_3:
                    {
                        var toLoad = curState.Load(Instr - JavaInstruction.fload_0);
                        return (curState.Push(toLoad), noActions);
                    }
                    case JavaInstruction.dload_0:
                    case JavaInstruction.dload_1:
                    case JavaInstruction.dload_2:
                    case JavaInstruction.dload_3:
                    {
                        var toLoad = curState.Load(Instr - JavaInstruction.dload_0);
                        return (curState.Push(toLoad).Push(null), noActions);
                    }
                    case JavaInstruction.aload_0:
                    case JavaInstruction.aload_1:
                    case JavaInstruction.aload_2:
                    case JavaInstruction.aload_3:
                    {
                        var toLoad = curState.Load(Instr - JavaInstruction.aload_0);
                        return (curState.Push(toLoad), noActions);
                    }
                    case JavaInstruction.iaload:
                    case JavaInstruction.faload:
                    case JavaInstruction.baload:
                    case JavaInstruction.caload:
                    case JavaInstruction.saload:
                    case JavaInstruction.aaload:
                    case JavaInstruction.laload:
                    case JavaInstruction.daload:
                    {
                        var newState = curState.Pop(out var index).Pop(out var array);
                        var value = new CalculatedValue(array.ActualType.GetElementType());
                        newState = newState.Push(value);
                        if (Instr == JavaInstruction.laload || Instr == JavaInstruction.daload)
                            newState = newState.Push(null);
                        return (newState, new[] {new ArrayIndexAction(value, array, index)});
                    }
                    case JavaInstruction.istore_0:
                    case JavaInstruction.istore_1:
                    case JavaInstruction.istore_2:
                    case JavaInstruction.istore_3:
                    {
                        return (curState.Pop(out var v).Store(Instr - JavaInstruction.istore_0, v), noActions);
                    }
                    case JavaInstruction.lstore_0:
                    case JavaInstruction.lstore_1:
                    case JavaInstruction.lstore_2:
                    case JavaInstruction.lstore_3:
                    {
                        return (curState.Pop().Pop(out var v).Store(Instr - JavaInstruction.lstore_0, v), noActions);
                    }
                    case JavaInstruction.fstore_0:
                    case JavaInstruction.fstore_1:
                    case JavaInstruction.fstore_2:
                    case JavaInstruction.fstore_3:
                    {
                        return (curState.Pop(out var v).Store(Instr - JavaInstruction.fstore_0, v), noActions);
                    }
                    case JavaInstruction.dstore_0:
                    case JavaInstruction.dstore_1:
                    case JavaInstruction.dstore_2:
                    case JavaInstruction.dstore_3:
                    {
                        return (curState.Pop().Pop(out var v).Store(Instr - JavaInstruction.dstore_0, v), noActions);
                    }
                    case JavaInstruction.astore_0:
                    case JavaInstruction.astore_1:
                    case JavaInstruction.astore_2:
                    case JavaInstruction.astore_3:
                    {
                        return (curState.Pop(out var v).Store(Instr - JavaInstruction.astore_0, v), noActions);
                    }
                    case JavaInstruction.iastore:
                    case JavaInstruction.fastore:
                    case JavaInstruction.bastore:
                    case JavaInstruction.castore:
                    case JavaInstruction.sastore:
                    case JavaInstruction.aastore:
                    case JavaInstruction.lastore:
                    case JavaInstruction.dastore:
                    {
                        var newState = curState;
                        if (Instr == JavaInstruction.lastore || Instr == JavaInstruction.dastore)
                            newState = newState.Pop();
                        newState = newState.Pop(out var value).Pop(out var index).Pop(out var array);
                        return (newState, new[] {new ArraySetAction(array, index, value)});
                    }
                    case JavaInstruction.pop:
                        return (curState.Pop(out _), noActions);
                    case JavaInstruction.pop2:
                        return (curState.Pop().Pop(out _), noActions);
                    case JavaInstruction.dup:
                    {
                        return (
                            curState
                                .Pop(out var value)
                                .Push(value).Push(value),
                            noActions);
                    }
                    case JavaInstruction.dup_x1:
                    {
                        return (
                            curState
                                .Pop(out var value1).Pop(out var value2)
                                .Push(value1).Push(value2).Push(value1),
                            noActions);
                    }
                    case JavaInstruction.dup_x2:
                    {
                        return (
                            curState
                                .Pop(out var value1).Pop(out var value2).Pop(out var value3)
                                .Push(value1).Push(value3).Push(value2).Push(value1),
                            noActions);
                    }
                    case JavaInstruction.dup2:
                    {
                        return (
                            curState
                                .Pop(out var value1).Pop(out var value2)
                                .Push(value2).Push(value1).Push(value2).Push(value1),
                            noActions);
                    }
                    case JavaInstruction.dup2_x1:
                    {
                        return (
                            curState
                                .Pop(out var value1).Pop(out var value2).Pop(out var value3)
                                .Push(value2).Push(value1).Push(value3).Push(value2).Push(value1),
                            noActions);
                    }
                    case JavaInstruction.dup2_x2:
                    {
                        return (
                            curState
                                .Pop(out var value1).Pop(out var value2).Pop(out var value3).Pop(out var value4)
                                .Push(value2).Push(value1).Push(value4).Push(value3).Push(value2).Push(value1),
                            noActions);
                    }
                    case JavaInstruction.swap:
                    {
                        return (
                            curState
                                .Pop(out var value1).Pop(out var value2)
                                .Push(value1).Push(value2),
                            noActions);
                    }
                    case JavaInstruction.iadd:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x + y,                      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Add));
                    case JavaInstruction.ladd:  return BinaryOp<long  ,long  >(curState, true,  (x, y) => x + y,                      typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Add));
                    case JavaInstruction.fadd:  return BinaryOp<float ,float >(curState, false, (x, y) => x + y,                      typeSystem.Single, BinaryOperationAction.Creator(BinaryOperationAction.Op.Add));
                    case JavaInstruction.dadd:  return BinaryOp<double,double>(curState, true,  (x, y) => x + y,                      typeSystem.Double, BinaryOperationAction.Creator(BinaryOperationAction.Op.Add));
                    case JavaInstruction.isub:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x - y,                      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Sub));
                    case JavaInstruction.lsub:  return BinaryOp<long  ,long  >(curState, true,  (x, y) => x - y,                      typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Sub));
                    case JavaInstruction.fsub:  return BinaryOp<float ,float >(curState, false, (x, y) => x - y,                      typeSystem.Single, BinaryOperationAction.Creator(BinaryOperationAction.Op.Sub));
                    case JavaInstruction.dsub:  return BinaryOp<double,double>(curState, true,  (x, y) => x - y,                      typeSystem.Double, BinaryOperationAction.Creator(BinaryOperationAction.Op.Sub));
                    case JavaInstruction.imul:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x * y,                      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Mul));
                    case JavaInstruction.lmul:  return BinaryOp<long  ,long  >(curState, true,  (x, y) => x * y,                      typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Mul));
                    case JavaInstruction.fmul:  return BinaryOp<float ,float >(curState, false, (x, y) => x * y,                      typeSystem.Single, BinaryOperationAction.Creator(BinaryOperationAction.Op.Mul));
                    case JavaInstruction.dmul:  return BinaryOp<double,double>(curState, true,  (x, y) => x * y,                      typeSystem.Double, BinaryOperationAction.Creator(BinaryOperationAction.Op.Mul));
                    case JavaInstruction.idiv:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x / y,                      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Div));
                    case JavaInstruction.ldiv:  return BinaryOp<long  ,long  >(curState, true,  (x, y) => x / y,                      typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Div));
                    case JavaInstruction.fdiv:  return BinaryOp<float ,float >(curState, false, (x, y) => x / y,                      typeSystem.Single, BinaryOperationAction.Creator(BinaryOperationAction.Op.Div));
                    case JavaInstruction.ddiv:  return BinaryOp<double,double>(curState, true,  (x, y) => x / y,                      typeSystem.Double, BinaryOperationAction.Creator(BinaryOperationAction.Op.Div));
                    case JavaInstruction.irem:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x % y,                      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Rem));
                    case JavaInstruction.lrem:  return BinaryOp<long  ,long  >(curState, true,  (x, y) => x % y,                      typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Rem));
                    case JavaInstruction.frem:  return BinaryOp<float ,float >(curState, false, (x, y) => x % y,                      typeSystem.Single, BinaryOperationAction.Creator(BinaryOperationAction.Op.Rem));
                    case JavaInstruction.drem:  return BinaryOp<double,double>(curState, true,  (x, y) => x % y,                      typeSystem.Double, BinaryOperationAction.Creator(BinaryOperationAction.Op.Rem));
                    case JavaInstruction.ishl:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x << y,                     typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Shl));
                    case JavaInstruction.lshl:  return BinaryOp<long  ,int  >(curState, true,  (x, y) => x << y,               typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Shl), true);
                    case JavaInstruction.ishr:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x >> y,                     typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Shr));
                    case JavaInstruction.lshr:  return BinaryOp<long  ,int  >(curState, true,  (x, y) => x >> y,               typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Shr), true);
                    case JavaInstruction.iushr: return BinaryOp<int   ,int   >(curState, false, (x, y) => (int) ((uint) x >> y),      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Ushr));
                    case JavaInstruction.lushr: return BinaryOp<long  ,int  >(curState, true,  (x, y) => (long) ((ulong) x >> y),     typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Ushr), true);
                    case JavaInstruction.iand:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x & y,                      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.And));
                    case JavaInstruction.land:  return BinaryOp<long  ,long  >(curState, true,  (x, y) => x & y,                      typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.And));
                    case JavaInstruction.ior:   return BinaryOp<int   ,int   >(curState, false, (x, y) => x | y,                      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Or));
                    case JavaInstruction.lor:   return BinaryOp<long  ,long  >(curState, true,  (x, y) => x | y,                      typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Or));
                    case JavaInstruction.ixor:  return BinaryOp<int   ,int   >(curState, false, (x, y) => x ^ y,                      typeSystem.Int32,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Xor));
                    case JavaInstruction.lxor:  return BinaryOp<long  ,long  >(curState, true,  (x, y) => x ^ y,                      typeSystem.Int64,  BinaryOperationAction.Creator(BinaryOperationAction.Op.Xor));

                    case JavaInstruction.ineg: return UnaryOp<int,    int   >(curState, false, false, x => -x, typeSystem.Int32,  UnaryOperationAction.Creator(UnaryOperationAction.Op.Neg));
                    case JavaInstruction.lneg: return UnaryOp<long,   long  >(curState, true,  true,  x => -x, typeSystem.Int64,  UnaryOperationAction.Creator(UnaryOperationAction.Op.Neg));
                    case JavaInstruction.fneg: return UnaryOp<float,  float>(curState, false, false, x => -x, typeSystem.Single, UnaryOperationAction.Creator(UnaryOperationAction.Op.Neg));
                    case JavaInstruction.dneg: return UnaryOp<double, double>(curState, true,  true,  x => -x, typeSystem.Double, UnaryOperationAction.Creator(UnaryOperationAction.Op.Neg));

                    case JavaInstruction.l2i: return UnaryOp<long,   int   >(curState, true,  false, x => (int) x,    typeSystem.Int32,  UnaryOperationAction.Creator(UnaryOperationAction.Op.ToI));
                    case JavaInstruction.f2i: return UnaryOp<float,  int   >(curState, false, false, x => (int) x,    typeSystem.Int32,  UnaryOperationAction.Creator(UnaryOperationAction.Op.ToI));
                    case JavaInstruction.d2i: return UnaryOp<double, int   >(curState, true,  false, x => (int) x,    typeSystem.Int32,  UnaryOperationAction.Creator(UnaryOperationAction.Op.ToI));
                    case JavaInstruction.i2l: return UnaryOp<int,    long  >(curState, false, true,  x => (long) x,   typeSystem.Int64,  UnaryOperationAction.Creator(UnaryOperationAction.Op.ToL));
                    case JavaInstruction.f2l: return UnaryOp<float,  long  >(curState, false, true,  x => (long) x,   typeSystem.Int64,  UnaryOperationAction.Creator(UnaryOperationAction.Op.ToL));
                    case JavaInstruction.d2l: return UnaryOp<double, long  >(curState, true,  true,  x => (long) x,   typeSystem.Int64,  UnaryOperationAction.Creator(UnaryOperationAction.Op.ToL));
                    case JavaInstruction.i2f: return UnaryOp<int,    float>(curState, false, false, x => (float) x,  typeSystem.Single, UnaryOperationAction.Creator(UnaryOperationAction.Op.ToF));
                    case JavaInstruction.l2f: return UnaryOp<long,   float>(curState, true,  false, x => (float) x,  typeSystem.Single, UnaryOperationAction.Creator(UnaryOperationAction.Op.ToF));
                    case JavaInstruction.d2f: return UnaryOp<double, float>(curState, true,  false, x => (float) x,  typeSystem.Single, UnaryOperationAction.Creator(UnaryOperationAction.Op.ToF));
                    case JavaInstruction.i2d: return UnaryOp<int,    double>(curState, false, true,  x => (double) x, typeSystem.Double, UnaryOperationAction.Creator(UnaryOperationAction.Op.ToD));
                    case JavaInstruction.l2d: return UnaryOp<long,   double>(curState, true,  true,  x => (double) x, typeSystem.Double, UnaryOperationAction.Creator(UnaryOperationAction.Op.ToD));
                    case JavaInstruction.f2d: return UnaryOp<float,  double>(curState, false, true,  x => (double) x, typeSystem.Double, UnaryOperationAction.Creator(UnaryOperationAction.Op.ToD));

                    case JavaInstruction.i2b: return UnaryOp<int, int>(curState, false, false,  x => (sbyte) x,  typeSystem.Byte,  UnaryOperationAction.Creator(UnaryOperationAction.Op.ToB));
                    case JavaInstruction.i2c: return UnaryOp<int, int>(curState, false, false,  x => (char) x,  typeSystem.Char,  UnaryOperationAction.Creator(UnaryOperationAction.Op.ToC));
                    case JavaInstruction.i2s: return UnaryOp<int, int>(curState, false, false,  x => (short) x, typeSystem.Int16, UnaryOperationAction.Creator(UnaryOperationAction.Op.ToS));

                    case JavaInstruction.lcmp:
                        return BinaryOp<long, long>(curState, true, null, typeSystem.Int32, BinaryOperationAction.Creator(BinaryOperationAction.Op.Lcmp), false, true);
                    case JavaInstruction.fcmpg:
                        return BinaryOp<float, float>(curState, false, null, typeSystem.Int32, BinaryOperationAction.Creator(BinaryOperationAction.Op.Fcmpg), false, true);
                    case JavaInstruction.fcmpl:
                        return BinaryOp<float, float>(curState, false, null, typeSystem.Int32, BinaryOperationAction.Creator(BinaryOperationAction.Op.Fcmpl), false, true);

                    case JavaInstruction.dcmpg:
                        return BinaryOp<double, double>(curState, true, null, typeSystem.Int32, BinaryOperationAction.Creator(BinaryOperationAction.Op.Fcmpg), false, true);
                    case JavaInstruction.dcmpl:
                        return BinaryOp<double, double>(curState, true, null, typeSystem.Int32, BinaryOperationAction.Creator(BinaryOperationAction.Op.Fcmpl), false, true);

                    case JavaInstruction.ireturn:
                    case JavaInstruction.freturn:
                    case JavaInstruction.areturn:
                        return Return(curState, false);
                    case JavaInstruction.lreturn:
                    case JavaInstruction.dreturn:
                        return Return(curState, true);
                    case JavaInstruction.@return:
                        return Return(curState, false, true);

                    case JavaInstruction.arraylength:
                    {
                        var newState = curState.Pop(out var array);
                        var length = new CalculatedValue(typeSystem.Int32);
                        return (newState.Push(length), new[] {new ArrayLengthAction(length, array)});
                    }

                    case JavaInstruction.athrow:
                    {
                        curState.Pop(out var exc);
                        return (null, new[] {new ThrowAction(exc)});
                    }
                    case JavaInstruction.monitorenter:
                    case JavaInstruction.monitorexit:
                    {
                        return (curState.Pop(out var exc), new[] {new MonitorAction(exc, Instr == JavaInstruction.monitorenter)});
                    }

                    default:
                        throw new NotImplementedException($"{Instr} as Niladic");
                }
            }


            private static (JavaState newState, IEnumerable<MethodAction> actions) BinaryOp<T, T2>(
                JavaState curState,
                bool dubWidth,
                Func<T, T2, T> op,
                TypeReference tr,
                Func<CalculatedValue, JavaValue, JavaValue, MethodAction> mkAction, bool otherOk = false, bool outOk = false)
            {
                curState = curState.PopIf(dubWidth && !otherOk).Pop(out var value2);
                curState = curState.PopIf(dubWidth).Pop(out var value1);

                if (op != null && value1 is ConstantValue cVal1 && value2 is ConstantValue cVal2)
                {
                    curState = curState.Push(new ConstantValue(tr, op((T) cVal1.Value, (T2) cVal2.Value))).PushIf(dubWidth);
                    return (curState, new MethodAction[0]);
                }

                var calcValue = new CalculatedValue(tr);
                curState = curState.Push(calcValue).PushIf(dubWidth && !outOk);
                return (curState, new[] {mkAction(calcValue, value1, value2)});
            }

            private static (JavaState newState, IEnumerable<MethodAction> actions) Return(
                JavaState curState,
                bool dubWidth,
                bool isVoid = false
            )
            {
                if (isVoid)
                    return (null, new[] {new ReturnAction()});

                curState.PopIf(dubWidth).Pop(out var res);

                return (null, new[] {new ReturnAction(res)});
            }

            private static (JavaState newState, IEnumerable<MethodAction> actions) UnaryOp<T, T2>(
                JavaState curState,
                bool dubWidth, 
                bool dubWidthOut,
                Func<T, T2> op,
                TypeReference tr,
                Func<CalculatedValue, JavaValue, MethodAction> mkAction)
            {
                curState = curState.PopIf(dubWidth).Pop(out var value1);
                if (value1 is ConstantValue cVal1)
                {
                    curState = curState.Push(new ConstantValue(tr, op((T) cVal1.Value))).PushIf(dubWidthOut);
                    return (curState, new MethodAction[0]);
                }

                var calcValue = new CalculatedValue(tr);
                curState = curState.Push(calcValue).PushIf(dubWidthOut);
                return (curState, new[] {mkAction(calcValue, value1)});
            }
        }

        public class AType : JavaOp
        {
            public ArrayType Type { get; }

            public AType(int start, JavaInstruction instr, ArrayType arrayType) : base(start, instr)
            {
                Type = arrayType;
            }

            public override string ArgsString { get; }

            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                switch (Instr)
                {
                    case JavaInstruction.newarray:
                        var newState = curState.Pop(out var count);
                        var array = new CalculatedValue(GetTypeRef(Type).MakeArrayType());
                        return (newState.Push(array), new[] {new NewArrayAction(array, GetTypeRef(Type), count)});
                    default:
                        throw new NotImplementedException($"{Instr} as AType");
                }
            }

            private TypeReference GetTypeRef(ArrayType at)
            {
                var ts = JavaAssemblyBuilder.Instance.TypeSystem;
                switch (at)
                {
                    case ArrayType.Boolean: return ts.Boolean;
                    case ArrayType.Char: return ts.Char;
                    case ArrayType.Float: return ts.Single;
                    case ArrayType.Double: return ts.Double;
                    case ArrayType.Byte: return ts.SByte;
                    case ArrayType.Short: return ts.Int16;
                    case ArrayType.Int: return ts.Int32;
                    case ArrayType.Long: return ts.Int64;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(at), at, "Unknown arraytype");
                }
            }
        }

        public class LocalVarIndex : JavaOp
        {
            public int LocalIndex { get; }

            public LocalVarIndex(int start, JavaInstruction instr, int localIndex) : base(start, instr)
            {
                LocalIndex = localIndex;
            }

            public override string ArgsString => $"#{LocalIndex}";

            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                switch (Instr)
                {
                    case JavaInstruction.iload:
                    case JavaInstruction.fload:
                    case JavaInstruction.aload:
                        return Load(curState, LocalIndex, false);
                    case JavaInstruction.lload:
                    case JavaInstruction.dload:
                        return Load(curState, LocalIndex, true);
                    case JavaInstruction.istore:
                    case JavaInstruction.fstore:
                    case JavaInstruction.astore:
                        return Store(curState, LocalIndex, false);
                    case JavaInstruction.lstore:
                    case JavaInstruction.dstore:
                        return Store(curState, LocalIndex, true);
                    default:
                        throw new NotImplementedException($"{Instr} as LocalVarIndex");
                }
            }

            private (JavaState newState, IEnumerable<MethodAction> actions) Load(JavaState curState, int index, bool dblWidth)
            {
                var newState = curState.Push(curState.Load(index)).PushIf(dblWidth);
                return (newState, new MethodAction[0]);
            }
            private (JavaState newState, IEnumerable<MethodAction> actions) Store(JavaState curState, int index, bool dblWidth)
            {
                var newState = curState;
                if (dblWidth)
                    newState = newState.Pop();
                newState = newState.Pop(out var value);
                if (newState.TryLoad(index) is ArgumentValue av)
                {
                    av.NeedsBacking();
                }
                newState = newState.Store(index, value);
                return (newState, new MethodAction[0]);
            }
        }

        public class Numeric : JavaOp
        {
            public int Value { get; }

            public Numeric(int start, JavaInstruction instr, int value) : base(start, instr)
            {
                Value = value;
            }

            public override string ArgsString => $"{Value}";

            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                var ts = JavaAssemblyBuilder.Instance.TypeSystem;
                switch (Instr)
                {
                    case JavaInstruction.bipush:
                        return (curState.Push(new ConstantValue(ts.SByte, Value)), new MethodAction[0]);
                    case JavaInstruction.sipush:
                        return (curState.Push(new ConstantValue(ts.Int16, Value)), new MethodAction[0]);
                    default:
                        throw new NotImplementedException($"{Instr} as Numeric");
                }
            }
        }

        public class Branch : JavaOp
        {
            public int JumpTarget { get; }

            public Branch(int start, JavaInstruction instr, int jumpTarget) : base(start, instr)
            {
                JumpTarget = jumpTarget;
            }

            public override string ArgsString => $"0x{JumpTarget:X4}";
            public override IEnumerable<int> JumpTargets => new[] {JumpTarget};
            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                switch (Instr)
                {
                    case JavaInstruction.ifnull:
                    case JavaInstruction.ifeq: return CondJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Eq);
                    case JavaInstruction.ifnonnull:
                    case JavaInstruction.ifne: return CondJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Ne);
                    case JavaInstruction.iflt: return CondJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Lt);
                    case JavaInstruction.ifle: return CondJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Le);
                    case JavaInstruction.ifgt: return CondJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Gt);
                    case JavaInstruction.ifge: return CondJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Ge);

                    case JavaInstruction.if_acmpeq:
                    case JavaInstruction.if_icmpeq: return CmpJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Eq);
                    case JavaInstruction.if_acmpne:
                    case JavaInstruction.if_icmpne: return CmpJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Ne);
                    case JavaInstruction.if_icmplt: return CmpJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Lt);
                    case JavaInstruction.if_icmple: return CmpJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Le);
                    case JavaInstruction.if_icmpgt: return CmpJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Gt);
                    case JavaInstruction.if_icmpge: return CmpJump(curState, blocks[JumpTarget], ConditionalJumpAction.JumpCondition.Ge);

                    case JavaInstruction.@goto:
                    case JavaInstruction.goto_w:
                        return (null, new[] {new UnconditionalJump(blocks[JumpTarget])});

                    default:
                        throw new NotImplementedException($"{Instr} as Branch");

                }
            }

            private (JavaState newState, IEnumerable<MethodAction> actions) CondJump(JavaState state, ActionBlock target, ConditionalJumpAction.JumpCondition condition)
            {
                var newState = state.Pop(out var value);
                return (newState, new[] {new ConditionalJumpAction(value, target, condition)});
            }
            private (JavaState newState, IEnumerable<MethodAction> actions) CmpJump(JavaState state, ActionBlock target, ConditionalJumpAction.JumpCondition condition)
            {
                var newState = state.Pop(out var value1).Pop(out var value2);
                return (newState, new[] { new ConditionalJumpAction(value1, target, condition, value2) });
            }



        }

        public class Iinc : JavaOp
        {
            public int Index { get; }
            public int Diff { get; }

            public Iinc(int start, JavaInstruction instr, int index, int diff) : base(start, instr)
            {
                Index = index;
                Diff = diff;
            }

            public override string ArgsString => $"#{Index}, {Diff}";

            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                var local = curState.Load(Index);
                var newValue = new CalculatedValue(local.ActualType);
                var increment = new ConstantValue(local.ActualType, Diff);
                return (curState.Store(Index, newValue), new[] {new BinaryOperationAction(newValue, BinaryOperationAction.Op.Add, local, increment)});
            }
        }


        public class ConstPool : JavaOp
        {
            public CpInfo CpInfo { get; }

            public ConstPool(int start, JavaInstruction instr, CpInfo cpInfo) : base(start, instr)
            {
                CpInfo = cpInfo;
            }

            public override string ArgsString => CpInfo.Represent();

            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                var asm = JavaAssemblyBuilder.Instance;
                var noAction = new MethodAction[0];
                switch (Instr)
                {
                    case JavaInstruction.ldc:
                    case JavaInstruction.ldc_w:
                        switch (CpInfo)
                        {
                            case StringInfo info: return (curState.Push(new ConstantValue(asm.TypeSystem.String, info.String)), noAction);
                            case IntegerInfo info: return (curState.Push(new ConstantValue(asm.TypeSystem.Int32, info.Value)), noAction);
                            case FloatInfo info: return (curState.Push(new ConstantValue(asm.TypeSystem.Single, info.Value)), noAction);
                            case ClassInfo info: return (curState.Push(new ConstantValue(asm.Import(typeof(RuntimeTypeHandle)), asm.ResolveTypeReference(info.Name))), noAction);
                            default:
                                throw new NotImplementedException($"Can't process {CpInfo.Tag}Info for ldc");
                        }

                    case JavaInstruction.ldc2_w:
                        switch (CpInfo)
                        {
                            case LongInfo info: return (curState.Push(new ConstantValue(asm.TypeSystem.Int64, info.Value)).Push(null), noAction);
                            case DoubleInfo info: return (curState.Push(new ConstantValue(asm.TypeSystem.Double, info.Value)).Push(null), noAction);
                            default:
                                throw new NotImplementedException($"Can't process {CpInfo.Tag}Info for ldc2");
                        }
                    case JavaInstruction.getstatic:
                    {
                        var cp = (FieldOrMethodrefInfo) CpInfo;
                        var fld = asm.ResolveFieldReference(cp);
                        var result = new CalculatedValue(JavaAssemblyBuilder.Instance.Import(fld.FieldType));
                        var isDouble = cp.NameAndType.Descriptor == "J" || cp.NameAndType.Descriptor == "D";
                            return (curState.Push(result).PushIf(isDouble), new[] {new GetStaticAction(result, fld)});
                    }
                    case JavaInstruction.putstatic:
                    {
                        var cp = (FieldOrMethodrefInfo) CpInfo;
                        var fld = asm.ResolveFieldReference(cp);
                        var isDouble = cp.NameAndType.Descriptor == "J" || cp.NameAndType.Descriptor == "D";
                        return (curState.PopIf(isDouble).Pop(out var value), new[] {new SetStaticAction(value, fld)});
                    }
                    case JavaInstruction.getfield:
                    {
                        var cp = (FieldOrMethodrefInfo)CpInfo;
                        var fld = asm.ResolveFieldReference(cp);
                        var target = new CalculatedValue(JavaAssemblyBuilder.Instance.Import(fld.FieldType));
                        var isDouble = cp.NameAndType.Descriptor == "J" || cp.NameAndType.Descriptor == "D";
                        return (curState.Pop(out var from).Push(target).PushIf(isDouble), new[] {new GetFieldAction(target, from, fld)});
                    }
                    case JavaInstruction.putfield:
                    {
                        var cp = (FieldOrMethodrefInfo) CpInfo;
                        var fld = asm.ResolveFieldReference(cp);
                        var isDouble = cp.NameAndType.Descriptor == "J" || cp.NameAndType.Descriptor == "D";
                        return (curState.PopIf(isDouble).Pop(out var value).Pop(out var target), new[] {new SetFieldAction(target, value, fld)});
                    }
                    case JavaInstruction.invokevirtual:
                    case JavaInstruction.invokespecial:
                    case JavaInstruction.invokestatic:
                    case JavaInstruction.invokeinterface:
                    {
                        var cp = (FieldOrMethodrefInfo) CpInfo;
                        var isStatic = Instr == JavaInstruction.invokestatic;
                        var method =
                            asm.ResolveMethodReference(isStatic, cp, Instr == JavaInstruction.invokeinterface)
                            ?? CreateLateBoundMethodReference(cp, asm, isStatic);

                        var args = new JavaValue[method.Parameters.Count];
                        var state = curState;
                        for (var i = args.Length - 1; i >= 0; i--)
                        {
                            state = state.Pop(out args[i]);
                            if (args[i] == null)
                            {
                                state = state.Pop(out args[i]);
                                Debug.Assert(args[i].ActualType.Name.StartsWith("Int64")
                                             || args[i].ActualType.Name.StartsWith("Double"));
                            }
                        }

                        JavaValue objRef = null;
                        if (!method.Resolve()?.IsStatic ?? !isStatic)
                        {
                            state = state.Pop(out objRef);
                        }

                        CalculatedValue result = null;
                        if (method.ReturnType.FullName != typeof(void).FullName)
                        {
                            result = new CalculatedValue(method.ReturnType);
                            state = state.Push(result);
                            var lc = cp.NameAndType.Descriptor.LastIndexOf(')');
                            var retTyp = cp.NameAndType.Descriptor.Substring(lc + 1);
                            if (retTyp == "J" || retTyp == "D")
                                state = state.Push(null);
                        }

                        if (cp.NameAndType.Name == "<init>" && objRef is CalculatedValue cv)
                        {
                            //var cv = (CalculatedValue) objRef;
                            // this is a constructor call

                            if (objRef is ArgumentValue av)
                            {
                                // this is a BASE constructor call
                                // just handle as if it's a normal call

                                Debug.Assert(av.Param.Index == -1);

                            }
                            else if (method.Name == ".ctor")
                                return (state, new[] {new ConstructorAction(cv, method, args)});
                            else
                            {
                                // this is a plugged constructor call
                                Debug.Assert(!method.HasThis);
                                return (state, new[] {new InvokeAction(cv, null, method, args)});
                            }
                        }

                        return (state, new[] {new InvokeAction(result, objRef, method, args, Instr == JavaInstruction.invokevirtual || Instr == JavaInstruction.invokeinterface)});
                    }
                    case JavaInstruction.@new:
                    {
                        var cp = (ClassInfo)CpInfo;
                        var calc = new CalculatedValue(asm.ResolveTypeReference(cp.Name));
                        return (curState.Push(calc), noAction); // new is kind of dumb in JVM
                    }
                    case JavaInstruction.anewarray:
                    {
                        var state = curState.Pop(out var count);
                        var cp = (ClassInfo) CpInfo;
                        var typeRef = asm.ResolveTypeReference(cp.Name);
                        var result = new CalculatedValue(typeRef.MakeArrayType());
                        return (state.Push(result), new[] {new NewArrayAction(result, typeRef, count)});
                    }
                    case JavaInstruction.checkcast:
                    {
                        var state = curState.Pop(out var count);
                        var cp = (ClassInfo)CpInfo;
                        var typeRef = asm.ResolveTypeReference(cp.Name);
                        var result = new CalculatedValue(typeRef);
                        return (state.Push(result), new[] { new CastAction(result, typeRef, count)});
                    }
                    case JavaInstruction.instanceof:
                    {
                        var state = curState.Pop(out var count);
                        var cp = (ClassInfo)CpInfo;
                        var typeRef = asm.ResolveTypeReference(cp.Name);
                        var result = new CalculatedValue(asm.TypeSystem.Boolean);
                        return (state.Push(result), new[] { new InstanceOfAction(result, typeRef, count) });
                        }
                    default:
                        throw new NotImplementedException($"{Instr} as CP");
                }
            }

            private MethodReference CreateLateBoundMethodReference(FieldOrMethodrefInfo cp, JavaAssemblyBuilder asm, bool isStatic)
            {
                var (retType, paramTypes) = asm.ResolveMethodDescriptor(cp.NameAndType.Descriptor);
                var mr = new MethodReference(asm.TranslateMethodName(cp.NameAndType.Name), retType, asm.ResolveTypeReference(cp.Class.Name));
                foreach (var paramType in paramTypes)
                {
                    mr.Parameters.Add(new ParameterDefinition(paramType));
                }

                mr.HasThis = !isStatic;

                return mr;
            }
        }

        public class MultiNewArray : JavaOp
        {
            public ClassInfo Type { get; }
            public int NumDims { get; }

            public MultiNewArray(int start, JavaInstruction instr, CpInfo type, byte numDims) : base(start, instr)
            {
                Type = (ClassInfo) type;
                NumDims = numDims;
            }

            public override string ArgsString => $"{Type.Represent()} ** {NumDims}";

            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                var dims = new JavaValue[NumDims];
                var state = curState;
                for (var i = dims.Length - 1; i >= 0; i--)
                {
                    state = state.Pop(out dims[i]);
                }

                var typeRef = JavaAssemblyBuilder.Instance.ResolveTypeReference(Type.Name);
                var calc = new CalculatedValue(typeRef);
                return (state.Push(calc), new[] {new MultiNewArrayAction(calc, typeRef, dims)});
            }
        }

        public class LookupSwitch : JavaOp
        {
            public int DefOffset { get; }
            public KeyValuePair<int, int>[] Table { get; }

            public LookupSwitch(int start, JavaInstruction instr, int defOffset, KeyValuePair<int, int>[] table) : base(start, instr)
            {
                DefOffset = defOffset;
                Table = table;
             }

            public override string ArgsString { get; }
            public override IEnumerable<int> JumpTargets => Table.Select(x => x.Value);
            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                curState = curState.Pop(out var value);
                return (curState, new[] {new LookupSwitchAction(value, blocks[DefOffset], Table.Select(x => KeyValuePair.Create(x.Key, blocks[x.Value])).ToArray())});
            }
        }

        public class TableSwitch : JavaOp
        {
            public int DefOffset { get; }
            public int Low { get; }
            public int High { get; }
            public int[] Targets { get; }

            public TableSwitch(int start, JavaInstruction instr, int defOffset, int low, int high, int[] targets) : base(start, instr)
            {
                DefOffset = defOffset;
                Low = low;
                High = high;
                Targets = targets;
            }

            public override string ArgsString { get; }
            public override IEnumerable<int> JumpTargets => Targets;
            internal override (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState,
                Dictionary<int, ActionBlock> blocks)
            {
                curState = curState.Pop(out var value);
                return (curState, new[] {new TableSwitchAction(value, blocks[DefOffset], Low, High, Targets.Select(x => blocks[x]).ToArray())});
            }
        }

        internal abstract (JavaState newState, IEnumerable<MethodAction> actions) ActUpon(JavaState curState, Dictionary<int, ActionBlock> blocks);
    }

    public enum ArrayType
    {
        Boolean = 4,
        Char = 5,
        Float = 6,
        Double = 7,
        Byte = 8,
        Short = 9,
        Int = 10,
        Long = 11,
    }
}