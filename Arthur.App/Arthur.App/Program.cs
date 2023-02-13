using Arthur.App.Interface;
using Arthur.App.Provider;
using Arthur.App.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IEvenProvider, EvenProvider>();
builder.Services.AddSingleton<IPrimeProvider, PrimeProvider>();
builder.Services.AddScoped<IStatsProvider, StatsProvider>();
builder.Services.AddScoped<IStatsRepository, StatsRepository>();
builder.Services.AddDbContext<ArthurDbContext>(builder => builder.UseSqlite("FileName=sqlLiteDb.db"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
