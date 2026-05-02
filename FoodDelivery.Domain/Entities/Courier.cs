using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

public class Courier : User
{
    public bool IsAvailable { get; private set; } = true;

    private Courier() { }
    public Courier(string email, string password, string first, string last, DateTime birth, string phone)
        : base(email, password, first, last, birth, phone)
    {
    }

    public bool AcceptOrder(Order order)
    {
        if (order.Status != OrderStatus.Created)
            return false;

        order.ChangeStatus(OrderStatus.Accepted);
        IsAvailable = false;
        return true;
    }

    public bool UpdateOrderStatus(Order order, OrderStatus status)
    {
        return order.ChangeStatus(status);
    }
}