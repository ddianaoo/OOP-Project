using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Application.Interfaces;

public interface IOrderService
{
    bool CreateOrder(Order order);
    List<Order> GetOrders();
    bool UpdateStatus(Order order, OrderStatus status);
}