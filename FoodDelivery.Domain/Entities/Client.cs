using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

public class Client : User
{
    public Cart Cart { get; private set; } = new();

    public Client(string email, string password, string first, string last, DateTime birth, string phone)
        : base(email, password, first, last, birth, phone)
    {
    }

    public bool AddToCart(Dish dish, int quantity)
    {
        return Cart.AddItem(dish, quantity);
    }

    public (bool success, int orderId) CreateOrder(string address)
    {
        if (!Cart.Items.Any())
            return (false, 0);

        var order = new Order(address, Cart.Items.ToList());
        Cart.Clear();

        return (true, order.Id);
    }

    public OrderStatus TrackOrder(Order order)
    {
        return order.Status;
    }
}