using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using Xunit;

public class CourierTests
{
    private Courier CreateCourier()
    {
        return new Courier(
            "test@mail.com",
            "123456",
            "John",
            "Doe",
            new DateTime(1990, 1, 1),
            "+380000000000"
        );
    }

    private Order CreateOrder()
    {
        var dish = new Dish("Pizza", "Tasty pizza", 10m);

        var items = new List<OrderItem>
        {
            new OrderItem(dish, 1)
        };

        return new Order(Guid.NewGuid(), "Kyiv", items);
    }

    [Fact]
    public void AcceptOrder_Should_Set_Status_And_Disable_Availability()
    {
        var courier = CreateCourier();
        var order = CreateOrder();

        var result = courier.AcceptOrder(order);

        Assert.True(result);
        Assert.Equal(OrderStatus.Accepted, order.Status);
        Assert.False(courier.IsAvailable);
    }

    [Fact]
    public void UpdateOrderStatus_Should_Update_Status()
    {
        var courier = CreateCourier();
        var order = CreateOrder();

        courier.UpdateOrderStatus(order, OrderStatus.Delivered);

        Assert.Equal(OrderStatus.Delivered, order.Status);
    }
}