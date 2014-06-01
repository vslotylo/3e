using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

namespace WebMarket.Repository.Current
{
    public interface IProducerRepository : IRepository<Producer>
    {
        Producer Find(string name);
    }
}
