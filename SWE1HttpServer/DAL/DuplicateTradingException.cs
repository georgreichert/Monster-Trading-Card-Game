using System;
using System.Runtime.Serialization;

namespace Server.DAL
{
    [Serializable]
    internal class DuplicateTradingException : Exception
    {
        public DuplicateTradingException()
        {
        }

        public DuplicateTradingException(string message) : base(message)
        {
        }

        public DuplicateTradingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateTradingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}