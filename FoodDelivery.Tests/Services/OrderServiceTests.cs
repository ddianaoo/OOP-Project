using Xunit;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Infrastructure.Data;
using FoodDelivery.Infrastructure.Services;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Tests.Services;

public class OrderServiceTests
{
    private AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Order()
    {
        var context = CreateContext();
        var service = new OrderService(context);

        var dish = new Dish("Burger", "Tasty burger", 15);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();

        var order = new Order(
            Guid.NewGuid(),
            "Kyiv street 1",
            new List<OrderItem>
            {
                new OrderItem(dish, 2)
            }
        );

        var result = await service.CreateAsync(order);

        Assert.NotNull(result);
        Assert.Single(await context.Orders.ToListAsync());
        Assert.Single(await context.OrderItems.ToListAsync());
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Orders_With_Items_And_Dish()
    {
        var context = CreateContext();
        var service = new OrderService(context);

        var client = new Client(
            "test@test.com",
            "123456",
            "John",
            "Doe",
            new DateTime(2000, 1, 1),
            "+380991112233"
        );

        context.Users.Add(client);
        await context.SaveChangesAsync();

        var dish = new Dish("Pizza", "Cheese pizza", 20);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();

        var order = new Order(
            client.Id,
            "Kyiv street 2",
            new List<OrderItem>
            {
            new OrderItem(dish, 1)
            }
        );

        await service.CreateAsync(order);

        context.ChangeTracker.Clear();

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Single(result[0].Items);
        Assert.NotNull(result[0].Items[0].Dish);
        Assert.Equal("Pizza", result[0].Items[0].Dish.Name);
    }

    [Fact]
    public async Task ChangeStatusAsync_Should_Update_Status()
    {
        var context = CreateContext();
        var service = new OrderService(context);

        var dish = new Dish("Sushi", "Fresh sushi", 25);
        await context.Dishes.AddAsync(dish);
        await context.SaveChangesAsync();

        var order = new Order(
            Guid.NewGuid(),
            "Kyiv street 3",
            new List<OrderItem>
            {
                new OrderItem(dish, 3)
            }
        );

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        var result = await service.ChangeStatusAsync(order.Id, OrderStatus.Accepted);

        Assert.True(result);

        var updated = await context.Orders.FirstAsync();
        Assert.Equal(OrderStatus.Accepted, updated.Status);
    }

    [Fact]
    public async Task ChangeStatusAsync_Should_Return_False_If_Order_Not_Found()
    {
        var context = CreateContext();
        var service = new OrderService(context);

        var result = await service.ChangeStatusAsync(999, OrderStatus.Accepted);

        Assert.False(result);
    }
}