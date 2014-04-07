using System.Collections.Generic;

namespace WebMarket.Repository.Core
{
    public interface IRepository<TEntity>
      where TEntity : class, new()
    {
        void Insert(TEntity entity);
        void Insert<T>(IList<T> entities) where T : class;
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        TEntity Get(object id);
        IEnumerable<TEntity> GetAll();
        void Commit();
    }
}
