using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    public class ProductMaterial
    {
        [CeramRequired]
        public int Id { get; set; }

        public int IdMaterial { get; set; }

        public int IdProduct { get; set; }

        [CeramRequired]
        public double Quantity { get; set; }

        public double Cost { get; set; }

        public Material Material { get; set; } = default!;
        public Product Product { get; set; } = default!;


        public ProductMaterial(int idMaterial, int idProduct, double quantity, double cost)
        {
            IdMaterial = idMaterial;
            IdProduct = idProduct;
            Quantity = quantity;
            Cost = cost;
        }

        public double CalculatedCost { get { return Cost / Material.UnifiedQuantity * Quantity; } }
    }
}