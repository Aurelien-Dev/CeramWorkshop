using Domain.CustomDataAnotation;
using Microsoft.AspNetCore.Localization;
using System.ComponentModel.DataAnnotations;

namespace Client.ViewModels
{
    public class RegisterInfo
    {
        [CeramRequired]
        public string Name { get; set; } = string.Empty;

        [CeramRequired]
        public string UserName { get; set; } = string.Empty;

        [CeramRequired]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [CeramRequired]
        public string Password { get; set; } = string.Empty;

        [CeramRequired]
        [CeramCompare(nameof(Password))]
        public string Password2 { get; set; } = string.Empty;

        public RequestCulture Culture { get; set; }
    }
}