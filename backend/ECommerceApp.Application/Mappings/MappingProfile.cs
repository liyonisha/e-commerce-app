using AutoMapper;
using ECommerceApp.Application.DTOs.Cart;
using ECommerceApp.Application.DTOs.Category;
using ECommerceApp.Application.DTOs.Order;
using ECommerceApp.Application.DTOs.Product;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.Product != null ? src.Product.ImageUrl : string.Empty))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Product != null ? src.Product.Price : 0));
        CreateMap<Cart, CartDto>()
            .ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.CartItems));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.Product != null ? src.Product.ImageUrl : string.Empty));
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));
    }
}
