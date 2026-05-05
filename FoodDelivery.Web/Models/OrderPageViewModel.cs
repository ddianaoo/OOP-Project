using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FoodDelivery.Web.Models;

public class OrderPageViewModel
{
    [BindNever]
    public Cart Cart { get; set; }

    public CreateOrderViewModel Form { get; set; } = new();
}