using Domain.Models.WorkshopDomaine;

namespace Domain.Interfaces
{
    public interface IWorkshopRepository : IGenericRepository<Workshop, int>
    {
        Workshop? GetForLogin(string email);
        bool CheckIfEmailExists(string email);
    }
}