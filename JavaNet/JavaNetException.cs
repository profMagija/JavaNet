using System;

namespace JavaNet
{
    [Serializable]
    public class JavaNetException : Exception
    {
        public enum ReasonType
        {
            ClassLoad
        }

        public ReasonType Reason { get; }

        public JavaNetException(ReasonType reason)
        {
            Reason = reason;
        }
        public JavaNetException(ReasonType reason, string message) : base(message)
        {
            Reason = reason;
        }
        public JavaNetException(ReasonType reason, string message, Exception inner) : base(message, inner)
        {
            Reason = reason;
        }
        protected JavaNetException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}