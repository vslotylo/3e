using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

namespace WebMarket.Repository.Current
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category Find(string name);
    }
}
