using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace JavaNet
{
    public static class JarReader
    {
        public static JarFile BuildJarFile(Stream input)
        {
            var zip = new ZipArchive(input, ZipArchiveMode.Read, true);
            var jf = new JarFile();
            var classFiles = new List<ClassFile>();
            foreach (var entry in zip.Entries)
            {
                if (entry.Name.EndsWith(".class"))
                {
                    classFiles.Add(BuildClassFile(entry.Open()));
                }
            }

            jf.ClassFiles = classFiles.ToArray();
            return jf;
        }

        private static ClassFile BuildClassFile(Stream s)
        {
            var cf = new ClassFile();
            cf.Magic = s.U4();
            if (cf.Magic != 0xcafebabe)
            {
                Console.WriteLine("unexpected magic number 0x{0:X4} in class file", cf.Magic);
            }

            cf.MinorVersion = s.U2();
            cf.MajorVersion = s.U2();
            var cpCount = s.U2();
            cf.ConstantPool = new CpInfo[cpCount];
            for (var i = 1; i < cpCount; i++)
            {
                cf.ConstantPool[i] = BuildCpInfo(s);
                //Console.WriteLine("{0} / {1}: {2}", i, cf.ConstantPoolCount, cf.ConstantPool[i]);
                if (cf.ConstantPool[i] is LongInfo || cf.ConstantPool[i] is DoubleInfo)
                    i++;
            }

            foreach (var info in cf.ConstantPool)
            {
                info?.Fill(cf.ConstantPool);
            }

            cf.AccessFlags = (ClassFile.Flags) s.U2();
            cf.ThisClass = (ClassInfo)cf.ConstantPool[s.U2()];
            cf.SuperClass = (ClassInfo)cf.ConstantPool[s.U2()];
            var intCount = s.U2();
            cf.Interfaces = new ClassInfo[intCount];
            for (var i = 0; i < intCount; i++)
            {
                cf.Interfaces[i] = (ClassInfo)cf.ConstantPool[s.U2()];
            }

            var fieldsCount = s.U2();
            cf.Fields = new JavaFieldInfo[fieldsCount];
            for (var i = 0; i < fieldsCount; i++)
            {
                cf.Fields[i] = BuildFieldInfo(s, cf.ConstantPool);
            }
            
            var methodsCount = s.U2();
            cf.Methods = new JavaMethodInfo[methodsCount];
            for (var i = 0; i < methodsCount; i++)
            {
                cf.Methods[i] = BuildMethodInfo(s, cf.ConstantPool);
            }
            
            var attrCount = s.U2();
            cf.Attributes = new JavaAttributeInfo[attrCount];
            for (var i = 0; i < attrCount; i++)
            {
                cf.Attributes[i] = BuildAttributeInfo(s, cf.ConstantPool);
            }

            return cf;
        }

        private static JavaMethodInfo BuildMethodInfo(Stream s, CpInfo[] cp)
        {
            var mi = new JavaMethodInfo();
            mi.AccessFlags = (JavaMethodInfo.Flags) s.U2();
            mi.Name = ((Utf8Info)cp[s.U2()]).Data;
            mi.Descriptor = ((Utf8Info)cp[s.U2()]).Data;
            mi.AttributesCount = s.U2();
            mi.Attributes = new JavaAttributeInfo[mi.AttributesCount];
            for (var i = 0; i < mi.AttributesCount; i++)
            {
                mi.Attributes[i] = BuildAttributeInfo(s, cp);
            }

            return mi;
        }

        private static JavaFieldInfo BuildFieldInfo(Stream s, CpInfo[] cp)
        {
            var fi = new JavaFieldInfo();
            fi.AccessFlags = (JavaFieldInfo.Flags) s.U2();
            fi.Name = ((Utf8Info)cp[s.U2()]).Data;
            fi.Descriptor = ((Utf8Info)cp[s.U2()]).Data;
            fi.AttributesCount = s.U2();
            fi.Attributes = new JavaAttributeInfo[fi.AttributesCount];
            for (var i = 0; i < fi.AttributesCount; i++)
            {
                fi.Attributes[i] = BuildAttributeInfo(s, cp);
            }

            return fi;
        }

        private static JavaAttributeInfo BuildAttributeInfo(Stream s, CpInfo[] cp)
        {
            var name = ((Utf8Info)cp[s.U2()]).Data;
            var len = s.U4();
            switch (name)
            {
                case AttributeName.ConstantValue:
                    return new ConstantValueAttribute(name, len, cp[s.U2()]);
                case AttributeName.Code:
                {
                    var maxStack = s.U2();
                    var maxLocals = s.U2();
                    var codeLength = s.U4();
                    var code = s.ReadNext((int) codeLength);
                    var excTblLength = s.U2();
                    var excTbl = new ExceptionTableEntry[excTblLength];
                    for (var i = 0; i < excTblLength; i++)
                    {
                        excTbl[i] = new ExceptionTableEntry(s.U2(), s.U2(), s.U2(), s.U2());
                    }

                    var attrCount = s.U2();
                    var attrs = new JavaAttributeInfo[attrCount];
                    for (var i = 0; i < attrCount; i++)
                    {
                        attrs[i] = BuildAttributeInfo(s, cp);
                    }

                    return new CodeAttribute(name, len, maxStack, maxLocals, code, excTbl, attrs);
                }
                default:
                    return new UnknownAttribute(name, len, s.ReadNext((int) len));
            }
        }

        private static CpInfo BuildCpInfo(Stream s)
        {
            var tag = (ConstantPoolTag) s.U1();
            switch (tag)
            {
                case ConstantPoolTag.Class:
                    return new ClassInfo(tag, s.U2());
                case ConstantPoolTag.Fieldref:
                case ConstantPoolTag.Methodref:
                case ConstantPoolTag.InterfaceMethodref:
                    return new FieldOrMethodrefInfo(tag, s.U2(), s.U2());
                case ConstantPoolTag.String:
                    return new StringInfo(tag, s.U2());
                case ConstantPoolTag.Integer:
                    return new IntegerInfo(tag, s.I4());
                case ConstantPoolTag.Float:
                    return new FloatInfo(tag, BitConverter.Int32BitsToSingle(s.I4()));
                case ConstantPoolTag.Long:
                    return new LongInfo(tag, s.I8());
                case ConstantPoolTag.Double:
                    return new DoubleInfo(tag, BitConverter.Int64BitsToDouble(s.I8()));
                case ConstantPoolTag.NameAndType:
                    return new NameAndTypeInfo(tag, s.U2(), s.U2());
                case ConstantPoolTag.Utf8:
                    var len = s.U2();
                    var utf8 = s.ReadNext(len);
                    return new Utf8Info(tag, len, Encoding.UTF8.GetString(utf8));
                case ConstantPoolTag.MethodHandle:
                    return new MethodHandleInfo(tag, (MethodHandleType) s.U1(), s.U2());
                case ConstantPoolTag.MethodType:
                    return new MethodTypeInfo(tag, s.U2());
                case ConstantPoolTag.InvokeDynamic:
                    return new InvokeDynamicInfo(tag, s.U2(), s.U2());
                case ConstantPoolTag.Module:
                case ConstantPoolTag.Package:
                    return new ModuleOrPackageInfo(tag, s.U2());
                default:
                    throw new ArgumentOutOfRangeException(nameof(tag), tag, "Invalid value");
            }
        }
    }
}