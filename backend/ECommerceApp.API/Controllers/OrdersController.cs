using ECommerceApp.API.Extensions;
using ECommerceApp.Application.Common;
using ECommerceApp.Application.DTOs.Order;
using ECommerceApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService) => _orderService = orderService;

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout()
    {
        var order = await _orderService.CheckoutAsync(User.GetUserId());
        return Ok(ApiResponse<OrderDto>.Ok(order, "Order placed successfully."));
    }

    [HttpGet]
    public async Task<IActionResult> GetMyOrders()
    {
        var orders = await _orderService.GetUserOrdersAsync(User.GetUserId());
        return Ok(ApiResponse<IEnumerable<OrderDto>>.Ok(orders));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id, User.GetUserId());
        if (order == null) return NotFound(ApiResponse<OrderDto>.Fail("Order not found."));
        return Ok(ApiResponse<OrderDto>.Ok(order));
    }
}
