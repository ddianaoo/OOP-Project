using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Interfaces;

public interface ICartService
{
    void AddToCart(Guid clientId, Guid dishId, int quantity);
    void RemoveFromCart(Guid clientId, Guid dishId);
    void Clear(Guid clientId);
}