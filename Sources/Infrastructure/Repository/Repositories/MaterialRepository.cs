using Domain.Interfaces;
using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class MaterialRepository : GenericRepository<Material, int>, IMaterialRepository
    {
        public MaterialRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ICollection<Material>> GetAll(MaterialType type)
        {
            return await Context.Materials
                                 .Where(p => p.Type == type)
                                 .ToListAsync();
        }

        public void UpdateAllMaterialCost(int idMat)
        {
            var matToUpdate = Context.Materials
                .Include(p => p.ProductMaterial)
                .Where(m => m.Id == idMat).Single();

            foreach (var item in matToUpdate.ProductMaterial)
            {
                item.Cost = matToUpdate.Cost;
            }

            Context.SaveChanges();
        }
    }
}