using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<Client>> GetClients()
        {
            return await _context.Users
                .OfType<Client>()
                .ToListAsync();
        }

        public async Task<List<Courier>> GetCouriers()
        {
            return await _context.Users
                .OfType<Courier>()
                .ToListAsync();
        }
    }
}
