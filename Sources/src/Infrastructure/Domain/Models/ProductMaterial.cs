namespace Domain.Models
{
    public class ProductMaterial
    {
        public int IdMaterial { get; set; } = default!;
        public int IdProduct { get; set; } = default!;
        public double Quantity { get; set; } = default!;
        public double Cost { get; set; } = default!;


        public Material Material { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}