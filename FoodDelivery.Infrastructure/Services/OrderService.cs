using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using FoodDelivery.Domain.Interfaces;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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

    public async Task<List<Order>> GetMyActiveOrders(Guid courierId)
    {
        return await _context.Orders
            .Where(o => o.CourierId == courierId)
            .Include(o => o.Items)
            .ThenInclude(i => i.Dish)
            .Include(o => o.Client)
            .ToListAsync();
    }
    public async Task<List<Order>> GetAvailableOrders()
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Dish)
            .Where(o => o.CourierId == null &&
           o.Status == OrderStatus.New)
            .Include(o => o.Client)
            .ToListAsync();
    }

    public async Task<List<Order>> GetByClientIdAsync(Guid clientId)
    {
        return await _context.Orders
            .Where(o => o.ClientId == clientId)
            .Include(o => o.Items)
            .ThenInclude(i => i.Dish)
            .Include(o => o.Courier)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> Accept(Guid orderId, Guid courierId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

        if (order == null)
            return false;

        if (order.Status != OrderStatus.New)
            return false;

        if (order.CourierId != null)
            return false;

        var courier = await _context.Users.OfType<Courier>()
            .FirstOrDefaultAsync(x => x.Id == courierId);

        if (courier == null || !courier.IsAvailable)
            return false;

        order.CourierId = courierId;
        order.Status = OrderStatus.Accepted;

        courier.IsAvailable = false;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SetInProgress(Guid orderId, Guid courierId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

        if (order == null)
            return false;

        if (order.CourierId != courierId)
            return false;

        order.SetInProgress();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Deliver(Guid orderId, Guid courierId)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

        if (order == null)
            return false;

        if (order.CourierId != courierId)
            return false;

        if (order.Status != OrderStatus.InProgress)
            return false;

        order.Deliver();
        var courier = await _context.Users.OfType<Courier>()
            .FirstOrDefaultAsync(x => x.Id == courierId);
        courier.IsAvailable = true;

        await _context.SaveChangesAsync();
        return true;
    }
}