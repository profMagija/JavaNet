using System.ComponentModel;
using System.Security.Cryptography;

namespace JavaNet
{
    public enum ConstantPoolTag : byte
    {
        Class = 7,
        Fieldref = 9,
        Methodref = 10,
        InterfaceMethodref = 11,
        String = 8,
        Integer = 3,
        Float = 4,
        Long = 5,
        Double = 6,
        NameAndType = 12,
        Utf8 = 1,
        MethodHandle = 15,
        MethodType = 16,
        InvokeDynamic = 18,
        Module = 19,
        Package = 20
    }

    public abstract class CpInfo
    {
        public ConstantPoolTag Tag;

        protected CpInfo(ConstantPoolTag tag)
        {
            Tag = tag;
        }

        public virtual void Fill(CpInfo[] cp)
        {
        }

        public abstract string Represent();
    }

    public class ClassInfo : CpInfo
    {
        private readonly ushort _nameIndex;

        public string Name;

        public ClassInfo(ConstantPoolTag tag, ushort nameIndex) : base(tag)
        {
            _nameIndex = nameIndex;
        }

        public override string ToString() => $"ClassInfo [NameIndex={_nameIndex}]";
        
        public override void Fill(CpInfo[] cp)
        {
            Name = ((Utf8Info) cp[_nameIndex]).Data;
        }

        public override string Represent() => Name.Replace('/', '.').Replace("]", "[]");
    }

    public class FieldOrMethodrefInfo : CpInfo
    {
        private readonly ushort _classIndex;
        private readonly ushort _nameAndTypeIndex;

        public ClassInfo Class;
        public NameAndTypeInfo NameAndType;

        public FieldOrMethodrefInfo(ConstantPoolTag tag, ushort classIndex, ushort nameAndTypeIndex) : base(tag)
        {
            _classIndex = classIndex;
            _nameAndTypeIndex = nameAndTypeIndex;
        }

        public override string ToString() => $"{Tag}Info [ClassIndex={_classIndex} NameAndTypeIndex={_nameAndTypeIndex}]";
        
        public override void Fill(CpInfo[] cp)
        {
            Class = (ClassInfo) cp[_classIndex];
            NameAndType = (NameAndTypeInfo) cp[_nameAndTypeIndex];
        }

        public override string Represent() => $"{Class.Represent()}.{NameAndType.Name}:{NameAndType.Descriptor}";
    }

    public class StringInfo : CpInfo
    {
        private readonly ushort _stringIndex;
        public string String;

        public StringInfo(ConstantPoolTag tag, ushort stringIndex) : base(tag)
        {
            _stringIndex = stringIndex;
        }

        public override string ToString() => $"StringInfo [StringIndex={_stringIndex}]";

        public override void Fill(CpInfo[] cp)
        {
            String = ((Utf8Info) cp[_stringIndex]).Data;
        }

        public override string Represent() => String
            .Replace("\\", "\\\\")
            .Replace("\n", "\\n")
            .Replace("\"", "\\\"")
            .Replace("\a", "\\a")
            .Replace("\b", "\\b")
            .Replace("\f", "\\f")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            .Replace("\t", "\\t")
            .Replace("\v", "\\v")
            .Replace("\0", "\\0");

    }

    public class IntegerInfo : CpInfo
    {
        public int Value;

        public IntegerInfo(ConstantPoolTag tag, int value) : base(tag)
        {
            Value = value;
        }

        public override string ToString() => $"IntegerInfo [Value={Value}]";
        public override string Represent() => Value.ToString();
    }

    public class FloatInfo : CpInfo
    {
        public float Value;

        public FloatInfo(ConstantPoolTag tag, float value) : base(tag)
        {
            Value = value;
        }

        public override string ToString() => $"FloatInfo [Value={Value}]";
        public override string Represent() => Value.ToString();
    }

    public class LongInfo : CpInfo
    {
        public long Value;

        public LongInfo(ConstantPoolTag tag, long value) : base(tag)
        {
            Value = value;
        }

        public override string ToString() => $"LongInfo [Value={Value}]";
        public override string Represent() => Value.ToString();
    }

    public class DoubleInfo : CpInfo
    {
        public double Value;

        public DoubleInfo(ConstantPoolTag tag, double value) : base(tag)
        {
            Value = value;
        }


        public override string ToString() => $"DoubleInfo [Value={Value}]";
        public override string Represent() => Value.ToString();
    }

    public class NameAndTypeInfo : CpInfo
    {
        private readonly ushort _nameIndex;
        private readonly ushort _descriptorIndex;

        public string Name;
        public string Descriptor;

