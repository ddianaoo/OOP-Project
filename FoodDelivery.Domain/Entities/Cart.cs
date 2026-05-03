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

    public void AddItem(Guid dishId, int quantity)
    {
        var existing = Items.FirstOrDefault(x => x.DishId == dishId);

        if (existing != null)
            existing.Increase(quantity);
        else
            Items.Add(new CartItem(Id, dishId, quantity));
    }

    public void RemoveItem(Guid dishId)
    {
        var item = Items.FirstOrDefault(x => x.DishId == dishId);
        if (item != null)
            Items.Remove(item);
    }

    public void Clear() => Items.Clear();
}