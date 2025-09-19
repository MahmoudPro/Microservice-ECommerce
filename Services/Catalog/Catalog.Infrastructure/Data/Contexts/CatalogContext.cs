using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; } 

        public IMongoCollection<ProductBrand> ProductBrands { get; } 

        public IMongoCollection<ProductType> ProductTypes { get; } 

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Products = database.GetCollection<Product>(configuration["DatabaseSettings:ProductsCollection"]);
            ProductBrands = database.GetCollection<ProductBrand>(configuration["DatabaseSettings:ProductBrandsCollection"]);
            ProductTypes = database.GetCollection<ProductType>(configuration["DatabaseSettings:ProductTypesCollection"]);

            _= ProductContextSeed.SeedData(Products);
            _= BrandContextSeed.SeedData(ProductBrands);
            _= TypeContextSeed.SeedData(ProductTypes);


        }


    }
}
