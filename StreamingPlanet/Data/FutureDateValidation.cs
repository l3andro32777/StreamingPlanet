using StreamingPlanet.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StreamingPlanet.Data
{
    public class FutureDateValidation : ValidationAttribute
    {
        public string GetErrorMessage() => $"Data de nascimento não pode ser do futuro.";

        protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
        {
            PropertyInfo? dateTimeProperty = validationContext.ObjectType.GetProperty("BirthDate");
            if (dateTimeProperty == null)
            {
                return ValidationResult.Success;
            }

            DateTime date = (DateTime)dateTimeProperty.GetValue(value, null);
            var releaseYear = ((DateTime)value!).Year;

            if (date.CompareTo(DateTime.Now) >= 0)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

    }
}
