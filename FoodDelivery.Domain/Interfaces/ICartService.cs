using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Interfaces;

public interface ICartService
{
    Task AddToCart(Guid clientId, Guid dishId, int quantity);
    Task RemoveFromCart(Guid clientId, Guid dishId);
    Task Clear(Guid clientId);
    Task UpdateQuantity(Guid clientId, Guid dishId, int delta);
    Task<Cart> GetCart(Guid clientId);
}