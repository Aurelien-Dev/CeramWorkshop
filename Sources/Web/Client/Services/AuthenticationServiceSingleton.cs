using Domain.Models.WorkshopDomaine;
using System.Security.Claims;

namespace Client.Services
{
    public sealed class AuthenticationServiceSingleton
    {
        public AuthenticationServiceSingleton Instance { get; } = new AuthenticationServiceSingleton();

        static AuthenticationServiceSingleton() { }
        private AuthenticationServiceSingleton() { }


        public static AuthenticateInformation LoginInfo { get; set; } = new();
        public static bool AuthanticateInitialized { get; set; } = false;

        internal static void ClearSession()
        {
            LoginInfo = new();
        }

        internal static void StartSession(AuthenticateInformation loginInfo)
        {
            LoginInfo = loginInfo;
            AuthanticateInitialized = true;
        }
    }

    public class AuthenticateInformation
    {
        public AuthenticateInformation() { }

        public ClaimsPrincipal ClaimsPrincipal { get; internal set; }
        public Workshop? Workshop { get; internal set; }
        public string? Token { get; internal set; }
        public bool IsAuthenticate { get; set; } = false;
    }
}