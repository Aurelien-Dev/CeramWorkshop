using Domain.Interfaces;
using Domain.Models.WorkshopDomaine;

namespace Repository.Repositories
{
    public class WorkshopRepository : GenericRepository<Workshop, int>, IWorkshopRepository
    {
        public WorkshopRepository(ApplicationDbContext context) : base(context) { }


        public Workshop? GetForLogin(string email)
        {
            return _context.Workshops.FirstOrDefault(w => w.Email == email);
        }

        public bool CheckIfEmailExists(string email)
        {
            return _context.Workshops.Any(w => w.Email == email);
        }
    }
}