using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public class MaterialMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().HasKey(p => p.Id);
        }
    }
}