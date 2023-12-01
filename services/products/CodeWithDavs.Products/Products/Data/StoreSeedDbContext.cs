using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Products.Models;
using System.Text.Json;

namespace Products.Data
{
    public class StoreSeedDbContext
    {
        public static async Task SeedDataAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                // Seed Categories
                if (!context.Categories.Any())
                {
                    var categoryData = File.ReadAllText("./Data/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                    foreach (var item in categories)
                    {
                        Guid uid = Guid.NewGuid();
                        item.Id = uid.ToString();
                        item.CreatedAt = DateTime.Now;
                        context.Categories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreSeedDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
