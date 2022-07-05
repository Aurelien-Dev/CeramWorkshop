using Client.Utils.Middlewares;
using Common.Utils.Singletons;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using MudBlazor.Services;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRepository();

builder.Services.AddMudServices();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp => { return (ServerAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>(); });

//Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.AccessDeniedPath = new PathString("/AccessDenied");
                    options.LoginPath = new PathString("/login");
                    options.LogoutPath = new PathString("/logou");
                    options.ForwardForbid = CookieAuthenticationDefaults.AuthenticationScheme;
                });
builder.Services.AddAuthorization();

//Cookie Policy
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
});

var app = builder.Build();

EnvironementSingleton.WebRootPath = app.Environment.WebRootPath;
EnvironementSingleton.ContentRootPath = app.Environment.ContentRootPath;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();

app.UseGlobalCustomMiddleware();

app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/Layout/_Host");

app.Run();
