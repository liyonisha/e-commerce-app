using ECommerceApp.Application.Common;
using ECommerceApp.Application.DTOs.Product;

namespace ECommerceApp.Application.Interfaces;

public interface IProductService
{
    Task<PagedResponse<ProductDto>> GetProductsAsync(int page, int pageSize, string? search, int? categoryId);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
    Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto dto);
    Task DeleteProductAsync(int id);
}
