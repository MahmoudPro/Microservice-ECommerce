using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class BrandContextSeed
    {
        public static async Task SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            // If the collection is empty → seed data
            bool existBrands = await brandCollection.Find(p => true).AnyAsync();
            if (!existBrands)
            {
                // Read JSON file
                var filePath = Path.Combine("Data", "SeedData", "brands.json");
                if (!File.Exists(filePath))
                    return; // nothing to seed if file not found

                var jsonData = await File.ReadAllTextAsync(filePath);

                // Deserialize JSON to list of ProductBrand
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(jsonData);

                if (brands != null && brands.Count > 0)
                {
                    await brandCollection.InsertManyAsync(brands);
                }
            }
        }
    }
}
