using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Repository.Current
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
