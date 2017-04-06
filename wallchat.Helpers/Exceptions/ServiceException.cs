using System;
using System.Runtime.Serialization;

namespace wallchat.Helpers.Exceptions
{
    public class ServiceException : WallchatException
    {
        public ServiceException()
        {
        }

        public ServiceException(string message)
            : base (message)
        {
        }

        public ServiceException(string message, params object[] args)
            : base (FormatArgs (message, args))
        {
        }

        public ServiceException(Exception inner, string message, params object[] args)
            : base (FormatArgs (message, args), inner)
        {
        }

        public ServiceException(string message, Exception inner)
            : base (message, inner)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        private static string FormatArgs(string message, object[] args)
        {
            return args == null || args.Length <= 0 ? message : string.Format(message, args);
        }
    }
}


