using Client.Services;
using Client.Utils;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;
using MudBlazor;
using System.Security.Claims;

namespace Client.Pages.Authentification
{
    public partial class Login : CustomLayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;
        [Inject] public IDataProtectionProvider dataProtectionProvider { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        public Workshop WorkshopDetail { get; set; } = new();

        MudForm form = new();
        bool success;
        string[] errors = Array.Empty<string>();


        private async Task Authenticate()
        {
            StateHasChanged();
            await form.Validate();

            if (form.IsValid)
            {

                //Check login before open authentication


                LoginInfo loginInfo = new LoginInfo();
                loginInfo.Workshop = new Workshop("Atelier Cr�mazie", null, "", "", "");
                loginInfo.DiagPortalToken = EncryptCookie(loginInfo.ClaimsPrincipal, dataProtectionProvider);

                authenticationprovider.SetAuthenticationState(Task.FromResult(new AuthenticationState(loginInfo.ClaimsPrincipal)));

                AuthenticationServiceSingleton.StartSession(loginInfo);

                _ = await JSRuntime.InvokeAsync<string>("setCookie", new object[] { ".AspNetCore.Cookies", loginInfo.DiagPortalToken, 1 });

                NavigationManager.NavigateTo("/", forceLoad: false);
            }
        }


        private static string EncryptCookie(ClaimsPrincipal claimsPrincipal, IDataProtectionProvider dataProtectionProvider)
        {
            AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, CookieAuthenticationDefaults.AuthenticationScheme);
            IDataProtector dataProtector = dataProtectionProvider.CreateProtector("Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware", "Cookies", "v2");
            var ticketDataFormat = new TicketDataFormat(dataProtector);

            string encyptedCookie = ticketDataFormat.Protect(ticket);
            return encyptedCookie;
        }


    }
}