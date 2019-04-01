using System.Reflection;
using System.Reflection.Emit;

namespace JavaNet
{
    public abstract class JavaAttributeInfo
    {
        public string Name { get; }
        public uint Length { get; }

        protected JavaAttributeInfo(string name, uint length)
        {
            Name = name;
            Length = length;
        }
    }

    public static class AttributeName
    {
        public const string ConstantValue = "ConstantValue";
        public const string Code = "Code";
        public const string StackMapTable = "StackMapTable";
        public const string Exceptions = "Exceptions";
        public const string InnerClasses = "InnerClasses";
        public const string EnclosingMethod = "EnclosingMethod";
        public const string Synthetic = "Synthetic";
        public const string Signature = "Signature";
        public const string SourceFile = "SourceFile";
        public const string SourceDebugExtension = "SourceDebugExtension";
        public const string LineNumberTable = "LineNumberTable";
        public const string LocalVariableTable = "LocalVariableTable";
        public const string LocalVariableTypeTable = "LocalVariableTypeTable";
        public const string Deprecated = "Deprecated";
        public const string RuntimeVisibleAnnotations = "RuntimeVisibleAnnotations";
        public const string RuntimeInvisibleAnnotations = "RuntimeInvisibleAnnotations";
        public const string RuntimeVisibleParameterAnnotations = "RuntimeVisibleParameterAnnotations";
        public const string RuntimeInvisibleParameterAnnotations = "RuntimeInvisibleParameterAnnotations";
        public const string AnnotationDefault = "AnnotationDefault";
        public const string BootstrapMethods = "BootstrapMethods";
    }

    public class ConstantValueAttribute : JavaAttributeInfo
    {
        public CpInfo Value { get; }

        public ConstantValueAttribute(string name, uint length, CpInfo constantValue) : base(
            name, length)
        {
            Value = constantValue;
        }
    }

    public class CodeAttribute : JavaAttributeInfo
    {
        public int MaxStack { get; }
        public int MaxLocals { get; }
        public byte[] Code { get; }
        public ExceptionTableEntry[] ExceptionTable { get; }
        public JavaAttributeInfo[] Attributes { get; }

        public CodeAttribute(string name, uint length, int maxStack, int maxLocals, byte[] code,
            ExceptionTableEntry[] exceptionTable, JavaAttributeInfo[] attributes)
            : base(name, length)
        {
            MaxStack = maxStack;
            MaxLocals = maxLocals;
            Code = code;
            ExceptionTable = exceptionTable;
            Attributes = attributes;
        }
    }

    public class ExceptionTableEntry
    {
        public int StartPc { get; }
        public int EndPc { get; }
        public int HandlerPc { get; }
        public ClassInfo CatchType { get; }

        public ExceptionTableEntry(int startPc, int endPc, int handlerPc, ClassInfo catchType)
        {
            StartPc = startPc;
            EndPc = endPc;
            HandlerPc = handlerPc;
            CatchType = catchType;
        }
    }

    public class ExceptionsAttribute : JavaAttributeInfo
    {
        public ClassInfo[] ExceptionIndexTable { get; }

        public ExceptionsAttribute(string name, uint length, ClassInfo[] exceptionIndexTable) : base(name, length)
        {
            ExceptionIndexTable = exceptionIndexTable;
        }
    }

    public class InnerClassesAttribute : JavaAttributeInfo
    {
        public InnerClassesEntry[] Classes { get; }

        public InnerClassesAttribute(string name, uint length, InnerClassesEntry[] classes) : base(name, length)
        {
            Classes = classes;
        }
    }

    public class InnerClassesEntry
    {
        public ClassInfo InnerClass { get; }
        public ClassInfo OuterClass { get; }
        public Utf8Info InnerClassName { get; }
        public int InnerClassAccessFlags { get; }

        public InnerClassesEntry(ClassInfo innerClass, ClassInfo outerClass, Utf8Info innerClassName, int innerClassAccessFlags)
        {
            InnerClass = innerClass;
            OuterClass = outerClass;
            InnerClassName = innerClassName;
            InnerClassAccessFlags = innerClassAccessFlags;
        }
    }

    public class EnclosingMethodAttribute : JavaAttributeInfo
    {
        public ClassInfo Class { get; }
        public NameAndTypeInfo Method { get; }
        
        public EnclosingMethodAttribute(string name, uint length, ClassInfo @class, NameAndTypeInfo method) : base(name, length)
        {
            Class = @class;
            Method = method;
        }
    }

    public class SyntheticAttribute : JavaAttributeInfo
    {
        public SyntheticAttribute(string name, uint length) : base(name, length)
        {
        }
    }

    public class UnknownAttribute : JavaAttributeInfo
    {
        public byte[] Data;
        public UnknownAttribute(string name, uint length, byte[] data) : base(name, length)
        {
            Data = data;
        }
    }
}