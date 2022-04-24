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
                                 .Include(p => p.ProductImageInstruction)
                                 .FirstAsync();
        }
        public int CountImage(object id)
        {
            return _context.Products
                                 .Where(p => p.Id == (int)id)
                                 .Include(p => p.ProductImageInstruction).Select(p => p.ProductImageInstruction.Count()).FirstOrDefault();
        }
    }
}
