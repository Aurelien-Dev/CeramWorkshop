using System.ComponentModel.DataAnnotations;

namespace Domain.Models.MainDomain
{
    public class Accessory
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public double Cost { get; set; }
        
        public ICollection<ProductAccessory> ProductAccessory { get; set; } = new List<ProductAccessory>();


    }
}