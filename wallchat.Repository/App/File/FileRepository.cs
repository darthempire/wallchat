using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wallchat.DAL.App.Contracts;
using wallchat.DAL.App.Implementations;
using wallchat.Model.App.EF;

namespace wallchat.Repository.App.File
{
    public class FileRepository:Repository<Model.App.Entity.File>,IFileRepository
    {
        private DatabaseContext _dataContext;

        public FileRepository(IDatabaseFactory databaseFactory)
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
    public interface IFileRepository : IRepository<Model.App.Entity.File>
    {
    }
}
