using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    /// <summary>
    /// Represents the association between a product and a material used in its production.
    /// </summary>
    public class ProductMaterial
    {
        /// <summary>
        /// Gets or sets the ID of the association.
        /// </summary>
        [CeramRequired]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the material used in the association.
        /// </summary>
        public int IdMaterial { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product used in the association.
        /// </summary>
        public int IdProduct { get; set; }

        /// <summary>
        /// Gets or sets the quantity of material used in the association.
        /// </summary>
        [CeramRequired]
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the cost of the material used in the association.
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets the material used in the association.
        /// </summary>
        public Material Material { get; set; } = default!;

        /// <summary>
        /// Gets or sets the product used in the association.
        /// </summary>
        public Product Product { get; set; } = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMaterial"/> class with the specified material, product, quantity, and cost.
        /// </summary>
        /// <param name="idMaterial">The ID of the material used in the association.</param>
        /// <param name="idProduct">The ID of the product used in the association.</param>
        /// <param name="quantity">The quantity of material used in the association.</param>
        /// <param name="cost">The cost of the material used in the association.</param>
        public ProductMaterial(int idMaterial, int idProduct, double quantity, double cost)
        {
            IdMaterial = idMaterial;
            IdProduct = idProduct;
            Quantity = quantity;
            Cost = cost;
        }

        /// <summary>
        /// Gets the calculated cost of the material used in the association, based on the material's unified quantity and the association's quantity and cost.
        /// </summary>
        public double CalculatedCost { get { return Cost / Material.UnifiedQuantity * Quantity; } }
    }
}