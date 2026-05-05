using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using FoodDelivery.Domain.Interfaces;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Items)
                .ThenInclude(i => i.Dish)
            .Include(o => o.Client)
            .ToListAsync();
    }

    public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus status)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == orderId);

        if (order == null)
            return false;

        order.ChangeStatus(status);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Order>> GetByClientIdAsync(Guid clientId)
    {
        return await _context.Orders
            .Where(o => o.ClientId == clientId)
            .Include(o => o.Items)
            .ThenInclude(i => i.Dish)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}