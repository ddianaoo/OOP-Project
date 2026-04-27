using System;
using System.Linq;
using FoodDelivery.Domain.Entities;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class MenuTests
    {
        private Dish CreateDish(string name = "Pizza")
        {
            return new Dish(name, "Cheese pizza", 10m);
        }

        [Fact]
        public void AddDish_ShouldAddDish_WhenValid()
        {
            // Arrange
            var menu = new Menu();
            var dish = CreateDish();

            // Act
            var result = menu.AddDish(dish);

            // Assert
            Assert.True(result);
            Assert.Single(menu.GetMenu());
        }

        [Fact]
        public void AddDish_ShouldFail_WhenNameAlreadyExists_CaseInsensitive()
        {
            // Arrange
            var menu = new Menu();

            var dish1 = CreateDish("Pizza");
            var dish2 = CreateDish("pizza"); // lowercase duplicate

            menu.AddDish(dish1);

            // Act
            var result = menu.AddDish(dish2);

            // Assert
            Assert.False(result);
            Assert.Single(menu.GetMenu());
        }

        [Fact]
        public void RemoveDish_ShouldRemove_WhenDishExists()
        {
            // Arrange
            var menu = new Menu();
            var dish = CreateDish();

            menu.AddDish(dish);

            // Act
            var result = menu.RemoveDish(dish.Id);

            // Assert
            Assert.True(result);
            Assert.Empty(menu.GetMenu());
        }

        [Fact]
        public void RemoveDish_ShouldReturnFalse_WhenDishNotFound()
        {
            // Arrange
            var menu = new Menu();

            // Act
            var result = menu.RemoveDish(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateDish_ShouldUpdateAllFields_WhenValid()
        {
            // Arrange
            var menu = new Menu();
            var dish = CreateDish();

            menu.AddDish(dish);

            // Act
            var result = menu.UpdateDish(
                dish.Id,
                "Burger",
                "Beef burger",
                15m,
                "image.jpg"
            );

            // Assert
            Assert.True(result);

            var updated = menu.GetMenu().First();

            Assert.Equal("Burger", updated.Name);
            Assert.Equal("Beef burger", updated.Description);
            Assert.Equal(15m, updated.Price);
            Assert.Equal("image.jpg", updated.ImageUrl);
        }

        [Fact]
        public void UpdateDish_ShouldFail_WhenDishNotFound()
        {
            // Arrange
            var menu = new Menu();

            // Act
            var result = menu.UpdateDish(
                Guid.NewGuid(),
                "Burger",
                "Desc",
                10m,
                null
            );

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateDish_ShouldFail_WhenNameAlreadyExists()
        {
            // Arrange
            var menu = new Menu();

            var dish1 = CreateDish("Pizza");
            var dish2 = CreateDish("Burger");

            menu.AddDish(dish1);
            menu.AddDish(dish2);

            // Act
            var result = menu.UpdateDish(
                dish2.Id,
                "PIZZA", // duplicate (case insensitive)
                "New desc",
                20m,
                null
            );

            // Assert
            Assert.False(result);
        }
    }
}