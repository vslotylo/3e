using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

namespace WebMarket.Repository.Interfaces
{
    public interface IProducerRepository : IRepository<Producer>
    {
        Producer GetByName(string name);
    }
}
