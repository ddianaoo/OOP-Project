namespace FoodDelivery.Domain.Entities;

public class Cart
{
    private List<(Dish dish, int quantity)> _items = new();

    public IReadOnlyList<(Dish dish, int quantity)> Items => _items;

    public bool AddItem(Dish dish, int quantity)
    {
        if (quantity <= 0) return false;

        var existing = _items.FirstOrDefault(i => i.dish.Id == dish.Id);

        if (existing.dish != null)
        {
            _items.Remove(existing);
            _items.Add((dish, existing.quantity + quantity));
            return true;
        }

        _items.Add((dish, quantity));
        return true;
    }

    public bool RemoveItem(Dish dish)
    {
        var item = _items.FirstOrDefault(i => i.dish.Id == dish.Id);

        if (item.dish == null) return false;

        _items.Remove(item);
        return true;
    }

    public void Clear() => _items.Clear();
}