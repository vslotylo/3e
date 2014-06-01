using System.Linq;
using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

namespace WebMarket.Repository.Current
{
    public class ProducerRepository : RepositoryBase<Producer>, IProducerRepository
    {
        public ProducerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Producer Find(string name)
        {
            return DbContext.Producers.FirstOrDefault(obj => obj.Name == name);
        }
    }
}
