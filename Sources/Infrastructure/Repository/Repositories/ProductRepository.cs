using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override async Task<Product> Get(object id)
        {
            return await _context.Products
                                 .Where(p => p.Id == (int)id)
                                 .Include(p => p.ImageInstructions)
                                 .FirstAsync();
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                                 .Include(p => p.ImageInstructions)
                                 .ToListAsync();
        }

        public async Task<int> CountImage(int idProduct)
        {
            return await _context.ImageInstruction.Where(i => i.IdProduct == idProduct).CountAsync();
        }
    }
}
