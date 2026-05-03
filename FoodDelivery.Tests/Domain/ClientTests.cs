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
        [Fact]
        public void AddToCart_Should_Add_Item_When_Cart_Exists()
        {
            var client = new Client(
                "client@mail.com",
                "123456",
                "John",
                "Doe",
                new DateTime(2000, 1, 1),
                "+380991234567"
            );

            client.GetType()
                .GetProperty("Cart")
                ?.SetValue(client, new Cart(client.Id));

            var dish = new Dish("Pizza", "Tasty", 10m);

            client.AddToCart(dish, 2);

            Assert.Single(client.Cart.Items);
            Assert.Equal(2, client.Cart.Items.First().Quantity);
        }

        [Fact]
        public void CreateOrder_Should_Throw_When_Cart_Empty()
        {
            var client = new Client(
                "client@mail.com",
                "123456",
                "John",
                "Doe",
                new DateTime(2000, 1, 1),
                "+380991234567"
            );

            Assert.Throws<Exception>(() =>
                client.CreateOrder("Kyiv street"));
        }
    }
}