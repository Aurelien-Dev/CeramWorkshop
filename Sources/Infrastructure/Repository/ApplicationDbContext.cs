using Common.Utils.Singletons;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Maps;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Firing> Firings { get; set; }
        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<ProductFiring> ProductFirings { get; set; }
        public DbSet<ProductAccessory> ProductAccessories { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(EnvironementSingleton.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProductMap.Build(modelBuilder);
            ImageInstructionMap.Build(modelBuilder);
            MaterialMap.Build(modelBuilder);
            FiringMap.Build(modelBuilder);
            AccessoryMap.Build(modelBuilder);
            ProductMaterialMap.Build(modelBuilder);
            ProductFiringMap.Build(modelBuilder);
            ProductAccessoryMap.Build(modelBuilder);
        }
    }
}