using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces;

public interface IAuthService
{
    bool Login(string email, string password);
    bool Register(User user);
}