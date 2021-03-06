﻿using System;

namespace WebMarket.Repository.Exceptions
{
    public class EntityImportException : Exception
    {
        public EntityImportException(string message)
            : base(message)
        {
        }

        public EntityImportException(string message, Exception e)
            : base(message, e)
        {
        }
    }
}
