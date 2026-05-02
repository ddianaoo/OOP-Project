using FoodDelivery.Domain.Entities;

public class Cart
{
    public List<CartItem> Items { get; private set; } = new();

    public bool AddItem(Dish dish, int quantity)
    {
        if (quantity <= 0) return false;

        var existing = Items.FirstOrDefault(x => x.DishId == dish.Id);

        if (existing != null)
        {
            existing.Increase(quantity);
            return true;
        }

        Items.Add(new CartItem(dish, quantity));
        return true;
    }

    public void RemoveItem(Guid dishId)
    {
        var item = Items.FirstOrDefault(x => x.DishId == dishId);
        if (item != null)
            Items.Remove(item);
    }

    public void Clear() => Items.Clear();
}