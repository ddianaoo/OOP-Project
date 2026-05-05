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
            new OrderItem(dish.Id, 1)
        };

        return new Order(Guid.NewGuid(), "Kyiv", items);
    }

    [Fact]
    public void AcceptOrder_Should_Set_Status_And_Disable_Availability()
    {
        // arrange
        var courier = CreateCourier();
        var order = CreateOrder();

        // act
        var result = courier.AcceptOrder(order);

        // assert
        Assert.True(result);
        Assert.Equal(OrderStatus.Accepted, order.Status);
        Assert.False(courier.IsAvailable);
        Assert.Equal(courier.Id, order.CourierId);
    }

    [Fact]
    public void AcceptOrder_Should_Fail_When_Order_Not_New()
    {
        var courier = CreateCourier();
        var order = CreateOrder();

        order.Accept(Guid.NewGuid());

        var result = courier.AcceptOrder(order);

        Assert.False(result);
    }

    [Fact]
    public void Courier_Should_Be_Available_Initially()
    {
        var courier = CreateCourier();

        Assert.True(courier.IsAvailable);
    }
}