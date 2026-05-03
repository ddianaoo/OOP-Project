using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Interfaces;

namespace FoodDelivery.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly List<Cart> _carts = new();

    public void AddToCart(Guid clientId, Guid dishId, int quantity)
    {
        var cart = GetOrCreateCart(clientId);
        cart.AddItem(dishId, quantity);
    }

    public void RemoveFromCart(Guid clientId, Guid dishId)
    {
        var cart = GetOrCreateCart(clientId);
        cart.RemoveItem(dishId);
    }

    public void Clear(Guid clientId)
    {
        var cart = GetOrCreateCart(clientId);
        cart.Clear();
    }

    private Cart GetOrCreateCart(Guid clientId)
    {
        var cart = _carts.FirstOrDefault(x => x.ClientId == clientId);

        if (cart == null)
        {
            cart = new Cart(clientId);
            _carts.Add(cart);
        }

        return cart;
    }
}