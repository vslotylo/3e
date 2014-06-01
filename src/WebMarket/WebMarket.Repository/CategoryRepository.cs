using System.Linq;
using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

namespace WebMarket.Repository.Current
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Category Find(string name)
        {
            return DbContext.Categories.FirstOrDefault(obj => obj.Name == name);
        }
    }
}
