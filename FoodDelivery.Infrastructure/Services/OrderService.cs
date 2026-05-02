using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<List<Order>> GetOrders()
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Dish)
            .ToListAsync();
    }

    public async Task<bool> UpdateStatus(int orderId, OrderStatus status)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
        if (order == null) return false;

        order.ChangeStatus(status);
        await _context.SaveChangesAsync();
        return true;
    }
}