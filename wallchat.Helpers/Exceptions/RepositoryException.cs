namespace wallchat.Helpers.Exceptions
{
    public class RepositoryException : WallchatException
    {
        public RepositoryException(string message)
            : base (message)
        {
        }
    }
}