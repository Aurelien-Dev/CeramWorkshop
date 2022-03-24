namespace Domain.Models
{
    public class ProductFiring
    {
        public int IdFiring { get; set; } = default!;
        public int IdProduct { get; set; } = default!;

        public int TotalKwH { get; set; }
        public int CostKwH { get; set; }

        public Firing Firing { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}