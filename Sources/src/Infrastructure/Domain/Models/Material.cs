using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Reference { get; set; } = default!;
        public bool IsCommercial { get; set; } = default!;
        public double? Cost { get; set; }
        public string? Comment { get; set; }
        public string? Link { get; set; }
        public int Type { get; set; } = default;

        public ICollection<ProductMaterial> ProductMaterial { get; set; }
    }
}