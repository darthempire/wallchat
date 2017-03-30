using System;

namespace wallchat.Helpers.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException ( string message )
            : base (message)
        {
        }
    }
}