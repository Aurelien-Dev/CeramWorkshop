using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    /// <summary>
    /// Represents the association between a product and a firing used in its production.
    /// </summary>
    public class ProductFiring
    {
        /// <summary>
        /// Gets or sets the ID of the association.
        /// </summary>
        [CeramRequired]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the firing used in the association.
        /// </summary>
        public int IdFiring { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product used in the association.
        /// </summary>
        public int IdProduct { get; set; }

        /// <summary>
        /// Gets or sets the total kWh used for the firing. By default, this value is taken from the linked firing,
        /// but it can be modified for this specific association.
        /// </summary>
        public double TotalKwH { get; set; }

        /// <summary>
        /// Gets or sets the cost per kWh for the firing. By default, this value is taken from the linked firing,
        /// but it can be modified for this specific association.
        /// </summary>
        public double CostKwH { get; set; }

        /// <summary>
        /// Gets or sets the number of products that can be placed in a single firing.
        /// </summary>
        public int NumberProducts { get; set; }

        /// <summary>
        /// Gets or sets the firing used in the association.
        /// </summary>
        public Firing Firing { get; set; } = default!;

        /// <summary>
        /// Gets or sets the product used in the association.
        /// </summary>
        public Product Product { get; set; } = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductFiring"/> class with the specified firing, product, 
        /// total kWh, cost per kWh, and number of products.
        /// </summary>
        /// <param name="idFiring">The ID of the firing used in the association.</param>
        /// <param name="idProduct">The ID of the product used in the association.</param>
        /// <param name="totalKwH">The total kWh used for the firing.</param>
        /// <param name="costKwH">The cost per kWh for the firing.</param>
        /// <param name="numberProducts">The number of products that can be placed in a single firing.</param>
        public ProductFiring(int idFiring, int idProduct, double totalKwH, double costKwH, int numberProducts)
        {
            IdFiring = idFiring;
            IdProduct = idProduct;
            TotalKwH = totalKwH;
            CostKwH = costKwH;
            NumberProducts = numberProducts;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductFiring"/> class with the specified firing, 
        /// product, total kWh, and cost per kWh, assuming only one product can be placed in the firing.
        /// </summary>
        /// <param name="idFiring">The ID of the firing used in the association.</param>
        /// <param name="idProduct">The ID of the product used in the association.</param>
        /// <param name="totalKwH">The total kWh used for the firing.</param>
        /// <param name="costKwH">The cost per kWh for the firing.</param
        public ProductFiring(int idFiring, int idProduct, double totalKwH, double costKwH)
        {
            IdFiring = idFiring;
            IdProduct = idProduct;
            TotalKwH = totalKwH;
            CostKwH = costKwH;
            NumberProducts = 1;
        }
    }
}