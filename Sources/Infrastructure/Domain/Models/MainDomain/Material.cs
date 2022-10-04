using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    public enum MaterialType { Email = 0, Argile = 1, Engobe = 2, Accessory = 3 }
    public enum MaterialUnite { Kg = 0, L = 1, Unit = 2 }

    public class Material
    {
        [CeramRequired]
        public int Id { get; set; }

        [CeramRequired]
        public string Reference { get; set; }

        [CeramRequired]
        public string Name { get; set; }
        public bool? IsHomeMade { get; set; } = false;
        public double Cost { get; set; }
        public string? Comment { get; set; }
        public string? Link { get; set; }
        public double Quantity { get; set; } = 1;
        public MaterialUnite UniteMesure { get; set; } = default;
        public MaterialType Type { get; set; } = default;


        public ICollection<ProductMaterial> ProductMaterial { get; set; } = new List<ProductMaterial>();

        public Material() { }

        public Material(string reference, string name)
        {
            Reference = reference;
            Name = name;
        }
    }
}