        public NameAndTypeInfo(ConstantPoolTag tag, ushort nameIndex, ushort descriptorIndex) : base(tag)
        {
            _nameIndex = nameIndex;
            _descriptorIndex = descriptorIndex;
        }

        public override string ToString() =>
            $"NameAndTypeInfo [NameIndex={_nameIndex} DescriptorIndex={_descriptorIndex}]";

        public override void Fill(CpInfo[] cp)
        {
            Name = ((Utf8Info) cp[_nameIndex]).Data;
            Descriptor = ((Utf8Info) cp[_descriptorIndex]).Data;
        }

        public override string Represent() => $"{Name}:{Descriptor}";
    }

    public class Utf8Info : CpInfo
    {
        public string Data;

        public Utf8Info(ConstantPoolTag tag, ushort length, string data) : base(tag)
        {
            Data = data;
        }

        public override string ToString() => $"Utf8Info '{Data}'";
        public override string Represent() => $"'{Data}'";
    }

    public class MethodHandleInfo : CpInfo
    {
        public readonly MethodHandleType ReferenceKind;
        private readonly ushort _referenceIndex;
        public FieldOrMethodrefInfo Reference;

        public MethodHandleInfo(ConstantPoolTag tag, MethodHandleType referenceKind, ushort referenceIndex) : base(tag)
        {
            ReferenceKind = referenceKind;
            _referenceIndex = referenceIndex;
        }

        public override string ToString() =>
            $"MethodHandleInfo [ReferenceKind={ReferenceKind} ReferenceIndex={_referenceIndex}]";

        public override void Fill(CpInfo[] cp)
        {
            Reference = (FieldOrMethodrefInfo) cp[_referenceIndex];
        }

        public override string Represent() => $"{ReferenceKind.ToString().ToLower()} {Reference}";
    }

    public enum MethodHandleType : byte
    {
        GetField = 1, // getfield C.f:T
        GetStatic = 2, // getstatic C.f:T
        PutField = 3, // putfield C.f:T
        PutStatic = 4, // putstatic C.f:T
        InvokeVirtual = 5, // invokevirtual C.m:(A*)T
        InvokeStatic = 6, // invokestatic C.m:(A*)T
        InvokeSpecial = 7, // invokespecial C.m:(A*)T
        NewInvokeSpecial = 8, // new C; dup; invokespecial C.<init>:(A*)void
        InvokeInterface = 9 // invokeinterface C.m:(A*)T
    }

    public class MethodTypeInfo : CpInfo
    {
        private readonly ushort _descriptorIndex;

        public string Descriptor;

        public MethodTypeInfo(ConstantPoolTag tag, ushort descriptorIndex) : base(tag)
        {
            _descriptorIndex = descriptorIndex;
        }

        public override string ToString() =>
            $"MethodTypeInfo [DescriptorIndex={_descriptorIndex}]";

        public override void Fill(CpInfo[] cp)
        {
            Descriptor = ((Utf8Info) cp[_descriptorIndex]).Data;
        }

        public override string Represent() => Descriptor;
    }

    public class InvokeDynamicInfo : CpInfo
    {
        private readonly ushort _nameAndTypeIndex;

        public readonly ushort BootstrapMethodAttrIndex;
        public NameAndTypeInfo NameAndType;

        public InvokeDynamicInfo(ConstantPoolTag tag, ushort bootstrapMethodAttrIndex, ushort nameAndTypeIndex) :
            base(tag)
        {
            BootstrapMethodAttrIndex = bootstrapMethodAttrIndex;
            _nameAndTypeIndex = nameAndTypeIndex;
        }

        public override string ToString() =>
            $"InvokeDynamicInfo [BootstrapMethodAttrIndex={BootstrapMethodAttrIndex} NameAndTypeIndex={_nameAndTypeIndex}]";

        public override void Fill(CpInfo[] cp)
        {
            NameAndType = (NameAndTypeInfo) cp[_nameAndTypeIndex];
        }

        public override string Represent() => $"bootstrap[{BootstrapMethodAttrIndex}] {NameAndType}";
    }

    public class ModuleOrPackageInfo : CpInfo
    {
        private readonly ushort _nameIndex;

        public string Name;

        public ModuleOrPackageInfo(ConstantPoolTag tag, ushort nameIndex) : base(tag)
        {
            _nameIndex = nameIndex;
        }

        public override string ToString() => $"{Tag}Info [Name={Name}]";

        public override void Fill(CpInfo[] cp)
        {
            Name = ((Utf8Info) cp[_nameIndex]).Data;
        }

        public override string Represent() => $"{Tag.ToString().ToLower()} {Name}";
    }
}