using Client.Services.Authentication;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Localization;
using Microsoft.JSInterop;
using System.Security.Claims;
using Utils.Singletons;

namespace Client.Services
{
    public class AuthenticationService
    {
        private IHostEnvironmentAuthenticationStateProvider Authenticationprovider { get; set; }
        private IDataProtectionProvider DataProtectionProvider { get; set; }
        private IWorkshopWorker Worker { get; set; }
        private SessionInfo CurrentSession { get; set; }
        private IJSRuntime JsRuntime { get; set; }

        private readonly string _domain = ".atelier-cremazie.com";

        public AuthenticationService(IHostEnvironmentAuthenticationStateProvider authenticationprovider,
                                     IDataProtectionProvider dataProtectionProvider,
                                     IWorkshopWorker worker,
                                     SessionInfo sessionInfo,
                                     IJSRuntime jSRuntime)
        {
            Authenticationprovider = authenticationprovider;
            DataProtectionProvider = dataProtectionProvider;
            Worker = worker;
            CurrentSession = sessionInfo;
            JsRuntime = jSRuntime;


            if (EnvironementSingleton.IsInDev())
                _domain = string.Empty;
        }

        public bool AuthanticateInitialized => CurrentSession.Workshop != null;

        public async Task StopSession()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            Authenticationprovider.SetAuthenticationState(Task.FromResult(new AuthenticationState(claimsPrincipal)));

            _ = await JsRuntime.InvokeAsync<string>("eraseCookie", new object[] { ".AspNetCore.Cookies", _domain });
        }

        /// <summary>
        /// Start a session, and to create the cookies necessary for the good functioning
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>Return an boolean success and a error message for client</returns>
        public async Task<(bool, string)> StartSession(string email, string password)
        {
            (Workshop? workshop, bool emailExist) = await Worker.WorkshopRepository.GetWorkshopInformationForLogin(email);

            if (!emailExist)
                return (false, "Invalid email or password.");

            bool isWritePawwsord = ProtectedDataService.IsEqual(workshop.Salt, workshop.PasswordHash, password);

            if (!isWritePawwsord)
                return (false, "Invalid email or password.");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(CreateClaims(workshop), CookieAuthenticationDefaults.AuthenticationScheme));

            CurrentSession.Workshop = workshop;
            CurrentSession.ClaimsPrincipal = claimsPrincipal;
            CurrentSession.Token = EncryptCookie(CurrentSession.ClaimsPrincipal, DataProtectionProvider);

            Authenticationprovider.SetAuthenticationState(Task.FromResult(new AuthenticationState(CurrentSession.ClaimsPrincipal)));

            _ = await JsRuntime.InvokeAsync<string>("setCookie", new object[] { ".AspNetCore.Cookies", CurrentSession.Token, 1, _domain });
            _ = await JsRuntime.InvokeAsync<string>("setCookie", new object[] { CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(workshop.Culture)), 365 });

            System.Globalization.CultureInfo.CurrentCulture = new RequestCulture(workshop.Culture).Culture;

            return (true, string.Empty);
        }

        private static string EncryptCookie(ClaimsPrincipal claimsPrincipal, IDataProtectionProvider dataProtectionProvider)
        {
            AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, CookieAuthenticationDefaults.AuthenticationScheme);
            IDataProtector dataProtector = dataProtectionProvider.CreateProtector("Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware", "Cookies", "v2");
            var ticketDataFormat = new TicketDataFormat(dataProtector);

            string encyptedCookie = ticketDataFormat.Protect(ticket);
            return encyptedCookie;
        }

        private static List<Claim> CreateClaims(Workshop? workshop)
        {
            return new List<Claim>
                {
                    new Claim("IdWorkshop", workshop.Id.ToString()),
                    new Claim("NameWorkshop", workshop.Name),
                };
        }
    }
}