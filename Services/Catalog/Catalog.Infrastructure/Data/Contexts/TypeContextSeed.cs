using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class TypeContextSeed
    {
        // Implement seeding logic for ProductType collection here
        public static async Task SeedData(IMongoCollection<ProductType> typeCollection)
        {
            // If the collection is empty → seed data
            bool existTypes = await typeCollection.Find(p => true).AnyAsync();
            if (!existTypes)
            {
                // Read JSON file
                var filePath = Path.Combine("Data", "SeedData", "types.json");
                if (!File.Exists(filePath))
                    return; // nothing to seed if file not found
                var jsonData = await File.ReadAllTextAsync(filePath);
                // Deserialize JSON to list of ProductType
                var types = JsonSerializer.Deserialize<List<ProductType>>(jsonData);
                if (types != null && types.Count > 0)
                {
                    await typeCollection.InsertManyAsync(types);
                }
            }
        }
    }
}
