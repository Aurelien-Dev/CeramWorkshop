using Client.Utils;
using Domain.CustomDataAnotation;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Client.Pages.Authentification
{
    public partial class Login : CustomLayoutComponentBase
    {
        public LoginInfo LoginInfo { get; set; } = new();
        public bool LoginInProgress { get; set; } = false;

        string authError = string.Empty;

        private async Task Authenticate(EditContext context)
        {
            authError = string.Empty;
            if (context != null && context.Validate())
            {
                LoginInProgress = true;
                StateHasChanged();
                await Task.Delay(5);

                (_, authError) = await AuthenticationManager.StartSession(LoginInfo.Email, LoginInfo.Password);

                StateHasChanged();
            }
            LoginInProgress = false;
        }
    }

    public class LoginInfo
    {
        [CeramRequired]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [CeramRequired]
        public string Password { get; set; } = string.Empty;
    }
}