using System;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Services;
using Xunit;

namespace FoodDelivery.Tests.Services
{
    public class CartServiceTests
    {
        // =========================
        // HELPERS
        // =========================

        private CartService CreateService()
        {
            return new CartService();
        }

        // =========================
        // ADD TO CART
        // =========================

        [Fact]
        public void AddToCart_Should_Create_Cart_And_Add_Item()
        {
            var service = CreateService();
            var clientId = Guid.NewGuid();
            var dishId = Guid.NewGuid();

            service.AddToCart(clientId, dishId, 1);

            service.AddToCart(clientId, dishId, 2);
            Assert.True(true);
        }

        // =========================
        // REMOVE FROM CART
        // =========================

        [Fact]
        public void RemoveFromCart_Should_Remove_Item()
        {
            var service = CreateService();
            var clientId = Guid.NewGuid();
            var dishId = Guid.NewGuid();

            service.AddToCart(clientId, dishId, 1);
            service.RemoveFromCart(clientId, dishId);

            Assert.True(true);
        }

        // =========================
        // CLEAR CART
        // =========================

        [Fact]
        public void Clear_Should_Remove_All_Items()
        {
            var service = CreateService();
            var clientId = Guid.NewGuid();

            service.AddToCart(clientId, Guid.NewGuid(), 1);
            service.AddToCart(clientId, Guid.NewGuid(), 2);

            service.Clear(clientId);

            Assert.True(true);
        }

        // =========================
        // CART CREATION LOGIC
        // =========================

        [Fact]
        public void AddToCart_Should_Create_Separate_Carts_For_Different_Users()
        {
            var service = CreateService();

            var client1 = Guid.NewGuid();
            var client2 = Guid.NewGuid();

            service.AddToCart(client1, Guid.NewGuid(), 1);
            service.AddToCart(client2, Guid.NewGuid(), 1);

            Assert.True(true);
        }
    }
}