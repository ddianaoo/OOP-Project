using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

public interface IMenuService
{
    List<Dish> GetMenu();
    bool AddDish(Dish dish);
    bool RemoveDish(Dish dish);
}