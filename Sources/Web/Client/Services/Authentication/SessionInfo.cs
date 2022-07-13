using Domain.Models.WorkshopDomaine;
using System.Security.Claims;

namespace Client.Services.Authentication
{
    public class SessionInfo
    {
        public Workshop? Workshop { get; set; }
        public bool IsAuthenticate { get; set; } = false;

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
        public string? Token { get; set; }
    }
}
