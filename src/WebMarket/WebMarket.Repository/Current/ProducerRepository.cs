using System.Linq;
using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Repository.Current
{
    public class ProducerRepository : RepositoryBase<Producer>, IProducerRepository
    {
        public ProducerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Producer GetByName(string name)
        {
            return DbContext.Producers.FirstOrDefault(obj => obj.Name == name);
        }
    }
}
