using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Web.Models
{
    public class UserListViewModel
    {
        public List<Client> Clients { get; set; }
        public List<Courier> Couriers { get; set; }
    }
}
