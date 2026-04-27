using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly Cart _cart = new();

    public bool AddToCart(Dish dish, int quantity)
    {
        return _cart.AddItem(dish, quantity);
    }

    public bool RemoveFromCart(Dish dish)
    {
        return _cart.RemoveItem(dish);
    }
}