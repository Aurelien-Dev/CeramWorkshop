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
using System.Security.Claims;

namespace Client.Pages.Authentification
{
    public partial class Register : CustomLayoutComponentBase
    {
        [Inject] public IHostEnvironmentAuthenticationStateProvider authenticationprovider { get; set; } = default!;
        [Inject] public IDataProtectionProvider dataProtectionProvider { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IWorkshopWorker worker { get; set; } = default!;

        public Workshop WorkshopDetail { get; set; } = new();

        MudForm form = new();
        bool success;
        string[] errors = Array.Empty<string>();


        private async Task RegisterWorkshop()
        {
            StateHasChanged();
            await form.Validate();

            if (form.IsValid)
            {
                await worker.WorkshopRepository.Add(WorkshopDetail);
                worker.Completed();

                LoginInfo loginInfo = new LoginInfo();
                loginInfo.Workshop = WorkshopDetail;
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