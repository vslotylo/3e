using WebMarket.DAL.Entities;

namespace WebMarket.DAL.Interfaces
{
    public interface IProductManager
    {
        Product GetProductByPid(int id);
    }
}
