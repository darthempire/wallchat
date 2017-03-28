namespace wallchat.DAL.App.Contracts
{
    public interface IUnitOfWork
    {
        void Commit ();
        void CommitAsync();
    }
}