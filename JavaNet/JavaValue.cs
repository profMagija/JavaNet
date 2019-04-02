﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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

        public override bool IsConst => true;
        public override Instruction[] GetValue()
        {
            switch (Value)
            {
                case null: return new[] {Instruction.Create(OpCodes.Ldnull)};
                case string s: return new[] {Instruction.Create(OpCodes.Ldstr, s)};
                case -1: return new[] {Instruction.Create(OpCodes.Ldc_I4_M1)};
                case 0: return new[] {Instruction.Create(OpCodes.Ldc_I4_0)};
                case 1: return new[] {Instruction.Create(OpCodes.Ldc_I4_1)};
                case 2: return new[] {Instruction.Create(OpCodes.Ldc_I4_2)};
                case 3: return new[] {Instruction.Create(OpCodes.Ldc_I4_3)};
                case 4: return new[] {Instruction.Create(OpCodes.Ldc_I4_4)};
                case 5: return new[] {Instruction.Create(OpCodes.Ldc_I4_5)};
                case 6: return new[] {Instruction.Create(OpCodes.Ldc_I4_6)};
                case 7: return new[] {Instruction.Create(OpCodes.Ldc_I4_7)};
                case 8: return new[] {Instruction.Create(OpCodes.Ldc_I4_8)};
                case int i when i >= -128 && i < 128:
                    return new[] {Instruction.Create(OpCodes.Ldc_I4_S, (sbyte) i)};
                case int i: return new[] {Instruction.Create(OpCodes.Ldc_I4, i)};
                case long l: return new[] {Instruction.Create(OpCodes.Ldc_I8, l)};
                case float f: return new[] {Instruction.Create(OpCodes.Ldc_R4, f)};
                case double d: return new[] {Instruction.Create(OpCodes.Ldc_R8, d)};
                case TypeReference tr:
                    return new[] {Instruction.Create(OpCodes.Ldtoken, tr)};
                case MethodReference mr:
                    return new[] {Instruction.Create(OpCodes.Ldtoken, mr)};
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
        public VariableDefinition VarDef { get; set; }
        public override Instruction[] GetValue()
        {
            if (VarDef == null)
                throw new Exception("Uninitialized storage location for " + this);

            return new[] {Instruction.Create(OpCodes.Ldloc, VarDef)};
        }

        public Instruction[] StoreValue()
        {
            if (VarDef == null)
                return new[] {Instruction.Create(OpCodes.Pop)};

            return new[] {Instruction.Create(OpCodes.Stloc, VarDef)};
        }
    }

    class ArgumentValue : JavaValue
    {
        public ArgumentValue(TypeReference actualType, ParameterDefinition param) : base(actualType)
        {
            Param = param;
        }

        public ParameterDefinition Param { get; }

        public override string ToString() => $"arg{Param.Index}";

        public override Instruction[] GetValue()
        {
            Debug.Assert(Param != null);
            return new[] {Instruction.Create(OpCodes.Ldarg, Param)};
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
