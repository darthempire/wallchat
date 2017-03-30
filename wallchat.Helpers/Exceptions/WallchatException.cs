using System;
using System.Runtime.Serialization;

namespace wallchat.Helpers.Exceptions
{
    public class WallchatException : Exception
    {
        public WallchatException()
        {
        }

        public WallchatException ( string message )
            : base (message)
        {
        }

        public WallchatException ( string message, params object[] args )
            : base (FormatArgs (message, args))
        {
        }

        public WallchatException ( Exception inner, string message, params object[] args )
            : base (FormatArgs (message, args), inner)
        {
        }

        public WallchatException ( string message, Exception inner )
            : base (message, inner)
        {
        }

        protected WallchatException ( SerializationInfo info, StreamingContext context )
            : base (info, context)
        {
        }

        private static string FormatArgs ( string message, object[] args )
        {
            return args == null || args.Length <= 0 ? message : string.Format (message, args);
        }
    }
}