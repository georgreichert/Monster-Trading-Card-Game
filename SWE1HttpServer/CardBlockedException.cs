using System;
using System.Runtime.Serialization;

namespace Server
{
    [Serializable]
    internal class CardBlockedException : Exception
    {
        public CardBlockedException()
        {
        }

        public CardBlockedException(string message) : base(message)
        {
        }

        public CardBlockedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CardBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}