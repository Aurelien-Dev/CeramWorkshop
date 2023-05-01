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

        public async Task<ICollection<Material>> GetAll(MaterialType type, CancellationToken cancellationToken = default)
        {
            return await Context.Materials
                .Include(m => m.ProductMaterial)
                .Where(p => p.Type == type)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAllMaterialCost(int idMat, CancellationToken cancellationToken = default)
        {
            var matToUpdate = await Context.Materials
                .Include(p => p.ProductMaterial).SingleAsync(m => m.Id == idMat, cancellationToken);

            foreach (var item in matToUpdate.ProductMaterial)
            {
                item.Cost = matToUpdate.Cost;
            }

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}