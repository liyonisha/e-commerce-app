using AutoMapper;
using ECommerceApp.Application.Common;
using ECommerceApp.Application.DTOs.Product;
using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;

namespace ECommerceApp.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<ProductDto>> GetProductsAsync(int page, int pageSize, string? search, int? categoryId)
    {
        var (products, totalCount) = await _productRepository.GetPagedAsync(page, pageSize, search, categoryId);
        return new PagedResponse<ProductDto>
        {
            Items = _mapper.Map<IEnumerable<ProductDto>>(products),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _productRepository.AddAsync(product);
        var created = await _productRepository.GetByIdAsync(product.Id);
        return _mapper.Map<ProductDto>(created!);
    }

    public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Product {id} not found.");
        _mapper.Map(dto, product);
        await _productRepository.UpdateAsync(product);
        var updated = await _productRepository.GetByIdAsync(product.Id);
        return _mapper.Map<ProductDto>(updated!);
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Product {id} not found.");
        await _productRepository.DeleteAsync(product);
    }
}
