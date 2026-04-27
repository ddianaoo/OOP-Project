using FoodDelivery.Domain.Entities;
using System.Collections.Generic;

namespace FoodDelivery.Application.Interfaces;

public interface IMenuService
{
    IReadOnlyList<Dish> GetMenu();
    bool AddDish(Dish dish);
    bool RemoveDish(Dish dish);
}