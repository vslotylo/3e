using System;

namespace WebMarket.Repository.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception e)
            : base(message, e)
        {

        }
    }
}
