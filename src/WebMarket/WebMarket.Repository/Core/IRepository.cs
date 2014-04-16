using System.Collections.Generic;

namespace WebMarket.Repository.Core
{
    public interface IRepository<TEntity>
      where TEntity : class, new()
    {
        void Add(TEntity entity);
        void Add<T>(IList<T> entities) where T : class;
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        TEntity Find(object id);
        IEnumerable<TEntity> All();
    }
}
