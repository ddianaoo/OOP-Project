using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

public interface IAuthService
{
    User? Login(string email, string password);
    bool Register(User user);
}