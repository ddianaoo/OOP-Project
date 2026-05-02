namespace FoodDelivery.Domain.Entities;

public class OrderItem
{
    public int Id { get; private set; }

    public Guid DishId { get; private set; }
    public Dish Dish { get; private set; }

    public int Quantity { get; private set; }

    public int OrderId { get; private set; }
    public Order Order { get; private set; }

    private OrderItem() { } // EF

    public OrderItem(Dish dish, int quantity)
    {
        Dish = dish;
        DishId = dish.Id;
        Quantity = quantity;
    }
}