using Client;
using Client.Utils.Middlewares;
using Common.Utils.Singletons;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Repository;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new List<CultureInfo> { new CultureInfo("fr-FR"), new CultureInfo("en-US") };

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("fr", "fr");
    options.SupportedUICultures = supportedCultures;
    options.SupportedCultures = supportedCultures;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRepository();
builder.Services.AddClientServices();


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

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();

app.UseMiddleware();

app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/Layout/_Host");

app.Run();