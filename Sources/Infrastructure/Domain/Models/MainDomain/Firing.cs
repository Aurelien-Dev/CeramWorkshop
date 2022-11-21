using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    public class Firing
    {
        [CeramRequired]
        public int Id { get; set; }

        [CeramRequired]
        public string Name { get; set; } = string.Empty;
        
        public double Duration { get; set; }

        public double TotalKwH { get; set; }

        public double CostKwH { get; set; }

        public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();


    }
}