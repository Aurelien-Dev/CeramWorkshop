﻿using Client.Services.Authentication;
using Domain.InterfacesWorker;
using Domain.Models.WorkshopDomaine;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace Client.Services
{
    public sealed class AuthenticationService
    {
        private static IHostEnvironmentAuthenticationStateProvider Authenticationprovider {get;set;}
        private static IDataProtectionProvider DataProtectionProvider { get; set; }
        private static IWorkshopWorker Worker { get; set; }
        private static SessionInfo CurrentSession { get; set; }
        private static IJSRuntime JSRuntime { get; set; }

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
            JSRuntime = jSRuntime;
        }

        public static bool AuthanticateInitialized { get; set; } = false;

        internal static void ClearSession()
        {
        }

        public static async Task<string> StartSession(string Email, string Password)
        {
            string authError = null;
            //Check login before open authentication
            Workshop? workshop = Worker.WorkshopRepository.GetForLogin(Email);

            if (workshop == null)
                return "Invalid email.";

            bool userAuth = ProtectedDataService.IsEqual(workshop.Salt, workshop.PasswordHash, Password);

            if (!userAuth)
                return "Invalid email or password.";

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(CreateClaims(workshop), CookieAuthenticationDefaults.AuthenticationScheme));

            CurrentSession.Workshop = workshop;
            CurrentSession.ClaimsPrincipal = claimsPrincipal;
            CurrentSession.Token = EncryptCookie(CurrentSession.ClaimsPrincipal, DataProtectionProvider);

            Authenticationprovider.SetAuthenticationState(Task.FromResult(new AuthenticationState(CurrentSession.ClaimsPrincipal)));

            _ = await JSRuntime.InvokeAsync<string>("setCookie", new object[] { ".AspNetCore.Cookies", CurrentSession.Token, 1 });

            AuthanticateInitialized = true;
            return authError;
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