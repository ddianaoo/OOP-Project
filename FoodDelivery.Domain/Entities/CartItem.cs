using FoodDelivery.Domain.Entities;

public class CartItem
{
    public Guid DishId { get; private set; }
    public Dish Dish { get; private set; }

    public int Quantity { get; private set; }

    private CartItem() { }

    public CartItem(Dish dish, int quantity)
    {
        Dish = dish;
        DishId = dish.Id;
        Quantity = quantity;
    }

    public void Increase(int qty) => Quantity += qty;
}