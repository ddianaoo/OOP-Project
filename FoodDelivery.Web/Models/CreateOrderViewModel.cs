using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Web.Models;

public class CreateOrderViewModel
{
    [Required(ErrorMessage = "Вкажіть адресу доставки")]
    public string Address { get; set; }
}