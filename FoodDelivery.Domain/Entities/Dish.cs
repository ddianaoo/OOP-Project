using FoodDelivery.Domain.Entities;

public class Dish
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string? ImageUrl { get; private set; }
    public List<OrderItem> OrderItems { get; private set; } = new();

    private Dish() { } // EF

    public Dish(string name, string description, decimal price, string? imageUrl = null)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }

    public void Update(string name, string description, decimal price, string? imageUrl)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }
}