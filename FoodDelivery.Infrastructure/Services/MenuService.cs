using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using System.Collections.Generic;

namespace FoodDelivery.Infrastructure.Services;

public class MenuService : IMenuService
{
    private readonly Menu _menu = new();

    public bool AddDish(Dish dish) => _menu.AddDish(dish);

    public IReadOnlyList<Dish> GetMenu() => _menu.GetMenu();

    public bool RemoveDish(Dish dish) => _menu.RemoveDish(dish.Id);
}