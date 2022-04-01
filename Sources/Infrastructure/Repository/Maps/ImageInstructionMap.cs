using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public class ImageInstructionMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageInstruction>().HasKey(p => new { p.Id });

            modelBuilder.Entity<ImageInstruction>()
                .HasOne(s => s.ProductAssociate)
                .WithMany(g => g.ProductImageInstruction)
                .HasForeignKey(s => s.IdProduct);
        }
    }
}