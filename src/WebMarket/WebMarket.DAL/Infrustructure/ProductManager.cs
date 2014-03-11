using System.Linq;
using WebMarket.DAL.Common;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Interfaces;
using System.Data.Entity;

namespace WebMarket.DAL.Infrustructure
{
    public class ProductManager : IProductManager
    {
        private readonly WebMarketDbContext dbContext = new WebMarketDbContext();
        public Product GetProductByPid(int id)
        {
            return dbContext.Products.Where(obj => obj.Id == id).Include(obj => obj.Producer).Include(obj => obj.Group).SingleOrDefault();
        }
    }
}
