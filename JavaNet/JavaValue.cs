using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using JavaNet.Runtime.Plugs;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace JavaNet
{
    abstract class JavaValue
    {
        public TypeReference ActualType { get; }

        protected JavaValue(TypeReference actualType)
        {
            ActualType = actualType;
        }

        //public static JavaStackType ToStackType(TypeReference actualType)
        //{
        //    var ts = JavaAssemblyBuilder.Instance.TypeSystem;
        //    if (actualType.IsByReference)
        //        return JavaStackType.Reference;

        //    if (actualType == ts.Byte
        //        || actualType == ts.SByte
        //        || actualType == ts.Int16
        //        || actualType == ts.UInt16
        //        || actualType == ts.Int32
        //        || actualType == ts.UInt32)
        //    {
        //        return JavaStackType.Int;
        //    }

        //    if (actualType == ts.Int64
        //        || actualType == ts.UInt64
        //        || actualType == ts.IntPtr
        //        || actualType == ts.UIntPtr)
        //        return JavaStackType.Long;

        //    if (actualType == ts.Single)
        //        return JavaStackType.Float;

        //    if (actualType == ts.Double)
        //        return JavaStackType.Double;

        //    throw new NotImplementedException($"at = {actualType.FullName}");
        //}

        public virtual bool IsConst => false;

        public abstract Instruction[] GetValue();
    }

    internal enum JavaStackType
    {
        Int, Long, Float, Double, Reference
    }

    class ConstantValue : JavaValue
    {
        public object Value { get; }

        public ConstantValue(TypeReference actualType, object value) : base(actualType)
        {
            Value = value;
            Debug.Assert(GetValue() != null);
        }

        public override string ToString() => Value?.ToString() ?? "null";

        private static readonly MethodInfo _getClass = typeof(Intrinsics).GetMethod("GetClassFromHandle");
        //private static readonly MethodInfo _getMethod = typeof(Intrinsics).GetMethod("GetMethodFromHandle");
        //private static readonly MethodInfo _strToCharArray = typeof(string).GetMethod("ToCharArray", new Type[0]);

        public override bool IsConst => true;
        public override Instruction[] GetValue()
        {
            switch (Value)
            {
                case null: return new[] {Instruction.Create(OpCodes.Ldnull)};
                case string s:
                    return new[]
                    {
                        Instruction.Create(OpCodes.Ldstr, s),
                    };
                case int i: return new[] {Instruction.Create(OpCodes.Ldc_I4, i)};
                case long l: return new[] {Instruction.Create(OpCodes.Ldc_I8, l)};
                case float f: return new[] {Instruction.Create(OpCodes.Ldc_R4, f)};
                case double d: return new[] {Instruction.Create(OpCodes.Ldc_R8, d)};
                case TypeReference tr:
                    return new[]
                    {
                        Instruction.Create(OpCodes.Ldtoken, tr),
                        Instruction.Create(OpCodes.Call, JavaAssemblyBuilder.Instance.Import(_getClass)),
                    };
                default:
                    throw new Exception("Invalid constant " + Value);
            }
        }
    }

    class CalculatedValue : JavaValue
    {
        public CalculatedValue(TypeReference actualType) : base(actualType)
        {
        }

        public override string ToString() => $"local_{VarDef?.Index ?? GetHashCode():X}";
        public virtual VariableDefinition VarDef { get; set; }
        public override Instruction[] GetValue()
        {
            if (VarDef == null)
                throw new Exception("Uninitialized storage location for " + this);

            return new[] {Instruction.Create(OpCodes.Ldloc, VarDef)};
        }

        public virtual Instruction[] StoreValue()
        {
            if (VarDef == null)
                return new[] {Instruction.Create(OpCodes.Pop)};

            return new[] {Instruction.Create(OpCodes.Stloc, VarDef)};
        }
    }

    class ArgumentValue : CalculatedValue
    {
        public ArgumentValue(TypeReference actualType, ParameterDefinition param) : base(actualType)
        {
            Param = param;
        }

        public ParameterDefinition Param { get; }

        public CalculatedValue Backing { get; private set; }

        public override VariableDefinition VarDef
        {
            get => Backing.VarDef;
            set => Backing.VarDef = value;
        }

        public override string ToString() => $"arg{Param.Index}";

        public override Instruction[] GetValue()
        {
            Debug.Assert(Param != null);
            return Backing?.GetValue() ?? new[] {Instruction.Create(OpCodes.Ldarg, Param)};
        }

        public override Instruction[] StoreValue()
        {
            Debug.Assert(Backing != null);
            return Backing.StoreValue();
        }

        public void NeedsBacking()
        {
            Backing = Backing ?? new CalculatedValue(ActualType);
        }
    }

    //class InputStackValue : JavaValue
    //{
    //    public InputStackValue(TypeReference actualType, int stackPosition) : base(actualType)
    //    {
    //        StackPosition = stackPosition;
    //    }

    //    public int StackPosition { get; }
    //    public override void GetValue(ILProcessor proc)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //class InputLocalValue : JavaValue
    //{
    //    public InputLocalValue(TypeReference actualType, int localIndex) : base(actualType)
    //    {
    //        LocalIndex = localIndex;
    //    }

    //    public int LocalIndex { get; }
    //    public override void GetValue(ILProcessor proc)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
