using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public static class ProductAccessoryMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAccessory>().HasKey(p => new { p.IdProduct, p.IdAccessory});

            modelBuilder.Entity<ProductAccessory>().HasKey(pf => new { pf.IdProduct, pf.IdAccessory});
            modelBuilder.Entity<ProductAccessory>().HasOne(pf => pf.Product).WithMany(f => f.ProductAccessory).HasForeignKey(pf => pf.IdProduct);
            modelBuilder.Entity<ProductAccessory>().HasOne(pf => pf.Accessory).WithMany(f => f.ProductAccessory).HasForeignKey(f => f.IdAccessory);
        }
    }
}