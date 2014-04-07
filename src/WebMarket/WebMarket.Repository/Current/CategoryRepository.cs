using System.Linq;
using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Repository.Current
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Category GetByName(string categoryName)
        {
            return DbContext.Categories.FirstOrDefault(obj => obj.Name == categoryName);
        }
    }
}
