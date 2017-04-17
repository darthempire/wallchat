using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wallchat.DAL.App.Contracts;
using wallchat.DAL.App.Implementations;
using wallchat.Model.App.EF;
using wallchat.Model.App.Entity;

namespace wallchat.Repository.App.News
{
    public class ArticleRepository: Repository<Article> ,INewRepository
    {
        private DatabaseContext _dataContext;

        public ArticleRepository(IDatabaseFactory databaseFactory)
            : base (databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        protected IDatabaseFactory DatabaseFactory { get; }

        protected DatabaseContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }
    }
    public interface INewRepository : IRepository<Article>
    {
    }
}
