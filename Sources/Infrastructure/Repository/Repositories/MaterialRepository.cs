using Domain.Interfaces;
using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class MaterialRepository : GenericRepository<Material, int>, IMaterialRepository
    {
        public MaterialRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ICollection<Material>> GetAll(MaterialType type)
        {
            return await Context.Materials
                .Include(m => m.ProductMaterial)
                .Where(p => p.Type == type)
                .ToListAsync(ComponentDisposed);
        }

        public async Task UpdateAllMaterialCost(int idMat)
        {
            var matToUpdate = await Context.Materials
                .Include(p => p.ProductMaterial).SingleAsync(m => m.Id == idMat, ComponentDisposed);

            foreach (var item in matToUpdate.ProductMaterial)
            {
                item.Cost = matToUpdate.Cost;
            }

            await Context.SaveChangesAsync();
        }
    }
}