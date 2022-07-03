using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public static class ProductMaterialMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMaterial>().HasKey(p => p.Id);

            modelBuilder.Entity<ProductMaterial>().HasOne(pm => pm.Product).WithMany(p => p.ProductMaterial).HasForeignKey(pm => pm.IdProduct);
            modelBuilder.Entity<ProductMaterial>().HasOne(pm => pm.Material).WithMany(m => m.ProductMaterial).HasForeignKey(pm => pm.IdMaterial);
        }
    }
}