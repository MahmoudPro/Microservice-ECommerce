using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetAllBrandsAsync();
        Task<ProductBrand> GetBrandByIdAsync(string brandId);
        Task AddBrandAsync(ProductBrand brand);
        Task UpdateBrandAsync(ProductBrand brand);
        Task DeleteBrandAsync(string brandId);
    }
}
