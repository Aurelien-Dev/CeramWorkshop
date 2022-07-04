using Domain.Interfaces;
using Domain.Models.WorkshopDomaine;

namespace Repository.Repositories
{
    public class WorkshopRepository : GenericRepository<Workshop>, IWorkshopRepository
    {
        public WorkshopRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
