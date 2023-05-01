using Domain.Models.MainDomain;
using Domain.Models.WorkshopDomaine;
using Microsoft.EntityFrameworkCore;
using Repository.Maps.MainDomain;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Material> Materials { get; set; } = default!;
        public DbSet<Firing> Firings { get; set; } = default!;

        public DbSet<ImageInstruction> ImageInstruction { get; set; } = default!;

        public DbSet<ProductMaterial> ProductMaterials { get; set; } = default!;
        public DbSet<ProductFiring> ProductFirings { get; set; } = default!;

        public DbSet<Workshop> Workshops { get; set; } = default!;
        public DbSet<WorkshopParameter> WorkshopParameters { get; set; } = default!;

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProductMap.Build(modelBuilder);
            ImageInstructionMap.Build(modelBuilder);
            MaterialMap.Build(modelBuilder);
            FiringMap.Build(modelBuilder);
            ProductMaterialMap.Build(modelBuilder);
            ProductFiringMap.Build(modelBuilder);
        }
    }
}