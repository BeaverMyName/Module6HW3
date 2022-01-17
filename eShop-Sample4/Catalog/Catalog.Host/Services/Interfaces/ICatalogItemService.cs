namespace Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Dtos;

public interface ICatalogItemService
{
    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> UpdateProductAsync(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItemDto?> GetByIdAsync(int id);
    Task<IEnumerable<CatalogItemDto>> GetByBrandAsync(string brand);
    Task<IEnumerable<CatalogItemDto>> GetByTypeAsync(string type);
}