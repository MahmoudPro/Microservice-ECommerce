using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces.Repositories;
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
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await context.Products.Find(p => true).ToListAsync();
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
