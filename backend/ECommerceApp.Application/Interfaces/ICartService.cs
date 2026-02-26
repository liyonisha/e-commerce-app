using ECommerceApp.Application.DTOs.Cart;

namespace ECommerceApp.Application.Interfaces;

public interface ICartService
{
    Task<CartDto> GetCartAsync(int userId);
    Task<CartDto> AddToCartAsync(int userId, AddToCartDto dto);
    Task<CartDto> UpdateCartItemAsync(int userId, int cartItemId, UpdateCartItemDto dto);
    Task<CartDto> RemoveCartItemAsync(int userId, int cartItemId);
    Task ClearCartAsync(int userId);
}
