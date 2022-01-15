﻿using System;
using System.Runtime.Serialization;

namespace Server.DAL
{
    class DataAccessFailedException : Exception
    {
        public DataAccessFailedException()
        {
        }

        public DataAccessFailedException(string message) : base(message)
        {
        }

        public DataAccessFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}