using Client.Services;
using Client.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Client.Pages.Authentification
{
    [Authorize]
    public partial class Logout : CustomLayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            authenticationprovider.SetAuthenticationState(Task.FromResult(new AuthenticationState(claimsPrincipal)));

            _ = await JSRuntime.InvokeAsync<string>("eraseCookie", new object[] { ".AspNetCore.Cookies" });

            AuthenticationManager.ClearSession();
            NavigationManager.NavigateTo("/", true);
        }
    }
}