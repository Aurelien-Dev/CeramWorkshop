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

        public async Task<(IEnumerable<Material>, int)> GetAllWithPaging(MaterialType type, int pageNumber, int pageSize, string sortByName, string sortDirection, CancellationToken cancellationToken = default)
        {
            IQueryable<Material> query = Context.Materials.Where(p => p.Type == type).AsQueryable();

            if (sortDirection != "None")
                query = AddSorting(query, sortDirection, sortByName);

            int total = await query.CountAsync(cancellationToken);
            var result = await query
                .Include(m => m.ProductMaterial)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (result, total);
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