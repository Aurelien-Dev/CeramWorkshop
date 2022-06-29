using Common.Utils.Singletons;
using MudBlazor.Services;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRepository();

builder.Services.AddMudServices();

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

app.MapBlazorHub();
app.MapFallbackToPage("/Layout/_Host");

app.Run();
