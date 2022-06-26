namespace Domain.Models
{
    public enum MaterialType
    {
        Email = 0,
        Argile = 1,
        Engobe = 2,
    }
    public class Material
    {
        public int Id { get; set; }
        public string Reference { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsCommercial { get; set; } = default!;
        public double? Cost { get; set; }
        public string? Comment { get; set; }
        public string? Link { get; set; }
        public MaterialType Type { get; set; } = default;

        public ICollection<ProductMaterial> ProductMaterial { get; set; }
    }
}