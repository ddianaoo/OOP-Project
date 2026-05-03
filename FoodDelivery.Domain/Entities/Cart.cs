using FoodDelivery.Domain.Entities;

public class Cart
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }

    public List<CartItem> Items { get; private set; } = new();

    private Cart() { }

    public Cart(Guid clientId)
    {
        ClientId = clientId;
    }

    public void AddItem(Dish dish, int quantity)
    {
        var existing = Items.FirstOrDefault(x => x.DishId == dish.Id);

        if (existing != null)
            existing.Increase(quantity);
        else
            Items.Add(new CartItem(Id, dish.Id, quantity));
    }

    public void Clear() => Items.Clear();
}