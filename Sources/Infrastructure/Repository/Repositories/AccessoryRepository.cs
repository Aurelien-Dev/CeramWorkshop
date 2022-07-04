using Domain.Interfaces;
using Domain.Models.MainDomain;

namespace Repository.Repositories
{
    public class AccessoryRepository : GenericRepository<Accessory, int>, IAccessoryRepository
    {
        public AccessoryRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
