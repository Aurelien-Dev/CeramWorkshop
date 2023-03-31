using Domain.Interfaces;
using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }


        public async Task<Product> Get(int id, int idWorkshop)
        {
            return await _context.Products
                                 .Where(p => p.Id == id && p.IdWorkshop == idWorkshop)
                                 .Include(p => p.ImageInstructions)
                                 .Include(p => p.ProductMaterial)
                                 .ThenInclude(x => x.Material)
                                 .Include(p => p.ProductFiring)
                                 .ThenInclude(x => x.Firing)
                                 .FirstAsync();
        }


        public async Task<ICollection<Product>> GetAll(int idWorkshop)
        {
            return await _context.Products
                                 .Where(p => p.IdWorkshop == idWorkshop)
                                 .Include(p => p.ImageInstructions)
                                 .ToListAsync();
        }

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

        public void UpdateProductMaterialCostAndQuantity(ProductMaterial productMaterial)
        {
            if (productMaterial == null) return;

            ProductMaterial pMaterial = _context.ProductMaterials.First(p => p.Id == productMaterial.Id);

            pMaterial.Cost = productMaterial.Cost;
            pMaterial.Quantity = productMaterial.Quantity;

            _context.SaveChanges();
        }

        public void UpdateProductFiring(ProductFiring productFiring)
        {
            if (productFiring == null) return;

            ProductFiring pFiring = _context.ProductFirings.First(p => p.Id == productFiring.Id);

            pFiring.CostKwH = productFiring.CostKwH;

            _context.SaveChanges();
        }
    }
}
