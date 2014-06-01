using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

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
