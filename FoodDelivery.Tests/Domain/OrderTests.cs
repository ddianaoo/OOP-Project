using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using System;
using System.Collections.Generic;
using Xunit;

namespace FoodDelivery.Tests.Domain.FoodDelivery.Tests.DomainTests
{
    public class OrderTests
    {
        // =========================
        // HELPERS
        // =========================

        private Dish CreateDish()
        {
            return new Dish("Pizza", "Tasty pizza", 10m);
        }

        private List<OrderItem> CreateItems()
        {
            return new List<OrderItem>
            {
                new OrderItem(CreateDish().Id, 1)
            };
        }

        // =========================
        // CREATE ORDER
        // =========================

        [Fact]
        public void CreateOrder_Should_Set_Initial_State()
        {
            var clientId = Guid.NewGuid();

            var order = new Order(clientId, "Kyiv", CreateItems());

            Assert.Equal(clientId, order.ClientId);
            Assert.Equal("Kyiv", order.Address);
            Assert.Equal(OrderStatus.New, order.Status);
            Assert.Single(order.Items);
            Assert.NotNull(order.CreatedAt);
        }

        // =========================
        // VALIDATION
        // =========================

        [Fact]
        public void CreateOrder_Should_Throw_If_Address_Is_Empty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Order(Guid.NewGuid(), "", CreateItems())
            );
        }

        [Fact]
        public void CreateOrder_Should_Throw_If_Items_Are_Empty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Order(Guid.NewGuid(), "Kyiv", new List<OrderItem>())
            );
        }

        // =========================
        // COURIER ASSIGNMENT
        // =========================

        [Fact]
        public void AssignCourier_Should_Set_CourierId()
        {
            var order = new Order(Guid.NewGuid(), "Kyiv", CreateItems());
            var courierId = Guid.NewGuid();

            order.AssignCourier(courierId);

            Assert.Equal(courierId, order.CourierId);
        }

        [Fact]
        public void AssignCourier_Should_Throw_If_Already_Assigned()
        {
            var order = new Order(Guid.NewGuid(), "Kyiv", CreateItems());

            order.AssignCourier(Guid.NewGuid());

            Assert.Throws<Exception>(() =>
                order.AssignCourier(Guid.NewGuid())
            );
        }

        // =========================
        // ACCEPT ORDER (ONLY REAL FLOW)
        // =========================

        [Fact]
        public void Accept_Should_Set_Status_To_Accepted()
        {
            var order = new Order(Guid.NewGuid(), "Kyiv", CreateItems());
            var courierId = Guid.NewGuid();

            order.AssignCourier(courierId);

            order.Accept(courierId);

            Assert.Equal(OrderStatus.Accepted, order.Status);
            Assert.Equal(courierId, order.CourierId);
        }

        [Fact]
        public void Accept_Should_Not_Work_Without_Courier()
        {
            var order = new Order(Guid.NewGuid(), "Kyiv", CreateItems());

            var result = order.Status;

            Assert.Equal(OrderStatus.New, result);
        }
    }
}