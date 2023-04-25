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
            return await Context.Products
                                 .Where(p => p.Id == id && p.IdWorkshop == idWorkshop)
                                 .Include(p => p.ImageInstructions.OrderByDescending(i => i.IsFavoriteImage))
                                 .Include(p => p.ProductMaterial)
                                 .ThenInclude(x => x.Material)
                                 .Include(p => p.ProductFiring)
                                 .ThenInclude(x => x.Firing)
                                 .FirstAsync(ComponentDisposed);
        }

        public async Task<ICollection<Product>> GetAll(int idWorkshop)
        {
            return await Context.Products
                                 .Where(p => p.IdWorkshop == idWorkshop)
                                 .Include(p => p.ImageInstructions)
                                 .ToListAsync(ComponentDisposed);
        }

        public async Task<Product> GetLight(object id)
        {
            return await Context.Products
                                 .Where(p => p.Id == (int)id)
                                 .FirstAsync(ComponentDisposed);
        }

        public async Task<int> CountImage(int id)
        {
            return await Context.ImageInstruction.Where(i => i.IdProduct == id).CountAsync(ComponentDisposed);
        }

        public async Task UpdateProductMaterialCostAndQuantity(ProductMaterial productMaterial)
        {
            ProductMaterial pMaterial = await Context.ProductMaterials.FirstAsync(p => p.Id == productMaterial.Id, ComponentDisposed);

            pMaterial.Cost = productMaterial.Cost;
            pMaterial.Quantity = productMaterial.Quantity;

            await Context.SaveChangesAsync();
        }

        public async Task UpdateProductFiring(ProductFiring productFiring)
        {
            ProductFiring pFiring =await  Context.ProductFirings.FirstAsync(p => p.Id == productFiring.Id, ComponentDisposed);

            pFiring.CostKwH = productFiring.CostKwH;

            await Context.SaveChangesAsync();
        }
    }
}
