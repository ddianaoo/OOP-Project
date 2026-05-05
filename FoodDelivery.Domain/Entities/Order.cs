using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;
public class Order
{
    public int Id { get; private set; }

    public string Address { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; }

    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }

    public Guid? CourierId { get; private set; }
    public Courier? Courier { get; private set; }

    public List<OrderItem> Items { get; private set; } = new();

    private Order() { }

    public Order(Guid clientId, string address, List<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Invalid address");

        if (items == null || !items.Any())
            throw new ArgumentException("Order must have items");

        ClientId = clientId;
        Address = address;
        Items = items;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Created;
    }

    public void AssignCourier(Guid courierId)
    {
        if (CourierId != null)
            throw new Exception("Already assigned");

        CourierId = courierId;
    }

    public bool ChangeStatus(OrderStatus status)
    {
        if (Status == OrderStatus.Delivered)
            return false;

        Status = status;
        return true;
    }
}