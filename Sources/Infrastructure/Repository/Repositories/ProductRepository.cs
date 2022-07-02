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

        #region override
        public override async Task<Product> Get(object id)
        {
            return await _context.Products
                                 .Where(p => p.Id == (int)id)
                                 .Include(p => p.ImageInstructions)
                                 .Include(p => p.ProductMaterial)
                                 .ThenInclude(x => x.Material)
                                 .FirstAsync();
        }

        public override async Task<ICollection<Product>> GetAll()
        {
            return await _context.Products
                                 .Include(p => p.ImageInstructions)
                                 .ToListAsync();
        }
        #endregion


        public async Task<Product> GetLight(object id)
        {
            return await _context.Products
                                 .Where(p => p.Id == (int)id)
                                 .FirstAsync();
        }

        public async Task<int> CountImage(int id)
        {
            return await _context.ImageInstruction.Where(i => i.IdProduct == id).CountAsync();
        }

        public void UpdateProductMaterial(ProductMaterial productMaterial)
        {
            if (productMaterial == null) return;

            ProductMaterial pMaterial = _context.ProductMaterials.First(p => p.Id == productMaterial.Id);

            pMaterial.Cost = productMaterial.Cost;
            pMaterial.Quantity = productMaterial.Quantity;

            _context.SaveChanges();
        }
    }
}
