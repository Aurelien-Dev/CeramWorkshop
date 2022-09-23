using Client;
using Client.Utils.Logger;
using Client.Utils.Middlewares;
using Common.Utils.Singletons;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//Localization
var supportedCultures = new List<CultureInfo> { new CultureInfo("fr"), new CultureInfo("en") };
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

//Logging services
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddMyCustomLogger();

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

using (IServiceScope serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    //Apply last Entity Framework migration
    ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    context.Database.SetCommandTimeout(2400);
    context.Database.Migrate();
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