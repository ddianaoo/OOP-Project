using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Infrastructure.Services;

public class OrderService
{
    private readonly List<Order> _orders = new();

    public Order CreateOrder(string address, List<(Dish dish, int quantity)> items)
    {
        var order = new Order(address, items);
        _orders.Add(order);
        return order;
    }

    public List<Order> GetOrders() => _orders;

    public bool UpdateStatus(Order order, OrderStatus status)
    {
        return order.ChangeStatus(status);
    }
}