using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Firing
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        
        public int Duration { get; set; }
        
        public int TotalKwH { get; set; }
        
        public ICollection<ProductFiring> ProductFiring { get; set; } = new List<ProductFiring>();


    }
}