using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Product
    {
        public Product() { }

        public Product(string name)
        {
            Name = name;
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Reference { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double? Height { get; set; }
        public double? TopDiameter { get; set; }
        public double? BottomDiameter { get; set; }
        public string? DesignInstruction { get; set; }
        public int? Status { get; set; }
        public int MarginRate { get; set; }

        public ICollection<ImageInstruction> ProductImageInstruction { get; set; } = new List<ImageInstruction>();
        public ICollection<ProductMaterial> ProductMaterial { get; set; } = new List<ProductMaterial>();
        public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();
        public ICollection<ProductAccessory> ProductAccessory { get; set; } = new List<ProductAccessory>();
    }
}