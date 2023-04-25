using Client.Utils;
using Domain.CustomDataAnotation;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using Client.Utils.ComponentBase;

namespace Client.Pages.Authentification
{
    public partial class Login : CustomComponentBase
    {
        private LoginInfo LoginInfo { get; set; } = new();
        private bool LoginInProgress { get; set; } = false;

        private string _authError = string.Empty;

        private async Task Authenticate(EditContext? context)
        {
            _authError = string.Empty;
            if (context != null && context.Validate())
            {
                LoginInProgress = true;
                StateHasChanged();
                await Task.Delay(5);

                (_, _authError) = await AuthenticationManager.StartSession(LoginInfo.Email, LoginInfo.Password);

                StateHasChanged();
            }

            LoginInProgress = false;
        }
    }

    public class LoginInfo
    {
        [CeramRequired] [EmailAddress] public string Email { get; set; } = string.Empty;

        [CeramRequired] public string Password { get; set; } = string.Empty;
    }
}