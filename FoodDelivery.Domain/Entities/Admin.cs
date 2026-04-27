namespace FoodDelivery.Domain.Entities;

public class Admin : User
{
    public Admin(string email, string password, string first, string last, DateTime birth, string phone)
        : base(email, password, first, last, birth, phone)
    {
    }

    public bool CreateDish(Menu menu, Dish dish)
    {
        return menu.AddDish(dish);
    }

    public bool UpdateDish(Menu menu, Guid dishId, string name, string description, decimal price, string? imageUrl)
    {
        return menu.UpdateDish(dishId, name, description, price, imageUrl);
    }

    public bool DeleteDish(Menu menu, Guid dishId)
    {
        return menu.RemoveDish(dishId);
    }
}