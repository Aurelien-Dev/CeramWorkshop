using Domain.Interfaces;
using Domain.Models;

namespace Repository.Repositories
{
    public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(ApplicationDbContext context) : base(context)
        {


        }

        public void AddAndLinkMaterial(Material material, Product product)
        {
            _context.Materials.Add(material);
            _context.SaveChanges();

            _context.ProductMaterials.Add(new ProductMaterial() { IdMaterial = material.Id, IdProduct = product.Id });
            _context.SaveChanges();
        }
    }
}
