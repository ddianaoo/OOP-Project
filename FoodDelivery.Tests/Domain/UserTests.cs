using System;
using FoodDelivery.Domain.Entities;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class UserTests
    {
        [Fact]
        public void Constructor_ShouldCreateUser_WhenDataIsValid()
        {
            // Arrange
            var email = "test@mail.com";
            var password = "123456";
            var firstName = "John";
            var lastName = "Doe";
            var birthDate = new DateTime(2000, 1, 1);
            var phone = "+380501234567";

            // Act
            var user = new User(email, password, firstName, lastName, birthDate, phone);

            // Assert
            Assert.Equal(email, user.Email);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(phone, user.Phone);
            Assert.False(user.IsLoggedIn);
            Assert.NotEqual(Guid.Empty, user.Id);
        }

        [Fact]
        public void Login_ShouldReturnTrue_WhenCredentialsAreCorrect()
        {
            // Arrange
            var user = CreateValidUser();

            // Act
            var result = user.Login("test@mail.com", "123456");

            // Assert
            Assert.True(result);
            Assert.True(user.IsLoggedIn);
        }

        [Fact]
        public void Login_ShouldReturnFalse_WhenEmailIsWrong()
        {
            // Arrange
            var user = CreateValidUser();

            // Act
            var result = user.Login("wrong@mail.com", "123456");

            // Assert
            Assert.False(result);
            Assert.False(user.IsLoggedIn);
        }

        [Fact]
        public void Login_ShouldReturnFalse_WhenPasswordIsWrong()
        {
            // Arrange
            var user = CreateValidUser();

            // Act
            var result = user.Login("test@mail.com", "wrongpass");

            // Assert
            Assert.False(result);
            Assert.False(user.IsLoggedIn);
        }

        [Fact]
        public void Logout_ShouldSetIsLoggedInToFalse()
        {
            // Arrange
            var user = CreateValidUser();
            user.Login("test@mail.com", "123456");

            // Act
            user.Logout();

            // Assert
            Assert.False(user.IsLoggedIn);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenEmailIsInvalid()
        {
            Assert.Throws<ArgumentException>(() =>
                new User("invalidemail", "123456", "John", "Doe", DateTime.Now, "+380501234567"));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPasswordTooShort()
        {
            Assert.Throws<ArgumentException>(() =>
                new User("test@mail.com", "123", "John", "Doe", DateTime.Now, "+380501234567"));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenBirthDateIsTooOld()
        {
            Assert.Throws<ArgumentException>(() =>
                new User("test@mail.com", "123456", "John", "Doe", new DateTime(1800, 1, 1), "+380501234567"));
        }

        // Helper
        private User CreateValidUser()
        {
            return new User(
                "test@mail.com",
                "123456",
                "John",
                "Doe",
                new DateTime(2000, 1, 1),
                "+380501234567"
            );
        }
    }
}