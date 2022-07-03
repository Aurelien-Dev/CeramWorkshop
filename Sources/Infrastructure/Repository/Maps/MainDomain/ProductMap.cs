using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public static class ProductMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                        .HasOne(s => s.Workshop)
                        .WithMany(g => g.Products)
                        .HasForeignKey(s => s.IdWorkshop);
        }
    }
}