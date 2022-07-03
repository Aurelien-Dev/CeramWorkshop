using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public static class MaterialMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().HasKey(p => p.Id);
        }
    }
}