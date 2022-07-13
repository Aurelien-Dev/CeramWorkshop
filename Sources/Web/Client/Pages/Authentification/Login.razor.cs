using Client.Services;
using Client.Utils;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace Client.Pages.Authentification
{
    public partial class Login : CustomLayoutComponentBase
    {
        public LoginInfo LoginInfo { get; set; } = new();
        public bool LoginInProgress { get; set; } = false;

        MudForm form = new();
        string authError = string.Empty;

        private async Task Authenticate()
        {
            authError = string.Empty;
            await form.Validate();

            if (form.IsValid)
            {
                LoginInProgress = true;
                StateHasChanged();
                await Task.Delay(5);

                authError = await AuthenticationManager.StartSession(LoginInfo.Email, LoginInfo.Password);

                StateHasChanged();
            }
        }
    }

    public class LoginInfo
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}