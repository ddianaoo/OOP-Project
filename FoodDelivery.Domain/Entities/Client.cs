using FoodDelivery.Domain.Entities;

public class Client : User
{
    public Cart Cart { get; private set; }
    public List<Order> Orders { get; private set; } = new();

    private Client() { }

    public Client(string email, string password, string first, string last, DateTime birth, string phone)
        : base(email, password, first, last, birth, phone)
    {
    }

    public void AddToCart(Dish dish, int quantity)
    {
        Cart?.AddItem(dish.Id, quantity);
    }

    public Order CreateOrder(string address)
    {
        if (Cart == null || !Cart.Items.Any())
            throw new Exception("Cart empty");

        var order = new Order(
            this.Id,
            address,
            Cart.Items.Select(x => new OrderItem(x.Dish.Id, x.Quantity)).ToList()
        );

        Cart.Clear();
        return order;
    }
}