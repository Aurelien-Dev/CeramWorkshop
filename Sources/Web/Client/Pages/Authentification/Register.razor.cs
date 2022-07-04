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
    public partial class Register : CustomLayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;
        [Inject] public IDataProtectionProvider dataProtectionProvider { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IWorkshopWorker worker { get; set; } = default!;

        RegisterInfo registerInfo = new();

        MudForm form = new();
        bool success;
        string registerError = string.Empty;

        private async Task RegisterWorkshop()
        {
            registerError = string.Empty;
            await Task.Delay(5);

            StateHasChanged();
            await form.Validate();

            if (form.IsValid)
            {
                if(worker.WorkshopRepository.CheckIfEmailExists(registerInfo.Email))
                {
                    registerError = "Email already in use.";
                    return;
                }

                var WorkshopSalt = ProtectedDataService.GetSalt();
                var WorkshopPasswordHash = ProtectedDataService.HashPassword(registerInfo.Password, WorkshopSalt);

                Workshop workshopDetail = new(registerInfo.Name, null, registerInfo.Email, registerInfo.UserName, WorkshopPasswordHash, WorkshopSalt);


                await worker.WorkshopRepository.Add(workshopDetail);
                worker.Completed();

                AuthenticateInformation loginInfo = new AuthenticateInformation();
                loginInfo.Workshop = workshopDetail;
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
    }
}