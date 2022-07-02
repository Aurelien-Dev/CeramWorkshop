using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductMaterial
    {
        [Required]
        public int Id { get; set; }
        public int IdMaterial { get; set; } = default!;
        public int IdProduct { get; set; } = default!;
        [Required]
        public double Quantity { get; set; } = default!;
        public double Cost { get; set; } = default!;


        public Material Material { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}