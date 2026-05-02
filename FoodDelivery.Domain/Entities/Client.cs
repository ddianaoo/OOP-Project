using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

public class Client : User
{

    public Cart Cart { get; private set; } = new();

    private Client() { }

    public Client(string email, string password, string first, string last, DateTime birth, string phone)
        : base(email, password, first, last, birth, phone)
    {
    }

    public void AddToCart(Dish dish, int quantity)
    {
        Cart.AddItem(dish, quantity);
    }

    public Order CreateOrder(string address)
    {
        if (!Cart.Items.Any())
            throw new Exception("Cart empty");

        var order = new Order(
            address,
            Cart.Items.Select(x => new OrderItem(x.Dish, x.Quantity)).ToList()
        );

        Cart.Clear();
        return order;
    }

    public OrderStatus TrackOrder(Order order)
    {
        return order.Status;
    }
}