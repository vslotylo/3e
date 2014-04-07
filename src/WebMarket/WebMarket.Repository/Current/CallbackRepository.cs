using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Repository.Current
{
    public class CallbackRepository : RepositoryBase<Callback>, ICallbackRepository
    {
        public CallbackRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
