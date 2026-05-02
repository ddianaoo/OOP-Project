using FoodDelivery.Web.Validation;
using System.ComponentModel.DataAnnotations;
using FoodDelivery.Web.Validation;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Web.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Введіть ім'я")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Введіть прізвище")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Введіть пошту")]
    [EmailAddress(ErrorMessage = "Невірний формат email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Введіть номер телефону")]
    [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Формат: +380XXXXXXXXX")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Введіть пароль")]
    [MinLength(6, ErrorMessage = "Мінімум 6 символів")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Повторіть пароль")]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    public string Password2 { get; set; }

    [Required(ErrorMessage = "Виберіть дату народження")]
    [MinimumAge(18)]
    public DateTime? BirthDate { get; set; }

    [Required(ErrorMessage = "Виберіть роль користувача")]
    public UserRole Role { get; set; }
}