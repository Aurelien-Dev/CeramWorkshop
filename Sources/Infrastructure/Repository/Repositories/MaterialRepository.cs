using Domain.Interfaces;
using Domain.Models.MainDomain;

namespace Repository.Repositories
{
    public class MaterialRepository : GenericRepository<Material, int>, IMaterialRepository
    {
        public MaterialRepository(ApplicationDbContext context) : base(context)
        {


        }
    }
}
