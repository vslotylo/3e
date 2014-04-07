using System;
using System.Data.Entity;

namespace WebMarket.Repository.Core
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private bool disposed;

        public UnitOfWork(IDbContextFactory<TContext> dataContextFactory)
        {
            DataContext = dataContextFactory.GetDataContext();
        }

        internal TContext DataContext { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DataContext.Dispose();
                }
            }

            disposed = true;
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }
    }
}