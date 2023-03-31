using Domain.Models.WorkshopDomaine;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public static class WorkshopMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workshop>().HasKey(p => p.Id);
        }
    }
}