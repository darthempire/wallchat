using System;

namespace wallchat.Helpers.Exceptions
{
    public class ControllerException : Exception
    {
        public ControllerException ( string message )
            : base (message)
        {
        }
    }
}