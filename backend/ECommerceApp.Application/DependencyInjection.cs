using ECommerceApp.Application.Interfaces;
using ECommerceApp.Application.Mappings;
using ECommerceApp.Application.Services;
using ECommerceApp.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
