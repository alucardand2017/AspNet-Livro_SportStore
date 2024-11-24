using Microsoft.EntityFrameworkCore;

 namespace SportsStore.Models {
 public static class SeedData {
    public static void EnsurePopulated(IApplicationBuilder app) 
    {
    StoreDbContext context = app
    .ApplicationServices.CreateScope()
    .ServiceProvider.GetRequiredService<StoreDbContext>();
        if (context.Database.GetPendingMigrations().Any()) 
            {
            context.Database.Migrate();
            }
        if (!context.Products.Any()) 
            {
            context.Products.AddRange
                (
                new Product {Name = "Kayak", Description = "A boat for one person", Category = "Watersports", Price = 275},
                new Product {Name = "Lifejacket",Description = "Protective and fashionable",Category = "Watersports", Price = 48.95m},
                new Product {Name = "Soccer Ball",Description = "FIFA-approved size and weight",Category = "Soccer", Price = 19.50m},
                new Product {Name = "Corner Flags",Description = "Give your playing field a professional touch",Category = "Soccer", Price = 34.95m},
                new Product {Name = "Bola",Description = "Uma cola azul da cor do mar",Category = "Soccer", Price = 350m},
                new Product {Name = "cachorro",Description = "Cachorro salsicha da cor do caramelo",Category = "Pet", Price = 15m},
                new Product {Name = "gato",Description = "gato gatuno gaiato",Category = "Pet", Price = 150m},
                new Product {Name = "piriquito",Description = "Não voa, não canta, não dança",Category = "Pet", Price = 300m}
                );
            context.SaveChanges();
            }
        }
    }
 }