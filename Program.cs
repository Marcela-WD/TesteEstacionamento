using Microsoft.EntityFrameworkCore;
using Estacionamento.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )                                              
);    

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var culture = new CultureInfo("pt-BR");
    options.DefaultRequestCulture = new RequestCulture(culture);
    options.SupportedCultures = new List<CultureInfo> { culture };
    options.SupportedUICultures = new List<CultureInfo> { culture };

    options.RequestCultureProviders.Clear();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Veiculos}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
