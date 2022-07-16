using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    public class Firing
    {
        [CeramRequired]
        public int Id { get; set; }

        [CeramRequired]
        public string Name { get; set; } = string.Empty;
        
        public int Duration { get; set; }
        
        public int TotalKwH { get; set; }
        
        public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();


    }
}