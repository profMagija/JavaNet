using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavaNet.Runtime.Plugs
{
    public static class Intrinsics
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Fcmpg(float a, float b)
        {
            return a < b ? -1 : a == b ? 0 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Fcmpl(float a, float b)
        {
            return a > b ? 1 : a == b ? 0 : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Lcmp(long a, long b)
        {
            return a > b ? 1 : a == b ? 0 : -1;
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class SignaturePolymorphicAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public SignaturePolymorphicAttribute()
        {
            // TODO: Implement code here
            throw new NotImplementedException();
        }
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public sealed class JavaNameAttribute : Attribute
    {
        public string Name { get; }

        public static readonly ConstructorInfo Ctor = typeof(JavaNameAttribute).GetConstructors()[0];

        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public JavaNameAttribute(string name)
        {
            Name = name;
        }
    }
}
