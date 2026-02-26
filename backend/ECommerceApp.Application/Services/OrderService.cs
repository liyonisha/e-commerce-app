using AutoMapper;
using ECommerceApp.Application.DTOs.Order;
using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;

namespace ECommerceApp.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> CheckoutAsync(int userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null || !cart.CartItems.Any())
            throw new InvalidOperationException("Cart is empty.");

        var order = new Order
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var item in cart.CartItems)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId)
                ?? throw new KeyNotFoundException($"Product {item.ProductId} not found.");

            if (product.Stock < item.Quantity)
                throw new InvalidOperationException($"Insufficient stock for {product.Name}.");

            product.Stock -= item.Quantity;
            await _productRepository.UpdateAsync(product);

            order.OrderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price
            });
        }

        order.TotalAmount = order.OrderItems.Sum(i => i.Price * i.Quantity);
        await _orderRepository.AddAsync(order);
        await _cartRepository.ClearCartAsync(cart.Id);

        var created = await _orderRepository.GetOrderWithItemsAsync(order.Id);
        return _mapper.Map<OrderDto>(created!);
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int orderId, int userId)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(orderId);
        if (order == null || order.UserId != userId) return null;
        return _mapper.Map<OrderDto>(order);
    }
}
