using System;
using System.Runtime.Serialization;

namespace Server.DAL
{
    [Serializable]
    internal class DuplicateSaleException : Exception
    {
        public DuplicateSaleException()
        {
        }

        public DuplicateSaleException(string message) : base(message)
        {
        }

        public DuplicateSaleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateSaleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}