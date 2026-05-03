using System;
using FoodDelivery.Domain.Entities;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class DishTests
    {
        [Fact]
        public void Constructor_Should_Create_Dish_With_Correct_Values()
        {
            // Arrange
            var name = "Pizza";
            var description = "Cheese pizza";
            var price = 10.5m;
            var imageUrl = "image.jpg";

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
        public void Update_Should_Change_All_Fields()
        {
            // Arrange
            var dish = new Dish("Old", "Old desc", 5m);
            var newName = "New";
            var newDesc = "New desc";
            var newPrice = 20m;
            var newImage = "new.jpg";

            // Act
            dish.Update(newName, newDesc, newPrice, newImage);

            // Assert
            Assert.Equal(newName, dish.Name);
            Assert.Equal(newDesc, dish.Description);
            Assert.Equal(newPrice, dish.Price);
            Assert.Equal(newImage, dish.ImageUrl);
        }

        [Fact]
        public void OrderItems_Should_Be_Empty_By_Default()
        {
            // Arrange
            var dish = new Dish("Pizza", "desc", 10m);

            // Assert
            Assert.NotNull(dish.OrderItems);
            Assert.Empty(dish.OrderItems);
        }
    }
}