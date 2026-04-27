namespace FoodDelivery.Domain.Entities;

public class Menu
{
    private List<Dish> _dishes = new();

    public IReadOnlyList<Dish> GetMenu() => _dishes;

    public bool AddDish(Dish dish)
    {
        if (_dishes.Any(d => d.Name.ToLower() == dish.Name.ToLower()))
            return false;

        _dishes.Add(dish);
        return true;
    }

    public bool RemoveDish(Guid dishId)
    {
        var dish = _dishes.FirstOrDefault(d => d.Id == dishId);
        if (dish == null) return false;

        _dishes.Remove(dish);
        return true;
    }

    public bool UpdateDish(Guid dishId, string newName, string newDescription, decimal newPrice, string? newImageUrl)
    {
        var dish = _dishes.FirstOrDefault(d => d.Id == dishId);
        if (dish == null) return false;

        if (_dishes.Any(d => d.Id != dishId &&
                             d.Name.ToLower() == newName.ToLower()))
        {
            return false;
        }

        typeof(Dish).GetProperty(nameof(Dish.Name))!
            .SetValue(dish, newName);

        typeof(Dish).GetProperty(nameof(Dish.Description))!
            .SetValue(dish, newDescription);

        typeof(Dish).GetProperty(nameof(Dish.Price))!
            .SetValue(dish, newPrice);

        typeof(Dish).GetProperty(nameof(Dish.ImageUrl))!
            .SetValue(dish, newImageUrl);

        return true;
    }
}