namespace Domain.Models
{
    public class ProductAccessory
    {
        public int IdAccessory { get; set; } = default!;
        public int IdProduct { get; set; } = default!;
        public double Cost { get; set; }

        public Accessory Accessory { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}