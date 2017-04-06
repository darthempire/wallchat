using System;
using System.Runtime.Serialization;

namespace wallchat.Helpers.Exceptions
{
    public class RepositoryException : WallchatException
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string message)
            : base (message)
        {
        }

        public RepositoryException(string message, params object[] args)
            : base (FormatArgs (message, args))
        {
        }

        public RepositoryException(Exception inner, string message, params object[] args)
            : base (FormatArgs (message, args), inner)
        {
        }

        public RepositoryException(string message, Exception inner)
            : base (message, inner)
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
        }

        private static string FormatArgs(string message, object[] args)
        {
            return args == null || args.Length <= 0 ? message : string.Format(message, args);
        }
    }
}