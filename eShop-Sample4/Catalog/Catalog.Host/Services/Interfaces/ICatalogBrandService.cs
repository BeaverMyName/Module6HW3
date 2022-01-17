namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> CreateBrandAsync(string name);
        Task<bool> DeleteBrandAsync(int id);
        Task<bool> UpdateBrandAsync(int id, string name);
    }
}
