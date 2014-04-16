using System;
using System.Data.Entity;
using WebMarket.Repository.Exceptions;

namespace WebMarket.Repository.Core
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private bool disposed;

        public UnitOfWork(IDbContextFactory<TContext> dataContextFactory)
        {
            DbContext = dataContextFactory.GetDataContext();
        }

        internal TContext DbContext { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new RepositoryException("Failed to apply changes on database.", e);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }

            disposed = true;
        }
    }
}