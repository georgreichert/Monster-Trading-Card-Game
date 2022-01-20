using System;
using System.Runtime.Serialization;

namespace Server
{
    [Serializable]
    internal class DuplicateBattleEnqueueException : Exception
    {
        public DuplicateBattleEnqueueException()
        {
        }

        public DuplicateBattleEnqueueException(string message) : base(message)
        {
        }

        public DuplicateBattleEnqueueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateBattleEnqueueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}