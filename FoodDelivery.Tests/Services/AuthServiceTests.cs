using System;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Data;
using FoodDelivery.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FoodDelivery.Tests.Services
{
    public class AuthServiceTests
    {
        // =========================
        // HELPERS
        // =========================

        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        private Client CreateClient()
        {
            return new Client(
                "test@mail.com",
                "123456",
                "John",
                "Doe",
                new DateTime(1990, 1, 1),
                "+380000000000"
            );
        }

        // =========================
        // REGISTER
        // =========================

        [Fact]
        public void Register_Should_Add_User_When_Email_Not_Exists()
        {
            var context = CreateContext();
            var service = new AuthService(context);

            var user = CreateClient();

            var result = service.Register(user);

            Assert.True(result);
            Assert.Single(context.Users);
        }

        [Fact]
        public void Register_Should_Return_False_When_Email_Exists()
        {
            var context = CreateContext();
            var service = new AuthService(context);

            var user1 = CreateClient();
            var user2 = CreateClient();

            service.Register(user1);
            var result = service.Register(user2);

            Assert.False(result);
            Assert.Single(context.Users);
        }

        // =========================
        // LOGIN
        // =========================

        [Fact]
        public void Login_Should_Return_User_When_Credentials_Are_Correct()
        {
            var context = CreateContext();
            var service = new AuthService(context);

            var user = CreateClient();

            service.Register(user);

            var result = service.Login("test@mail.com", "123456");

            Assert.NotNull(result);
            Assert.Equal("test@mail.com", result.Email);
        }

        [Fact]
        public void Login_Should_Return_Null_When_Email_Is_Wrong()
        {
            var context = CreateContext();
            var service = new AuthService(context);

            var user = CreateClient();
            service.Register(user);

            var result = service.Login("wrong@mail.com", "123456");

            Assert.Null(result);
        }

        [Fact]
        public void Login_Should_Return_Null_When_Password_Is_Wrong()
        {
            var context = CreateContext();
            var service = new AuthService(context);

            var user = CreateClient();
            service.Register(user);

            var result = service.Login("test@mail.com", "wrongpass");

            Assert.Null(result);
        }
    }
}