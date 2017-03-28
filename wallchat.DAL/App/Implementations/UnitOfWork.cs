using wallchat.DAL.App.Contracts;
using wallchat.Model.App.EF;

namespace wallchat.DAL.App.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private DatabaseContext _dataContext;

        public UnitOfWork ( IDatabaseFactory databaseFactory )
        {
            _databaseFactory = databaseFactory;
        }

        protected DatabaseContext DataContext => _dataContext ?? ( _dataContext = _databaseFactory.Get ( ) );

        public void Commit ()
        {
            DataContext.Commit ( );
        }

        public void CommitAsync ()
        {
            DataContext.CommitAsync ( );
        }
    }
}