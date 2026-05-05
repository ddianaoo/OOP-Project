using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;
public class Order
{
    public Guid Id { get; private set; }

    public string Address { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; }

    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }

    public Guid? CourierId { get; set; }
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
        Status = OrderStatus.New;
    }

    public void AssignCourier(Guid courierId)
    {
        if (CourierId != null)
            throw new Exception("Already assigned");

        CourierId = courierId;
    }

    public void Accept(Guid courierId)
    {
        if (Status != OrderStatus.New)
            throw new Exception("Order already taken");

        CourierId = courierId;
        Status = OrderStatus.Accepted;
    }

    public void SetInProgress()
    {
        if (Status != OrderStatus.Accepted)
            throw new Exception("Not allowed");

        Status = OrderStatus.InProgress;
    }

    public void Deliver()
    {
        if (Status != OrderStatus.InProgress)
            throw new Exception("Not allowed");

        Status = OrderStatus.Delivered;
    }
    public bool CanBeAccepted()
    {
        return Status == OrderStatus.New && CourierId == null;
    }
    public void Cancel()
    {
        if (Status != OrderStatus.New)
            throw new Exception("Cannot cancel accepted order");

        Status = OrderStatus.Cancelled;
    }
}