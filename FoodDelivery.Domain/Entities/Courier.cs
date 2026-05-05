using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

public class Courier : User
{
    public bool IsAvailable { get; set; } = true;
    public List<Order> Orders { get; private set; } = new();

    private Courier() { }
    public Courier(string email, string password, string first, string last, DateTime birth, string phone)
        : base(email, password, first, last, birth, phone)
    {
    }

    public bool AcceptOrder(Order order)
    {
        if (order.Status != OrderStatus.New)
            return false;

        order.AssignCourier(this.Id);
        order.Accept(this.Id);

        IsAvailable = false;
        return true;
    }

}