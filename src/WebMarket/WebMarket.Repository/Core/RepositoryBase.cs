using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using WebMarket.Repository.Exceptions;

namespace WebMarket.Repository.Core
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        private readonly UnitOfWork<WebMarketDbContext> unitOfWork;

        protected RepositoryBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork as UnitOfWork<WebMarketDbContext>;
        }

        protected WebMarketDbContext DbContext
        {
            get
            {
                return unitOfWork.DataContext;
            }
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                DbContext.Set<TEntity>().Add(entity);
            }
            catch (Exception e)
            {
                throw GetRepositoryException(e, "Failed to insert record into database.");
            }
        }

        public void Insert<T>(IList<T> entities) where T : class
        {
            try
            {
                DbContext.Set<T>().AddRange(entities);
            }
            catch (Exception e)
            {
                throw GetRepositoryException(e, "Failed to insert record into database.");
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                this.DbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw GetRepositoryException(e, "Failed to insert record into database.");
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                DbContext.Set<TEntity>().Remove(entity);
            }
            catch (Exception e)
            {
                throw GetRepositoryException(e, "Failed to delete record from database.");
            }
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            try
            {
                DbContext.Set<TEntity>().RemoveRange(entities);
            }
            catch (Exception e)
            {
                throw GetRepositoryException(e, "Failed to delete records from database.");
            }
        }

        public virtual TEntity Get(object id)
        {
            try
            {
                return DbContext.Set<TEntity>().Find(id);
            }
            catch (Exception e)
            {
                throw GetRepositoryException(e, "Failed to delete records from database.");
            }
        }

        protected RepositoryException GetRepositoryException(Exception e, string format, params object[] args)
        {
            var builder = new StringBuilder();
            builder.AppendFormat(format, args);
            builder.AppendFormat(" Entity type: {0} in {1}", typeof(TEntity).Name, DbContext.Set<TEntity>());
            var exc = new RepositoryException(builder.ToString(), e);
            return exc;
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return DbContext.Set<TEntity>();
            }
            catch (Exception e)
            {
                throw GetRepositoryException(e, "Failed to get records from database.");
            }
        }

        public void Commit()
        {
            try
            {
                unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw GetRepositoryException(e, "Failed to apply changes on database.");
            }
        }
    }
}