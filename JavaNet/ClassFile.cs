﻿using System;
using System.IO;
using System.Linq;
using Mono.Cecil;

namespace JavaNet
{
    public class ClassFile
    {
        public uint Magic;
        public ushort MinorVersion;
        public ushort MajorVersion;
        public CpInfo[] ConstantPool;
        public Flags AccessFlags;
        public ClassInfo ThisClass;
        public ClassInfo SuperClass;
        public ClassInfo[] Interfaces;
        public JavaFieldInfo[] Fields;
        public JavaMethodInfo[] Methods;
        public JavaAttributeInfo[] Attributes;

        public TypeDefinition TypeDefinition;

        [Flags]
        public enum Flags
        {
            Public = 0x0001, //Declared public; may be accessed from outside its package.
            Final = 0x0010, //Declared final; no subclasses allowed.
            Super = 0x0020, //Treat superclass methods specially when invoked by the invokespecial instruction.
            Interface = 0x0200, //Is an interface, not a class.
            Abstract = 0x0400, //Declared abstract; must not be instantiated.
            Synthetic = 0x1000, //Declared synthetic; not present in the source code.
            Annotation = 0x2000, //Declared as an annotation type.
            Enum = 0x4000 //Declared as an enum type. 
        }
    }
}