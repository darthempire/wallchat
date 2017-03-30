using System;

namespace wallchat.Helpers.Exceptions
{
    public class DalException : WallchatException
    {
        public DalException(string message)
            : base(message)
        { }
    }
}