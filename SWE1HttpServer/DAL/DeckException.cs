using System;
using System.Runtime.Serialization;

namespace Server.DAL
{
    [Serializable]
    internal class DeckException : Exception
    {
        public DeckException()
        {
        }

        public DeckException(string message) : base(message)
        {
        }

        public DeckException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeckException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}