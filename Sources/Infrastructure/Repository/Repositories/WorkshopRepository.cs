using Domain.Interfaces;
using Domain.Models.WorkshopDomaine;

namespace Repository.Repositories
{
    public class WorkshopRepository : GenericRepository<Workshop, int>, IWorkshopRepository
    {
        public WorkshopRepository(ApplicationDbContext context) : base(context) { }

        public (Workshop?, bool) GetWorkshopInformationForLogin(string email)
        {
            Workshop workshop = Context.Workshops.FirstOrDefault(w => w.Email == email);
            bool mailExist = workshop != null && email == workshop.Email;

            return (workshop, mailExist);
        }

        public bool CheckIfEmailExists(string email)
        {
            return Context.Workshops.Any(w => w.Email == email);
        }
    }
}