namespace wallchat.Helpers.Exceptions
{
    public class ControllerException : WallchatException
    {
        public ControllerException ( string message )
            : base (message)
        {
        }
    }
}