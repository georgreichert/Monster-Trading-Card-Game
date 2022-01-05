using System;
using System.Runtime.Serialization;

namespace Server.DAL
{
    [Serializable]
    internal class DeckIsFullException : Exception
    {
        public DeckIsFullException()
        {
        }

        public DeckIsFullException(string message) : base(message)
        {
        }

        public DeckIsFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeckIsFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}