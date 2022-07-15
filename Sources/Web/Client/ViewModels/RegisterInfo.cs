using Microsoft.AspNetCore.Localization;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class RegisterInfo
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare(nameof(Password))]
        public string Password2 { get; set; } = string.Empty;
        public RequestCulture Culture { get; set; }
    }
}
