using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> NameExistsAsync(string name);
}
