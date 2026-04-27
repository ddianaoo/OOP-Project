using System;
using FoodDelivery.Domain.Entities;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class DishTests
    {
        [Fact]
        public void Constructor_ShouldCreateDish_WhenDataIsValid()
        {
            // Arrange
            var name = "Pizza";
            var description = "Cheese pizza";
            var price = 10.5m;
            var imageUrl = "https://image.com/pizza.jpg";

            // Act
            var dish = new Dish(name, description, price, imageUrl);

            // Assert
            Assert.Equal(name, dish.Name);
            Assert.Equal(description, dish.Description);
            Assert.Equal(price, dish.Price);
            Assert.Equal(imageUrl, dish.ImageUrl);
            Assert.NotEqual(Guid.Empty, dish.Id);
        }

        [Fact]
        public void Constructor_ShouldAllowNullImageUrl()
        {
            // Act
            var dish = new Dish("Burger", "Beef burger", 8.0m);

            // Assert
            Assert.Equal("Burger", dish.Name);
            Assert.Null(dish.ImageUrl);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Dish("", "Valid description", 10m));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsWhitespace()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Dish("   ", "Valid description", 10m));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDescriptionIsEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Dish("Pizza", "", 10m));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPriceIsZero()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Dish("Pizza", "Cheese", 0m));
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenPriceIsNegative()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Dish("Pizza", "Cheese", -5m));
        }

        [Fact]
        public void Dish_ShouldHaveUniqueIds()
        {
            // Arrange & Act
            var dish1 = new Dish("Pizza", "Cheese", 10m);
            var dish2 = new Dish("Burger", "Beef", 12m);

            // Assert
            Assert.NotEqual(dish1.Id, dish2.Id);
        }
    }
}