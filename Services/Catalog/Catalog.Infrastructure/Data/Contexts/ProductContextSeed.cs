using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class ProductContextSeed
    {
        public static async Task SeedData(IMongoCollection<Product> productCollection)
        {
            // If the collection is empty → seed data
            bool existProducts = await productCollection.Find(p => true).AnyAsync();
            if (!existProducts)
            {
                // Read JSON file
                var filePath = Path.Combine("Data", "SeedData", "products.json");
                if (!File.Exists(filePath))
                    return; // nothing to seed if file not found
                var jsonData = await File.ReadAllTextAsync(filePath);
                // Deserialize JSON to list of Product
                var products = JsonSerializer.Deserialize<List<Product>>(jsonData);
                if (products != null && products.Count > 0)
                {
                    await productCollection.InsertManyAsync(products);
                }
            }
        }
    }
}
