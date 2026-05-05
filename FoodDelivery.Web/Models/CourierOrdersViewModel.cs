using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Web.Models
{
    public class CourierOrdersViewModel
    {
        public List<Order> AvailableOrders { get; set; } = new();
        public List<Order> MyOrders { get; set; } = new();
    }
}
