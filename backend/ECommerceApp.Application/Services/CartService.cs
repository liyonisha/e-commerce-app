using AutoMapper;
using ECommerceApp.Application.DTOs.Cart;
using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces;

namespace ECommerceApp.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CartDto> GetCartAsync(int userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId)
            ?? await _cartRepository.CreateCartAsync(userId);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> AddToCartAsync(int userId, AddToCartDto dto)
    {
        var product = await _productRepository.GetByIdAsync(dto.ProductId)
            ?? throw new KeyNotFoundException("Product not found.");

        if (product.Stock < dto.Quantity)
            throw new InvalidOperationException("Insufficient stock.");

        var cart = await _cartRepository.GetCartByUserIdAsync(userId)
            ?? await _cartRepository.CreateCartAsync(userId);

        var existingItem = await _cartRepository.GetCartItemAsync(cart.Id, dto.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += dto.Quantity;
            await _cartRepository.UpdateCartItemAsync(existingItem);
        }
        else
        {
            await _cartRepository.AddCartItemAsync(new CartItem
            {
                CartId = cart.Id,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            });
        }

        var updatedCart = await _cartRepository.GetCartByUserIdAsync(userId);
        return _mapper.Map<CartDto>(updatedCart!);
    }

    public async Task<CartDto> UpdateCartItemAsync(int userId, int cartItemId, UpdateCartItemDto dto)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId)
            ?? throw new KeyNotFoundException("Cart not found.");

        var item = cart.CartItems.FirstOrDefault(i => i.Id == cartItemId)
            ?? throw new KeyNotFoundException("Cart item not found.");

        if (dto.Quantity <= 0)
        {
            await _cartRepository.RemoveCartItemAsync(item);
        }
        else
        {
            item.Quantity = dto.Quantity;
            await _cartRepository.UpdateCartItemAsync(item);
        }

        var updatedCart = await _cartRepository.GetCartByUserIdAsync(userId);
        return _mapper.Map<CartDto>(updatedCart!);
    }

    public async Task<CartDto> RemoveCartItemAsync(int userId, int cartItemId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId)
            ?? throw new KeyNotFoundException("Cart not found.");

        var item = cart.CartItems.FirstOrDefault(i => i.Id == cartItemId)
            ?? throw new KeyNotFoundException("Cart item not found.");

        await _cartRepository.RemoveCartItemAsync(item);

        var updatedCart = await _cartRepository.GetCartByUserIdAsync(userId);
        return _mapper.Map<CartDto>(updatedCart!);
    }

    public async Task ClearCartAsync(int userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart != null)
            await _cartRepository.ClearCartAsync(cart.Id);
    }
}
