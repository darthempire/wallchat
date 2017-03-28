using wallchat.DAL.App.Contracts;
using wallchat.DAL.App.Implementations;
using wallchat.Model.App.EF;
using wallchat.Model.App.Entity;

namespace wallchat.Repository.App.Authorization
{
    public class ClientRepository : Repository<Client>
    {
        private DatabaseContext _dataContext;

        public ClientRepository ( IDatabaseFactory databaseFactory )
            : base (databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        protected IDatabaseFactory DatabaseFactory { get; }

        protected DatabaseContext DataContext => _dataContext ?? ( _dataContext = DatabaseFactory.Get ( ) );
    }

    public interface IClientRepository : IRepository<Client>
    {
    }
}