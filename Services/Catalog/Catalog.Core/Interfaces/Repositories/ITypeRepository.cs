using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces.Repositories
{
    public interface ITypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllTypesAsync();
        Task<ProductType> GetTypeByIdAsync(string typeId);
        Task AddTypeAsync(ProductType type);
        Task UpdateTypeAsync(ProductType type);
        Task DeleteTypeAsync(string typeId);
    }
}
