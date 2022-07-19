using Domain.Models.MainDomain;

namespace Domain.Interfaces
{
    public interface IMaterialRepository : IGenericRepository<Material, int>
    {
        Task<ICollection<Material>> GetAll(MaterialType type);
    }
}