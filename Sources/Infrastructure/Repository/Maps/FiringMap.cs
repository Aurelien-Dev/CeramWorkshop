using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public class FiringMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firing>().HasKey(p => p.Id);
        }
    }
}