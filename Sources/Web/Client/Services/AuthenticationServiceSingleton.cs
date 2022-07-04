using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Client.Services
{
    public sealed class AuthenticationServiceSingleton
    {
        public AuthenticationServiceSingleton Instance { get; } = new AuthenticationServiceSingleton();

        static AuthenticationServiceSingleton() { }
        private AuthenticationServiceSingleton() { }


        public static LoginInfo LoginInfo { get; set; } = new();

        internal static void ClearSession()
        {
            LoginInfo = new();
        }

        internal static void StartSession(LoginInfo loginInfo)
        {
            LoginInfo = loginInfo;
        }
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
        public string DiagPortalToken { get; internal set; }
    }
}