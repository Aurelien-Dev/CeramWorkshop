namespace Domain.Models
{
    public class Firing
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Duration { get; set; }
        public int TotalKwH { get; set; }
        public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();
    }
}