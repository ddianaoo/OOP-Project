public class CartItem
{
    public int Id { get; private set; }

    public Guid CartId { get; private set; }
    public Cart Cart { get; private set; }

    public Guid DishId { get; private set; }
    public Dish Dish { get; private set; }

    public int Quantity { get; set; }

    private CartItem() { }

    public CartItem(Guid cartId, Guid dishId, int quantity)
    {
        CartId = cartId;
        DishId = dishId;
        Quantity = quantity;
    }

    public void Increase(int qty) => Quantity += qty;
}