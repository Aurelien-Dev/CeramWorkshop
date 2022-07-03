using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductFiring
    {
        [Required]
        public int IdFiring { get; set; }

        [Required]
        public int IdProduct { get; set; }

        public int TotalKwH { get; set; }

        public int CostKwH { get; set; }

        public Firing Firing { get; set; } = default!;
        public Product Product { get; set; } = default!;

        public ProductFiring(int idFiring, int idProduct, int totalKwH, int costKwH)
        {
            IdFiring = idFiring;
            IdProduct = idProduct;                
            TotalKwH = totalKwH;
            CostKwH = costKwH;
        }
    }
}