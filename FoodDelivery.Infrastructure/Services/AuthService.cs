using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly List<User> _users = new();

    public bool Login(string email, string password)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        return user != null && user.Login(email, password);
    }

    public bool Register(User user)
    {
        if (_users.Any(u => u.Email == user.Email))
            return false;

        _users.Add(user);
        return true;
    }
}