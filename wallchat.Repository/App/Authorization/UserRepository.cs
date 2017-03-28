using wallchat.DAL.App.Contracts;
using wallchat.DAL.App.Implementations;
using wallchat.Model.App.EF;
using wallchat.Model.App.Entity;

namespace wallchat.Repository.App.Authorization
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private DatabaseContext _dataContext;

        public UserRepository ( IDatabaseFactory databaseFactory )
            : base (databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        protected IDatabaseFactory DatabaseFactory { get; }

        protected DatabaseContext DataContext
        {
            get { return _dataContext ?? ( _dataContext = DatabaseFactory.Get ( ) ); }
        }
    }

    public interface IUserRepository : IRepository<User>
    {
    }
}