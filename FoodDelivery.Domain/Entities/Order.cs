using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

public class Order
{
    private static int _counter = 1;

    public int Id { get; private set; }
    public string Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }

    public List<(Dish dish, int quantity)> Items { get; private set; }

    public Order(string address, List<(Dish dish, int quantity)> items)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Invalid address");

        Id = _counter++;
        Address = address;
        Items = items;
        CreatedAt = DateTime.Now;
        Status = OrderStatus.Created;
    }

    public bool ChangeStatus(OrderStatus status)
    {
        if (Status == OrderStatus.Delivered)
            return false;

        Status = status;
        return true;
    }
}