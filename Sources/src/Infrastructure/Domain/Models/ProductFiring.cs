using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductFiring
    {
        public int IdFiring { get; set; } = default!;
        public int IdProduct { get; set; } = default!;


        public Firing Firing { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}