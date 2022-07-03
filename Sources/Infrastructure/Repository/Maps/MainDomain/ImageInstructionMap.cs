using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public static class ImageInstructionMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageInstruction>().HasKey(p => new { p.Id });

            modelBuilder.Entity<ImageInstruction>()
                .HasOne(s => s.ProductAssociate)
                .WithMany(g => g.ImageInstructions)
                .HasForeignKey(s => s.IdProduct);
        }
    }
}