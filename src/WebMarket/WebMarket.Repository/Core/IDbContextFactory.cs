using System.Data.Entity;

namespace WebMarket.Repository.Core
{
    public interface IDbContextFactory<out TContext> where TContext : DbContext
    {
        TContext GetDataContext();
    }
}
