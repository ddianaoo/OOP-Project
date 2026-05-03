using System;
using FoodDelivery.Domain.Entities;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class UserTests
    {
        [Fact]
        public void Constructor_Should_Create_User_When_Data_Is_Valid()
        {
            var user = new User(
                "test@mail.com",
                "123456",
                "John",
                "Doe",
                new DateTime(2000, 1, 1),
                "+380991234567"
            );

            Assert.Equal("test@mail.com", user.Email);
            Assert.Equal("John", user.FirstName);
            Assert.Equal("Doe", user.LastName);
            Assert.NotEqual(Guid.Empty, user.Id);
        }

        [Fact]
        public void Constructor_Should_Throw_When_Email_Invalid()
        {
            Assert.Throws<ArgumentException>(() =>
                new User("bademail", "123456", "John", "Doe", DateTime.Today, "123"));
        }

        [Fact]
        public void Constructor_Should_Throw_When_Password_Short()
        {
            Assert.Throws<ArgumentException>(() =>
                new User("test@mail.com", "123", "John", "Doe", DateTime.Today, "123"));
        }
    }
}