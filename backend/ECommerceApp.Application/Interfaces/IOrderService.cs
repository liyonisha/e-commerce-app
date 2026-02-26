using ECommerceApp.Application.DTOs.Order;

namespace ECommerceApp.Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CheckoutAsync(int userId);
    Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId);
    Task<OrderDto?> GetOrderByIdAsync(int orderId, int userId);
}
