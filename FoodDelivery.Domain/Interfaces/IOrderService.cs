using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Interfaces;

public interface IOrderService
{
    Task<Order> CreateAsync(Order order);
    Task<List<Order>> GetAllAsync();
    Task<bool> ChangeStatusAsync(int orderId, OrderStatus status);
    Task<List<Order>> GetByClientIdAsync(Guid clientId);
}