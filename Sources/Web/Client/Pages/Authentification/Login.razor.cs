using Client.Services;
using Client.Utils;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Client.Pages.Authentification
{
    public partial class Login : CustomLayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;

        public Workshop WorkshopDetail { get; set; } = new();

        MudForm form = new();
        bool success;
        string[] errors = Array.Empty<string>();


        private async Task Authenticate()
        {
            try
            {
                AuthenticationServiceSingleton.Login(authenticationprovider);

                NavigationManager.NavigateTo("/", forceLoad: false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}