using System.ComponentModel.DataAnnotations;

namespace Domain.Models.MainDomain
{
    public enum MaterialType { Email = 0, Argile = 1, Engobe = 2, }

    public class Material
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public string Name { get; set; }

        public bool? IsHomeMade { get; set; }

        public double? Cost { get; set; }

        public string? Comment { get; set; }

        public string? Link { get; set; }

        public MaterialType Type { get; set; } = default;


        public ICollection<ProductMaterial> ProductMaterial { get; set; } = new List<ProductMaterial>();

        public Material()
        {
            Reference = string.Empty;
            Name = string.Empty;
        }

        public Material(string reference, string name)
        {
            Reference = reference;
            Name = name;
        }
    }
}