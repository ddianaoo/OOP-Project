using System;
using System.Collections.Generic;
using System.Linq;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class ClientTests
    {
        private Client CreateClient()
        {
            return new Client(
                "client@mail.com",
                "123456",
                "John",
                "Doe",
                new DateTime(2000, 1, 1),
                "+380501234567"
            );
        }

        private Dish CreateDish(string name = "Pizza")
        {
            return new Dish(name, "Cheese pizza", 10m);
        }

        [Fact]
        public void AddToCart_ShouldAddDish()
        {
            // Arrange
            var client = CreateClient();
            var dish = CreateDish();

            // Act
            var result = client.AddToCart(dish, 2);

            // Assert
            Assert.True(result);
            Assert.Single(client.Cart.Items);
            Assert.Equal(2, client.Cart.Items.First().quantity);
        }

        [Fact]
        public void AddToCart_ShouldIncreaseQuantity_WhenDishAlreadyExists()
        {
            // Arrange
            var client = CreateClient();
            var dish = CreateDish();

            client.AddToCart(dish, 2);

            // Act
            client.AddToCart(dish, 3);

            // Assert
            Assert.Single(client.Cart.Items);
            Assert.Equal(5, client.Cart.Items.First().quantity);
        }

        [Fact]
        public void AddToCart_ShouldFail_WhenQuantityIsZero()
        {
            // Arrange
            var client = CreateClient();
            var dish = CreateDish();

            // Act
            var result = client.AddToCart(dish, 0);

            // Assert
            Assert.False(result);
            Assert.Empty(client.Cart.Items);
        }

        [Fact]
        public void CreateOrder_ShouldFail_WhenCartIsEmpty()
        {
            // Arrange
            var client = CreateClient();

            // Act
            var result = client.CreateOrder("Kyiv");

            // Assert
            Assert.False(result.success);
            Assert.Equal(0, result.orderId);
        }

        [Fact]
        public void CreateOrder_ShouldCreateOrder_AndClearCart()
        {
            // Arrange
            var client = CreateClient();
            var dish = CreateDish();

            client.AddToCart(dish, 2);

            // Act
            var result = client.CreateOrder("Kyiv");

            // Assert
            Assert.True(result.success);
            Assert.True(result.orderId > 0);
            Assert.Empty(client.Cart.Items);
        }

        [Fact]
        public void TrackOrder_ShouldReturnCorrectStatus()
        {
            // Arrange
            var client = CreateClient();
            var dish = CreateDish();

            var order = new Order(
                "Kyiv",
                new List<(Dish dish, int quantity)>
                {
                    (dish, 1)
                }
            );

            // Act
            var status = client.TrackOrder(order);

            // Assert
            Assert.Equal(OrderStatus.Created, status);
        }
    }
}