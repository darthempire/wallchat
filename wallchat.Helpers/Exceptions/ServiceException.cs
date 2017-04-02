using System;

namespace wallchat.Helpers.Exceptions
{
    public class ServiceException : WallchatException
    {
        public ServiceException ( string message )
            : base (message)
        {
        }
    }
}


