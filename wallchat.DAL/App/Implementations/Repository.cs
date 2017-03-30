using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using wallchat.DAL.App.Contracts;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.EF;

namespace wallchat.DAL.App.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbSet<T> _dbset;
        private DatabaseContext _dataContext;

        protected Repository ( IDatabaseFactory databaseFactory )
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T> ( );
        }

        protected IDatabaseFactory DatabaseFactory { get; }

        protected DatabaseContext DataContext => _dataContext ?? ( _dataContext = DatabaseFactory.Get ( ) );

        public virtual void Add ( T entity )
        {
            try
            {
                _dbset.Add (entity);
                DataContext.SaveChanges( );
            }
            catch (Exception ex)
            {
                //logger
                throw new DalException ("DAL exception, error with Adding " + typeof ( T ).FullName + " entity");
            }
        }

        public virtual void Update ( T entity )
        {
            _dbset.Attach (entity);
            _dataContext.Entry (entity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public virtual void Delete ( T entity )
        {
            _dbset.Attach (entity);
            _dataContext.Entry (entity).State = EntityState.Deleted;
            DataContext.SaveChanges ( );
        }

        public virtual void Delete ( Expression<Func<T, bool>> where )
        {
            var objects = _dbset.Where (where).AsEnumerable ( );
            foreach ( var obj in objects )
                _dbset.Remove (obj);
        }

        public virtual T GetById ( long id )
        {
            return _dbset.Find (id);
        }

        public virtual T GetById ( string id )
        {
            return _dbset.Find (id);
        }

        public virtual IEnumerable<T> GetAll ()
        {
            return _dbset.ToList ( );
        }

        public virtual IEnumerable<T> GetMany ( Expression<Func<T, bool>> where )
        {
            return _dbset.Where (where).ToList ( );
        }

        public T Get ( Expression<Func<T, bool>> where )
        {
            return _dbset.Where (where).FirstOrDefault ( );
        }
    }
}