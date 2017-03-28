using wallchat.DAL.App.Contracts;
using wallchat.DAL.App.Implementations;
using wallchat.Model.App.EF;
using wallchat.Model.App.Entity;

namespace wallchat.Repository.App.Authorization
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        private DatabaseContext _dataContext;

        public RefreshTokenRepository ( IDatabaseFactory databaseFactory )
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

    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
    }
}