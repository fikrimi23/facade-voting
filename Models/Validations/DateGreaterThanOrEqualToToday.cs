using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Validations
{
    public class DateGreaterThanOrEqualToToday : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dt = (DateTime) value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Make sure your date is >= than today");
        }
    }
}