using Domain.Models.MainDomain;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.WorkshopDomaine
{
    public class Workshop
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName{ get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Logo { get; set; }


        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<WorkshopParameter> WorkshopParameters { get; set; } = new List<WorkshopParameter>();


        public Workshop()
        {
            Name = string.Empty;
            Logo = string.Empty;
            Email = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
        }

        public Workshop(string name, string? logo, string email, string userName, string password)
        {
            Name = name;
            Logo = logo;
            Email = email;
            UserName = userName;
            Password = password;
        }
    }
}