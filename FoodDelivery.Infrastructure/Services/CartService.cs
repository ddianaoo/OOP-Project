using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Interfaces;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Services;



public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddToCart(Guid clientId, Guid dishId, int quantity)
    {
        var cart = await GetOrCreateCart(clientId);

        var item = cart.Items.FirstOrDefault(x => x.DishId == dishId);

        if (item == null)
        {
            cart.Items.Add(new CartItem(cart.Id, dishId, quantity));
        }
        else
        {
            item.Increase(quantity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromCart(Guid clientId, Guid dishId)
    {
        var cart = await GetOrCreateCart(clientId);

        var item = cart.Items.FirstOrDefault(x => x.DishId == dishId);

        if (item != null)
        {
            _context.CartItems.Remove(item);
        }

        await _context.SaveChangesAsync();
    }

    public async Task Clear(Guid clientId)
    {
        var cart = await GetOrCreateCart(clientId);

        _context.CartItems.RemoveRange(cart.Items);

        await _context.SaveChangesAsync();
    }

    private async Task<Cart> CreateCart(Guid clientId)
    {
        var cart = new Cart(clientId);

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return cart;
    }

    public async Task<Cart> GetCart(Guid clientId)
    {
        return await _context.Carts
            .Include(x => x.Items)
            .ThenInclude(i => i.Dish)
            .FirstOrDefaultAsync(x => x.ClientId == clientId)
            ?? await CreateCart(clientId);
    }

    private async Task<Cart> GetOrCreateCart(Guid clientId)
    {
        var cart = await _context.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.ClientId == clientId);

        if (cart == null)
        {
            cart = new Cart(clientId);
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    public async Task UpdateQuantity(Guid clientId, Guid dishId, int delta)
    {
        var cart = await GetOrCreateCart(clientId);

        var item = cart.Items.FirstOrDefault(x => x.DishId == dishId);

        if (item == null) return;

        item.Quantity += delta;

        if (item.Quantity <= 0)
        {
            _context.CartItems.Remove(item);
        }

        await _context.SaveChangesAsync();
    }
}