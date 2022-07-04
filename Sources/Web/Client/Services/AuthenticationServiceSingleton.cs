using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Client.Services
{
    public sealed class AuthenticationServiceSingleton
    {
        public AuthenticationServiceSingleton Instance { get; } = new AuthenticationServiceSingleton();

        static AuthenticationServiceSingleton() { }
        private AuthenticationServiceSingleton() { }


        public static bool Login(IHostEnvironmentAuthenticationStateProvider serverAuthState)
        {
            LoginInfo.Workshop = new Workshop("Atelier Crémazie", null, "", "", "");



            LoginInfo loginInfo = new LoginInfo();
            serverAuthState.SetAuthenticationState(Task.FromResult(new AuthenticationState(loginInfo.ClaimsPrincipal)));

            return true;
        }

        public static bool Logout(IHostEnvironmentAuthenticationStateProvider serverAuthState)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            serverAuthState.SetAuthenticationState(Task.FromResult(new AuthenticationState(claimsPrincipal)));

            LoginInfo.Workshop = new();

            return true;
        }


        public static LoginInfo LoginInfo { get; set; } = new();

    }

    public class LoginInfo
    {
        public LoginInfo()
        {
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "dd")
                };
            ClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        }

        public ClaimsPrincipal ClaimsPrincipal { get; internal set; }
        public Workshop Workshop { get; internal set; }
        public bool IsAuthenticate { get; set; } = false;
    }
}