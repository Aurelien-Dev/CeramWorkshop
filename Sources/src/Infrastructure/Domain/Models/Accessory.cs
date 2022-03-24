namespace Domain.Models
{
    public class Accessory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public double Cost { get; set; }
        public ICollection<ProductAccessory> ProductAccessory { get; set; }
    }
}