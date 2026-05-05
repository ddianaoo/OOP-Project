using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Web.Models;

public class DishCreateViewModel
{
    [Required(ErrorMessage = "Назва обов'язкова")]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Ціна обов'язкова")]
    [Range(0.01, 10000, ErrorMessage = "Ціна повинна бути > 0")]
    public decimal? Price { get; set; }

    public IFormFile? ImageFile { get; set; }
}