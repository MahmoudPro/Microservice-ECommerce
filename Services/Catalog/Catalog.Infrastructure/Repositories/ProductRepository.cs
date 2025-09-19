using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        public ICatalogContext context;
        public ProductRepository(ICatalogContext _catalogContext) {
            context = _catalogContext;
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await context.Products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrandAsync(string brandName)
        {
            return await context.Products.Find(p => p.Brand.Name == brandName ).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return await context.Products.Find(p => p.Name == name).ToListAsync();
        }
        public async Task<Pagination<Product>> GetAllProductsAsync(CatalogSpecParams catalogSpecParams)
        {
            var filter = Builders<Product>.Filter.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter = filter & Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(catalogSpecParams.Search, "i"));
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                filter = filter & Builders<Product>.Filter.Eq(p => p.Brand.Id, catalogSpecParams.BrandId);
            }
            if (catalogSpecParams.TypeId != null)
            {
                filter = filter & Builders<Product>.Filter.Eq(p => p.Type.Id, catalogSpecParams.TypeId);
            }
            var totalItems = await context.Products.CountDocumentsAsync(filter);
            var products = await DataFilterAsync(catalogSpecParams, filter);
            return new Pagination<Product>(
                    catalogSpecParams.PageIndex,
                    catalogSpecParams.PageSize,
                    (int)totalItems,
                    products
                );
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProductAsync(string productId)
        {
            var deleteResult = await context.Products.DeleteOneAsync(p => p.Id == productId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult = await context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        
        public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
        {
            return await context.ProductBrands.Find(b => true).ToListAsync();
        }
        public async Task<IEnumerable<ProductType>> GetAllTypesAsync()
        {
            return await context.ProductTypes.Find(t => true).ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> DataFilterAsync(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            var defSort = Builders<Product>.Sort.Ascending(p => p.Name);
            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                switch (catalogSpecParams.Sort.ToLower())
                {
                    case "nameasc":
                        defSort = Builders<Product>.Sort.Ascending(p => p.Name);
                        break;
                    case "namedesc":
                        defSort = Builders<Product>.Sort.Descending(p => p.Name);
                        break;
                    case "priceasc":
                        defSort = Builders<Product>.Sort.Ascending(p => p.Price);
                        break;
                    case "pricedesc":
                        defSort = Builders<Product>.Sort.Descending(p => p.Price);
                        break;
                    default:
                        defSort = Builders<Product>.Sort.Ascending(p => p.Name);
                        break;
                }
            }
            return await context
                 .Products
                 .Find(filter)
                 .Sort(defSort)
                 .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                 .Limit(catalogSpecParams.PageSize)
                 .ToListAsync();
        }




        public Task AddBrandAsync(ProductBrand brand)
        {
            throw new NotImplementedException();
        }

        public Task AddTypeAsync(ProductType type)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBrandAsync(string brandId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTypeAsync(string typeId)
        {
            throw new NotImplementedException();
        }
        public Task<ProductBrand> GetBrandByIdAsync(string brandId)
        {
            throw new NotImplementedException();
        }
        public Task<ProductType> GetTypeByIdAsync(string typeId)
        {
            throw new NotImplementedException();
        }
        public Task UpdateBrandAsync(ProductBrand brand)
        {
            throw new NotImplementedException();
        }
        public Task UpdateTypeAsync(ProductType type)
        {
            throw new NotImplementedException();
        }
    }
}
