using Client.Services;
using Client.Services.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using MudBlazor;
using MudBlazor.Services;

namespace Client
{
    public static class AppBuilder
    {
        public static void AddClientServices(this IServiceCollection services)
        {
            services.AddSingleton<SessionInfo>();

            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
            services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp => { return (ServerAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>(); });
            
            services.AddScoped<AuthenticationService>();

            services.AddScoped<ILogger>(provider => { return provider.GetRequiredService<ILoggerFactory>().CreateLogger("CeramWorkshop - "); });

            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 4000;
                config.SnackbarConfiguration.HideTransitionDuration = 250;
                config.SnackbarConfiguration.ShowTransitionDuration = 250;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Text;
            });
        }
    }
}
