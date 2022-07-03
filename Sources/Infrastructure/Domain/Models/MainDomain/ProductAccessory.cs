using System.ComponentModel.DataAnnotations;

namespace Domain.Models.MainDomain
{
    public class ProductAccessory
    {
        [Required]
        public int IdAccessory { get; set; }

        [Required]
        public int IdProduct { get; set; }

        public double Cost { get; set; }

        public Accessory Accessory { get; set; } = default!;
        public Product Product { get; set; } = default!;

        public ProductAccessory(int idAccessory, int idProduct, double cost)
        {
            IdAccessory = idAccessory;
            IdProduct = idProduct;
            Cost = cost;
        }
    }
}