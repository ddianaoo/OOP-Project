using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Interfaces;

public interface IOrderService
{
    Task<Order> CreateAsync(Order order);
    Task<List<Order>> GetAllAsync();
    Task<List<Order>> GetMyActiveOrders(Guid courierId);
    Task<List<Order>> GetAvailableOrders();
    Task<List<Order>> GetByClientIdAsync(Guid clientId);

    Task<bool> Accept(Guid orderId, Guid courierId);
    Task<bool> SetInProgress(Guid orderId, Guid courierId);
    Task<bool> Deliver(Guid orderId, Guid courierId);
}