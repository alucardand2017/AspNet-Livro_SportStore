using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SportsStoreConnection");

builder.Services.AddDbContext<StoreDbContext>(opt=> {
    opt.UseSqlServer(connectionString);
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

 app.UseStaticFiles();
 app.MapControllerRoute("pagination",
 "Products/Page{productPage}",
 new { Controller = "Home", action = "Index" });
 app.MapDefaultControllerRoute();

 
 SeedData.EnsurePopulated(app);
 app.Run();
