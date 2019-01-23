using System.Reflection;
using System.Reflection.Emit;

namespace JavaNet
{
    public abstract class JavaAttributeInfo
    {
        public readonly string AttributeName;
        public readonly uint AttributeLength;

        protected JavaAttributeInfo(string attributeName, uint attributeLength)
        {
            AttributeName = attributeName;
            AttributeLength = attributeLength;
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
        public readonly CpInfo ConstantValue;

        public ConstantValueAttribute(string attributeName, uint attributeLength, CpInfo constantValue) : base(
            attributeName, attributeLength)
        {
            ConstantValue = constantValue;
        }
        
        
    }

    public class CodeAttribute : JavaAttributeInfo
    {
        public readonly int MaxStack;
        public readonly int MaxLocals;
        public readonly byte[] Code;
        public readonly ExceptionTableEntry[] ExceptionTable;
        public readonly JavaAttributeInfo[] Attributes;

        public CodeAttribute(string attributeName, uint attributeLength, int maxStack, int maxLocals, byte[] code,
            ExceptionTableEntry[] exceptionTable, JavaAttributeInfo[] attributes)
            : base(attributeName, attributeLength)
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
        public readonly int StartPc;
        public readonly int EndPc;
        public readonly int HandlerPc;
        public readonly int CatchType;

        public ExceptionTableEntry(int startPc, int endPc, int handlerPc, int catchType)
        {
            StartPc = startPc;
            EndPc = endPc;
            HandlerPc = handlerPc;
            CatchType = catchType;
        }
    }

    public class UnknownAttribute : JavaAttributeInfo
    {
        public byte[] Data;
        public UnknownAttribute(string attributeName, uint attributeLength, byte[] data) : base(attributeName, attributeLength)
        {
            Data = data;
        }
    }
}