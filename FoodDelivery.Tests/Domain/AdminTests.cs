using System;
using System.Linq;
using FoodDelivery.Domain.Entities;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class AdminTests
    {
        [Fact]
        public void Admin_Should_Be_Created_Successfully()
        {
            var admin = new Admin(
                "admin@mail.com",
                "123456",
                "Admin",
                "Root",
                new DateTime(1990, 1, 1),
                "+380991234567"
            );

            Assert.Equal("admin@mail.com", admin.Email);
            Assert.Equal("Admin", admin.FirstName);
        }
    }
}