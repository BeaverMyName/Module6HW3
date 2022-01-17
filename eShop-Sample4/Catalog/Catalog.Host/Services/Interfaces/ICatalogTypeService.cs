namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> CreateTypeAsync(string name);
        Task<bool> DeleteTypeAsync(int id);
        Task<bool> UpdateTypeAsync(int id, string name);
    }
}
