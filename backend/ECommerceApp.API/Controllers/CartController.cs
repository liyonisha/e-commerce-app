using ECommerceApp.API.Extensions;
using ECommerceApp.Application.Common;
using ECommerceApp.Application.DTOs.Cart;
using ECommerceApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService) => _cartService = cartService;

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var cart = await _cartService.GetCartAsync(User.GetUserId());
        return Ok(ApiResponse<CartDto>.Ok(cart));
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] AddToCartDto dto)
    {
        var cart = await _cartService.AddToCartAsync(User.GetUserId(), dto);
        return Ok(ApiResponse<CartDto>.Ok(cart, "Item added to cart."));
    }

    [HttpPut("items/{cartItemId:int}")]
    public async Task<IActionResult> UpdateItem(int cartItemId, [FromBody] UpdateCartItemDto dto)
    {
        var cart = await _cartService.UpdateCartItemAsync(User.GetUserId(), cartItemId, dto);
        return Ok(ApiResponse<CartDto>.Ok(cart, "Cart updated."));
    }

    [HttpDelete("items/{cartItemId:int}")]
    public async Task<IActionResult> RemoveItem(int cartItemId)
    {
        var cart = await _cartService.RemoveCartItemAsync(User.GetUserId(), cartItemId);
        return Ok(ApiResponse<CartDto>.Ok(cart, "Item removed."));
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart()
    {
        await _cartService.ClearCartAsync(User.GetUserId());
        return Ok(ApiResponse<object>.Ok(null!, "Cart cleared."));
    }
}
