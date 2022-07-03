using Client.Services;
using Client.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Pages.Authentification
{
    public partial class Login : PageComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;


        private async Task Authenticate()
        {
            try
            {
                AuthenticationServiceSingleton.Login(authenticationprovider);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        
    }
}