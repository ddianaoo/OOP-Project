using Xunit;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Infrastructure.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Tests.Services;

public class DishServiceTests
{
    private AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Create_Should_Add_Dish()
    {
        var context = CreateContext();
        var service = new DishService(context);

        var dish = new Dish("Pizza", "Cheese pizza", 20);

        await service.Create(dish);

        var result = await context.Dishes.ToListAsync();

        Assert.Single(result);
        Assert.Equal("Pizza", result[0].Name);
    }

    [Fact]
    public async Task GetAll_Should_Return_All_Dishes()
    {
        var context = CreateContext();
        var service = new DishService(context);

        context.Dishes.Add(new Dish("Burger", "Tasty", 10));
        context.Dishes.Add(new Dish("Pizza", "Cheese", 20));
        await context.SaveChangesAsync();

        var result = await service.GetAll();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetById_Should_Return_Dish()
    {
        var context = CreateContext();
        var service = new DishService(context);

        var dish = new Dish("Sushi", "Fresh", 25);

        context.Dishes.Add(dish);
        await context.SaveChangesAsync();

        var result = await service.GetById(dish.Id);

        Assert.NotNull(result);
        Assert.Equal("Sushi", result!.Name);
    }

    [Fact]
    public async Task GetById_Should_Return_Null_When_Not_Found()
    {
        var context = CreateContext();
        var service = new DishService(context);

        var result = await service.GetById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task Update_Should_Modify_Dish()
    {
        var context = CreateContext();
        var service = new DishService(context);

        var dish = new Dish("Old", "Desc", 10);

        context.Dishes.Add(dish);
        await context.SaveChangesAsync();

        var result = await service.Update(
            dish.Id,
            "New",
            "NewDesc",
            99,
            null
        );

        var updated = await context.Dishes.FirstAsync();

        Assert.True(result);
        Assert.Equal("New", updated.Name);
        Assert.Equal(99, updated.Price);
    }

    [Fact]
    public async Task Update_Should_Return_False_When_Not_Found()
    {
        var context = CreateContext();
        var service = new DishService(context);

        var result = await service.Update(
            Guid.NewGuid(),
            "x",
            "y",
            10,
            null
        );

        Assert.False(result);
    }

    [Fact]
    public async Task Delete_Should_Remove_Dish()
    {
        var context = CreateContext();
        var service = new DishService(context);

        var dish = new Dish("ToDelete", "Desc", 10);

        context.Dishes.Add(dish);
        await context.SaveChangesAsync();

        var result = await service.Delete(dish.Id);

        var exists = await context.Dishes.AnyAsync();

        Assert.True(result);
        Assert.False(exists);
    }

    [Fact]
    public async Task Delete_Should_Return_False_When_Not_Found()
    {
        var context = CreateContext();
        var service = new DishService(context);

        var result = await service.Delete(Guid.NewGuid());

        Assert.False(result);
    }
}