using FoodDelivery.Domain.Entities;

public class Admin : User
{
    private Admin() { }

    public Admin(string email, string password, string first, string last, DateTime birth, string phone)
        : base(email, password, first, last, birth, phone)
    {
    }
}