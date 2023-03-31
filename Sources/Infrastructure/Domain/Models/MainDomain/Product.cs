using Domain.CustomDataAnotation;
using Domain.Models.WorkshopDomaine;

namespace Domain.Models.MainDomain
{
    /// <summary>
    /// Represents the status of a product.
    /// </summary>
    public enum ProductStatus
    {
        None = 0,
        Test = 1,
        Abandoned = 2,
        Production = 3,
        Discontinued = 4
    }

    public class Product
    {
        /// <summary>
        /// Gets or sets the ID of the product.
        /// </summary>
        [CeramRequired]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the workshop where the product is made.
        /// </summary>
        [CeramRequired]
        public int IdWorkshop { get; set; }

        /// <summary>
        /// Gets or sets the reference code of the product.
        /// </summary>
        [CeramRequired]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        [CeramRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the height of the product.
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Gets or sets the top diameter of the product.
        /// </summary>
        public double? TopDiameter { get; set; }

        /// <summary>
        /// Gets or sets the bottom diameter of the product.
        /// </summary>
        public double? BottomDiameter { get; set; }

        /// <summary>
        /// Gets or sets the shrinking coefficient of the product.
        /// </summary>
        public double? ShrinkingCoeficient { get; set; }

        /// <summary>
        /// Gets or sets the design instruction of the product.
        /// </summary>
        public string? DesignInstruction { get; set; }

        /// <summary>
        /// Gets or sets the glazing instruction of the product.
        /// </summary>
        public string? GlazingInstruction { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Gets or sets the status of the product.
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the image instructions associated with the product.
        /// </summary>
        public ICollection<ImageInstruction> ImageInstructions { get; set; } = new List<ImageInstruction>();

        /// <summary>
        /// Gets or sets the materials associated with the product.
        /// </summary>
        public ICollection<ProductMaterial> ProductMaterial { get; set; } = new List<ProductMaterial>();

        /// <summary>
        /// Gets or sets the firings associated with the product.
        /// </summary>
        public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();

        /// <summary>
        /// Gets or sets the workshop where the product is made.
        /// </summary>
        public Workshop Workshop { get; set; } = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class with the specified name and reference.
        /// </summary>
        public Product(string name, string reference)
        {
            Name = name;
            Reference = reference;
        }
    }
}