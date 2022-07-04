using Client.Services;
using Client.Utils;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Client.Pages.Authentification
{
    public partial class Login : CustomLayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;
        [Inject] public IDataProtectionProvider dataProtectionProvider { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IWorkshopWorker worker { get; set; } = default!;

        public LoginInfo LoginInfo { get; set; } = new();

        MudForm form = new();
        bool success;
        string authError = string.Empty;

        private async Task Authenticate()
        {
            authError = string.Empty;
            await Task.Delay(5);



            StateHasChanged();
            await form.Validate();

            if (form.IsValid)
            {
                //Check login before open authentication
                Workshop? workshop = worker.WorkshopRepository.GetForLogin(LoginInfo.Email);

                if (workshop == null)
                {
                    authError = "Invalid email.";
                    return;
                }

                bool userAuth = ProtectedDataService.IsEqual(workshop.Salt, workshop.PasswordHash, LoginInfo.Password);

                if (!userAuth)
                {
                    authError = "Invalid email or password.";
                    return;
                }

                AuthenticateInformation loginInfo = new AuthenticateInformation();
                loginInfo.Workshop = workshop;
                loginInfo.Token = EncryptCookie(loginInfo.ClaimsPrincipal, dataProtectionProvider);

                authenticationprovider.SetAuthenticationState(Task.FromResult(new AuthenticationState(loginInfo.ClaimsPrincipal)));

                AuthenticationServiceSingleton.StartSession(loginInfo);

                _ = await JSRuntime.InvokeAsync<string>("setCookie", new object[] { ".AspNetCore.Cookies", loginInfo.Token, 1 });

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

    public class LoginInfo
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}