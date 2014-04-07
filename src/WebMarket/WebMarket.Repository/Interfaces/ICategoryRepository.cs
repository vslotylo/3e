using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

namespace WebMarket.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByName(string categoryName);
    }
}
