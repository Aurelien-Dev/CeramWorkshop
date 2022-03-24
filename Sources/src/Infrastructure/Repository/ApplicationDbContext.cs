using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Maps;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=ec2-52-18-116-67.eu-west-1.compute.amazonaws.com;Database=desbki1pskf9ed;Username=htsfosutwsnach;Password=e2b6c99a5cf7728e1ea17d3d98642be1559db77d8fa5f720635036de497653a7");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProductMap.Build(modelBuilder);
            MaterialMap.Build(modelBuilder);
            FiringMap.Build(modelBuilder);
            AccessoryMap.Build(modelBuilder);
            ProductMaterialMap.Build(modelBuilder);
            ProductFiringMap.Build(modelBuilder);
            ProductAccessoryMap.Build(modelBuilder);
        }
    }
}