using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Reference { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int? Height { get; set; }
        public int? TopDiameter{ get; set; }
        public int? BottomDiameter { get; set; }
        public string? DesignInstruction { get; set; }
        public int? Status { get; set; }
        public int MarginRate { get; set; }

        public ICollection<ProductMaterial> ProductMaterial { get; set; }
    }
}