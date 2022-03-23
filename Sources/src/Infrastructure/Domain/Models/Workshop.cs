using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Workshop
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string? WebSiteLink { get; set; }
    }
}