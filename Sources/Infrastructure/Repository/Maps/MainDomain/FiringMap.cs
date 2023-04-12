using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps.MainDomain
{
    public static class FiringMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firing>().HasKey(p => p.Id);
        }
    }
}