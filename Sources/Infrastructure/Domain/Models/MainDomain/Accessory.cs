using Domain.CustomDataAnotation;

namespace Domain.Models.MainDomain
{
    public class Accessory
    {
        [CeramRequired]
        public int Id { get; set; }

        [CeramRequired]
        public string Name { get; set; } = string.Empty;
        
        public double Cost { get; set; }
        
        public ICollection<ProductAccessory> ProductAccessory { get; set; } = new List<ProductAccessory>();


    }
}