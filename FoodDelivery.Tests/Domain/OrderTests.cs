using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodDelivery.Tests.Domain
{
    namespace FoodDelivery.Tests.DomainTests
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
                var items = CreateItems();

                var order = new Order(clientId, "Kyiv", items);

                Assert.Equal(clientId, order.ClientId);
                Assert.Equal("Kyiv", order.Address);
                Assert.Equal(OrderStatus.Created, order.Status);
                Assert.Single(order.Items);
                Assert.NotNull(order.CreatedAt);
            }

            // =========================
            // VALIDATION
            // =========================

            [Fact]
            public void CreateOrder_Should_Throw_If_Address_Is_Empty()
            {
                var items = CreateItems();

                Assert.Throws<ArgumentException>(() =>
                    new Order(Guid.NewGuid(), "", items)
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
            // STATUS FLOW
            // =========================

            [Fact]
            public void ChangeStatus_Should_Update_Status()
            {
                var order = new Order(Guid.NewGuid(), "Kyiv", CreateItems());

                var result = order.ChangeStatus(OrderStatus.Accepted);

                Assert.True(result);
                Assert.Equal(OrderStatus.Accepted, order.Status);
            }

            [Fact]
            public void ChangeStatus_Should_Not_Change_When_Delivered()
            {
                var order = new Order(Guid.NewGuid(), "Kyiv", CreateItems());

                order.ChangeStatus(OrderStatus.Delivered);

                var result = order.ChangeStatus(OrderStatus.Accepted);

                Assert.False(result);
                Assert.Equal(OrderStatus.Delivered, order.Status);
            }
        }
    }
}
