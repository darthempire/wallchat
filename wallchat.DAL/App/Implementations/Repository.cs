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
        private readonly Logger _logger;
        private DatabaseContext _dataContext;

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
                _logger.Info ("REPOSITORY: Add " + nameof (T) + " entity");
                DataContext.SaveChanges( );
                _logger.Info ("REPOSITORY: SaveChanges();");
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with ADD " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with ADD " + nameof (T) + " entity");
            }
        }

        public virtual void Update ( T entity )
        {
            try
            {
                _dbset.Attach (entity);
                _dataContext.Entry (entity).State = EntityState.Modified;
                _logger.Info ("REPOSITORY: Update " + nameof (T) + " entity");
                DataContext.SaveChanges( );
                _logger.Info ("REPOSITORY: SaveChanges();");
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with UPDATE " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with UPDATE " + nameof (T) + " entity");
            }
        }

        public virtual void Delete ( T entity )
        {
            try
            {
                _dbset.Attach (entity);
                _dataContext.Entry (entity).State = EntityState.Deleted;
                _logger.Info ("REPOSITORY: Delete " + nameof (T) + " entity");
                DataContext.SaveChanges( );
                _logger.Info ("REPOSITORY: SaveChanges();");
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with DELETE " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with DELETE " + nameof (T) + " entity");
            }
        }

        public virtual void Delete ( Expression<Func<T, bool>> where )
        {
            try
            {
                var objects = _dbset.Where (where).AsEnumerable( );
                foreach ( var obj in objects )
                    _dbset.Remove (obj);
                _logger.Info ("REPOSITORY: Delete " + nameof (T) + " entity");
                DataContext.SaveChanges( );
                _logger.Info ("REPOSITORY: SaveChanges();");
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with DELETE " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with DELETE " + nameof (T) + " entity");
            }
        }

        public virtual T GetById ( long id )
        {
            try
            {
                return _dbset.Find (id);
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with GET BY ID " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with GET BY ID " + nameof (T) + " entity");
            }
        }

        public virtual T GetById ( string id )
        {
            try
            {
                _logger.Info ("REPOSITORY: Getting Buy Id " + nameof (T) + " entity");
                return _dbset.Find (id);
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with GET BY ID " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with GET BY ID " + nameof (T) + " entity");
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                _logger.Info("REPOSITORY: Getting List " + nameof(T) + " entity");
                return _dbset.ToList( );
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with GET ALL " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with GET ALL " + nameof (T) + " entity");
            }
        }

        public virtual IEnumerable<T> GetMany ( Expression<Func<T, bool>> where )
        {
            try
            {
                _logger.Info("REPOSITORY: Getting List(Many) " + nameof(T) + " entity");
                return _dbset.Where (where).ToList( );
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with GET MANY " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with GET MANY " + nameof (T) + " entity");
            }
        }

        public T Get ( Expression<Func<T, bool>> where )
        {
            try
            {
                _logger.Info("REPOSITORY: Getting By expression " + nameof(T) + " entity");
                return _dbset.Where (where).FirstOrDefault( );
            }
            catch ( Exception ex )
            {
                _logger.Error ("Repository exception, error with GET BY EXPRESSION " + nameof (T) + " entity");
                _logger.Error ("EXCEPTION: " + ex);
                throw new RepositoryException (
                    "Repository exception, error with GET BY EXPRESSION " + nameof (T) + " entity");
            }
        }
    }
}