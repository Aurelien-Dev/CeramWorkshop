using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps.MainDomain
{
    public static class ProductMaterialMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMaterial>().HasKey(pm => pm.Id);

            modelBuilder.Entity<ProductMaterial>().HasOne(pm => pm.Product).WithMany(p => p.ProductMaterial).HasForeignKey(pm => pm.IdProduct);
            modelBuilder.Entity<ProductMaterial>().HasOne(pm => pm.Material).WithMany(m => m.ProductMaterial).HasForeignKey(pm => pm.IdMaterial);

            modelBuilder.Entity<ProductMaterial>().Ignore(pm => pm.CalculatedCost);
        }
    }
}