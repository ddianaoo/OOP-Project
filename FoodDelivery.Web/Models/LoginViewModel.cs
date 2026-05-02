using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть пошту")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; }
    }
}