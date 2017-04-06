using System;
using System.Runtime.Serialization;

namespace wallchat.Helpers.Exceptions
{
    public class ControllerException : WallchatException
    {
        public ControllerException()
        {
        }

        public ControllerException(string message)
            : base (message)
        {
        }

        public ControllerException(string message, params object[] args)
            : base (FormatArgs (message, args))
        {
        }

        public ControllerException(Exception inner, string message, params object[] args)
            : base (FormatArgs (message, args), inner)
        {
        }

        public ControllerException(string message, Exception inner)
            : base (message, inner)
        {
        }

        protected ControllerException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        private static string FormatArgs(string message, object[] args)
        {
            return args == null || args.Length <= 0 ? message : string.Format(message, args);
        }
    }
}