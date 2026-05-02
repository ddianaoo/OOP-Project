using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public User? Login(string email, string password)
    {
        return _context.Users
            .FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    public bool Register(User user)
    {
        if (_context.Users.Any(u => u.Email == user.Email))
            return false;

        _context.Users.Add(user);
        _context.SaveChanges();

        return true;
    }
}