﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaNet.Runtime.Plugs.NativeImpl
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class NativeImplAttribute : System.Attribute
    {
        public string ReturnType { get; }
        public string DeclaringType { get; }
        public string MethodName { get; }
        public string[] ArgTypes { get; }
        public bool IsStatic { get; set; }
        public NativeImplAttribute(string returnType, string declaringType, string methodName, params string[] argTypes)
        {
            ReturnType = returnType;
            DeclaringType = declaringType;
            MethodName = methodName;
            ArgTypes = argTypes;
        }

        public NativeImplAttribute(Type returnType, string declaringType, string methodName, params Type[] artTypes)
        {
            ReturnType = returnType.FullName;
            DeclaringType = declaringType;
            MethodName = methodName;
            ArgTypes = artTypes.Select(x => x.FullName).ToArray();
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public sealed class NativeDataAttribute : Attribute
    {
        public string TargetClass { get; }
        public bool IsStatic { get; }

        public NativeDataAttribute(string targetClass, bool isStatic)
        {
            TargetClass = targetClass;
            IsStatic = isStatic;
        }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public sealed class NativeDataParamAttribute : Attribute
    {
        public NativeDataParamAttribute()
        {
            
        }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class TypeHandleAttribute : Attribute
    {
        public string TypeName { get; }

        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public TypeHandleAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public sealed class FieldPtrAttribute : Attribute
    {
        public string FieldName { get; }

        public FieldPtrAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }

    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = true)]
    public sealed class MethodPtrAttribute : Attribute
    {
        public bool IsStatic { get; }
        public string ReturnType { get; }
        public string MethodName { get; }
        public string[] ArgTypes { get; }
        public MethodPtrAttribute(bool isStatic, string returnType, string methodName, params string[] argTypes)
        {
            IsStatic = isStatic;
            ReturnType = returnType;
            MethodName = methodName;
            ArgTypes = argTypes;
        }
    }
}
