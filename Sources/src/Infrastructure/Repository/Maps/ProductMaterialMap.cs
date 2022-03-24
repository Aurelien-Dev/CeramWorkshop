using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public class ProductMaterialMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMaterial>().HasKey(p => new { p.IdProduct, p.IdMaterial });
        }
    }
}