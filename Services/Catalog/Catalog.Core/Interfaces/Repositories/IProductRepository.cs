using Catalog.Core.Entities;
using Catalog.Core.Specs;
namespace Catalog.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetAllProductsAsync(CatalogSpecParams catalogSpecParams);
        Task<Product> GetProductByIdAsync(string productId);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByBrandAsync(string brandName);
        Task<Product> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(string productId);
    }
}
