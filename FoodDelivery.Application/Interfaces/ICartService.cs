using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

public interface ICartService
{
    bool AddToCart(Dish dish, int quantity);
    bool RemoveFromCart(Dish dish);
}