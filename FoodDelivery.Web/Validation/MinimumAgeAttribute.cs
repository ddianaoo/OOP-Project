using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Web.Validation;

public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minAge;

    public MinimumAgeAttribute(int minAge)
    {
        _minAge = minAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("Виберіть дату народження");

        var birthDate = (DateTime)value;
        var today = DateTime.Today;

        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age))
            age--;

        if (age < _minAge)
            return new ValidationResult($"Мінімальний вік {_minAge} років");

        return ValidationResult.Success;
    }
}