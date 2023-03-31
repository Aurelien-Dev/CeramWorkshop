using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    /// <summary>
    /// Represents the type of a material.
    /// </summary>
    public enum MaterialType
    {
        Email = 0,
        Argile = 1,
        Engobe = 2,
        Accessory = 3
    }

    /// <summary>
    /// Represents the unit of measurement for a material.
    /// </summary>
    public enum MaterialUnite
    {
        Kg = 0,
        L = 1,
        Unit = 2
    }

    /// <summary>
    /// Represents a material used in the production process.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Gets or sets the ID of the material.
        /// </summary>
        [CeramRequired]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reference code of the material.
        /// </summary>
        [CeramRequired]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the name of the material.
        /// </summary>
        [CeramRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the material is homemade.
        /// </summary>
        public bool? IsHomeMade { get; set; } = false;

        /// <summary>
        /// Gets or sets the cost of the material.
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets a comment associated with the material.
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets a link associated with the material.
        /// </summary>
        public string? Link { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the material.
        /// </summary>
        public double Quantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the unit of measurement for the material.
        /// </summary>
        public MaterialUnite UniteMesure { get; set; } = default;

        /// <summary>
        /// Gets or sets the type of material.
        /// </summary>
        public MaterialType Type { get; set; } = default;

        /// <summary>
        /// Gets or sets the products associated with the material.
        /// </summary>
        public ICollection<ProductMaterial> ProductMaterial { get; set; } = new List<ProductMaterial>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Material"/> class.
        /// </summary>
        public Material() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Material"/> class.
        /// </summary>
        public Material(string reference, string name)
        {
            Reference = reference;
            Name = name;
        }

        /// <summary>
        /// Gets the quantity of the material in unified units (e.g. grams or liters).
        /// </summary>
        public double UnifiedQuantity
        {
            get
            {
                switch (UniteMesure)
                {
                    case MaterialUnite.Kg:
                        return Quantity * 1000;
                    case MaterialUnite.L:
                        return Quantity * 1000;
                    case MaterialUnite.Unit:
                        return Quantity;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
    }
}