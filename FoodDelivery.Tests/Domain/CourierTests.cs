using System;
using System.Collections.Generic;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class CourierTests
    {
        private Courier CreateCourier()
        {
            return new Courier(
                "courier@mail.com",
                "123456",
                "John",
                "Courier",
                new DateTime(1995, 1, 1),
                "+380501111111"
            );
        }

        private Order CreateOrder()
        {
            var dish = new Dish("Pizza", "Cheese pizza", 10m);

            return new Order(
                "Kyiv",
                new List<(Dish dish, int quantity)>
                {
                    (dish, 1)
                }
            );
        }

        [Fact]
        public void AcceptOrder_ShouldSucceed_WhenOrderIsCreated()
        {
            // Arrange
            var courier = CreateCourier();
            var order = CreateOrder();

            // Act
            var result = courier.AcceptOrder(order);

            // Assert
            Assert.True(result);
            Assert.Equal(OrderStatus.Accepted, order.Status);
            Assert.False(courier.IsAvailable);
        }

        [Fact]
        public void AcceptOrder_ShouldFail_WhenOrderNotCreated()
        {
            // Arrange
            var courier = CreateCourier();
            var order = CreateOrder();

            order.ChangeStatus(OrderStatus.InTransit);

            // Act
            var result = courier.AcceptOrder(order);

            // Assert
            Assert.False(result);
            Assert.NotEqual(OrderStatus.Accepted, order.Status);
        }

        [Fact]
        public void UpdateOrderStatus_ShouldChangeStatus()
        {
            // Arrange
            var courier = CreateCourier();
            var order = CreateOrder();

            courier.AcceptOrder(order);

            // Act
            var result = courier.UpdateOrderStatus(order, OrderStatus.InTransit);

            // Assert
            Assert.True(result);
            Assert.Equal(OrderStatus.InTransit, order.Status);
        }

        [Fact]
        public void UpdateOrderStatus_ShouldAllowDelivered()
        {
            // Arrange
            var courier = CreateCourier();
            var order = CreateOrder();

            courier.AcceptOrder(order);

            // Act
            var result = courier.UpdateOrderStatus(order, OrderStatus.Delivered);

            // Assert
            Assert.True(result);
            Assert.Equal(OrderStatus.Delivered, order.Status);
        }

        [Fact]
        public void IsAvailable_ShouldBeFalse_AfterAcceptOrder()
        {
            // Arrange
            var courier = CreateCourier();
            var order = CreateOrder();

            // Act
            courier.AcceptOrder(order);

            // Assert
            Assert.False(courier.IsAvailable);
        }
    }
}