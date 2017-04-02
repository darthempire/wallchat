using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using NLog;
using wallchat.DAL.App.Contracts;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.EF;

namespace wallchat.DAL.App.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbSet<T> _dbset;
        private DatabaseContext _dataContext;
        private readonly Logger _logger;

        protected Repository ( IDatabaseFactory databaseFactory )
        {
            _logger = LogManager.GetCurrentClassLogger( );
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>( );
        }

        protected IDatabaseFactory DatabaseFactory { get; }

        protected DatabaseContext DataContext => _dataContext ?? ( _dataContext = DatabaseFactory.Get( ) );

        public virtual void Add ( T entity )
        {
            try
            {
                _dbset.Add (entity);
                DataContext.SaveChanges( );
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with ADD " + typeof ( T ).FullName + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with ADD " + typeof ( T ).FullName + " entity");
            }
        }

        public virtual void Update ( T entity )
        {
            try
            {
                _dbset.Attach(entity);
                _dataContext.Entry(entity).State = EntityState.Modified;
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error("Repository exception, error with UPDATE " + typeof(T).FullName + " entity");
                _logger.Error("EXCEPTION: " + ex);
                throw new RepositoryException(
                    "Repository exception, error with UPDATE " + typeof(T).FullName + " entity");
            }
        }

        public virtual void Delete ( T entity )
        {
            try
            {
                _dbset.Attach(entity);
                _dataContext.Entry(entity).State = EntityState.Deleted;
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error("Repository exception, error with DELETE " + typeof(T).FullName + " entity");
                _logger.Error("EXCEPTION: " + ex);
                throw new RepositoryException(
                    "Repository exception, error with DELETE " + typeof(T).FullName + " entity");
            }
        }

        public virtual void Delete ( Expression<Func<T, bool>> where )
        {
            try
            {
                var objects = _dbset.Where(where).AsEnumerable();
                foreach (var obj in objects)
                    _dbset.Remove(obj);
            }
            catch (Exception ex)
            {
                _logger.Error("Repository exception, error with DELETE " + typeof(T).FullName + " entity");
                _logger.Error("EXCEPTION: " + ex);
                throw new RepositoryException(
                    "Repository exception, error with DELETE " + typeof(T).FullName + " entity");
            }
        }

        public virtual T GetById ( long id )
        {
            try
            {
                return _dbset.Find(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Repository exception, error with GET BY ID " + typeof(T).FullName + " entity");
                _logger.Error("EXCEPTION: " + ex);
                throw new RepositoryException(
                    "Repository exception, error with GET BY ID " + typeof(T).FullName + " entity");
            }
        }

        public virtual T GetById ( string id )
        {
            try
            {
                return _dbset.Find(id);
            }
            catch (Exception ex)
            {
                _logger.Error("Repository exception, error with GET BY ID " + typeof(T).FullName + " entity");
                _logger.Error("EXCEPTION: " + ex);
                throw new RepositoryException(
                    "Repository exception, error with GET BY ID " + typeof(T).FullName + " entity");
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return _dbset.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Repository exception, error with GET ALL " + typeof(T).FullName + " entity");
                _logger.Error("EXCEPTION: " + ex);
                throw new RepositoryException(
                    "Repository exception, error with GET ALL " + typeof(T).FullName + " entity");
            }
        }

        public virtual IEnumerable<T> GetMany ( Expression<Func<T, bool>> where )
        {
            try
            {
                return _dbset.Where(where).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Repository exception, error with GET MANY " + typeof(T).FullName + " entity");
                _logger.Error("EXCEPTION: " + ex);
                throw new RepositoryException(
                    "Repository exception, error with GET MANY " + typeof(T).FullName + " entity");
            }
        }

        public T Get ( Expression<Func<T, bool>> where )
        {
            try
            {
                return _dbset.Where(where).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error("Repository exception, error with GET BY EXPRESSION " + typeof(T).FullName + " entity");
                _logger.Error("EXCEPTION: " + ex);
                throw new RepositoryException(
                    "Repository exception, error with GET BY EXPRESSION " + typeof(T).FullName + " entity");
            }
        }
    }
}