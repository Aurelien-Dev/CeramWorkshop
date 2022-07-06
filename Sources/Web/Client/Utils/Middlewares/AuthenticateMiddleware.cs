using Client.Services;
using Repository;
using System.Security.Claims;

namespace Client.Utils.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticateMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticateMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, ApplicationDbContext dbContext)
        {
            bool statusCheck = true;
            ClaimsPrincipal claimsP = httpContext.User;
            if (claimsP.Identity.IsAuthenticated && !AuthenticationServiceSingleton.AuthanticateInitialized)
            {
                int idWorkshop = Convert.ToInt32(claimsP.Claims.FirstOrDefault(d => d.Type == "IdWorkshop").Value);

                AuthenticationServiceSingleton.StartSession(new AuthenticateInformation()
                {
                    ClaimsPrincipal = httpContext.User,
                    Workshop = dbContext.Workshops.FirstOrDefault(w => w.Id == idWorkshop)
                });
            }
            else
            {
                if (statusCheck && httpContext.Request.Path.Value == "/")
                    Task.Run(() => httpContext.Response.Redirect("/login"));
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticateMiddleware>();
        }
    }
}
