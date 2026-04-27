namespace FoodDelivery.Domain.Entities;

public class Dish
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string? ImageUrl { get; private set; }

    private Dish() { } // EF Core

    public Dish(string name, string description, decimal price, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Invalid name");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Invalid description");

        if (price <= 0)
            throw new ArgumentException("Price must be > 0");

        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }
}