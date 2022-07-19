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
            return await _context.Materials
                                 .Where(p => p.Type == type)
                                 .ToListAsync();
        }
    }
}
