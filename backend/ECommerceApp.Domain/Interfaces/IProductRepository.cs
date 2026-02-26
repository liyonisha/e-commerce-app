using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<(IEnumerable<Product> Products, int TotalCount)> GetPagedAsync(
        int page, int pageSize, string? search = null, int? categoryId = null);
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
}
