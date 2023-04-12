using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps.MainDomain
{
    public static class MaterialMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().HasKey(p => p.Id);

            modelBuilder.Entity<Material>().Ignore(p => p.UnifiedQuantity);
        }
    }
}