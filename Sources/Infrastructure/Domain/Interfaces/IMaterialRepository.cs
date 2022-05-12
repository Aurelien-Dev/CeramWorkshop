using Domain.Models;

namespace Domain.Interfaces
{
    public interface IMaterialRepository : IGenericRepository<Material>
    {
        void AddAndLinkMaterial(Material material, Product product);
    }
}