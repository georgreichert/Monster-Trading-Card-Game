using System;
using System.Runtime.Serialization;

namespace Server.DAL
{
    [Serializable]
    internal class NoPackagesException : Exception
    {
        public NoPackagesException()
        {
        }

        public NoPackagesException(string message) : base(message)
        {
        }

        public NoPackagesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoPackagesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}