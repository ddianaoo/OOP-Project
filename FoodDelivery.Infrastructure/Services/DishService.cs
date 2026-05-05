using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class DishService
{
    private readonly AppDbContext _context;

    public DishService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Dish>> GetAll()
    {
        return await _context.Dishes.ToListAsync();
    }

    public async Task<Dish?> GetById(Guid id)
    {
        return await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> Create(Dish dish)
    {
        var exists = await _context.Dishes
            .AnyAsync(x => x.Name.ToLower() == dish.Name.ToLower());

        if (exists) return false;

        _context.Dishes.Add(dish);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Guid id, string name, string desc, decimal price, string? img)
    {
        var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id);
        if (dish == null) return false;

        var exists = await _context.Dishes
            .AnyAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != id);

        if (exists) return false;

        dish.Update(name, desc, price, img);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var dish = await _context.Dishes.FirstOrDefaultAsync(x => x.Id == id);
        if (dish == null) return false;

        _context.Dishes.Remove(dish);
        await _context.SaveChangesAsync();

        return true;
    }
}