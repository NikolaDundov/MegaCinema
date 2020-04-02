namespace MegaCinema.Web.ViewModels.Projection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class HoursValidationAttribute : ValidationAttribute
    {
        private DateTime openHour = new DateTime(2020, 1, 1, 9, 30, 0);
        private DateTime closeHour = new DateTime(2020, 1, 1, 22, 30, 0);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime timeToCheck = Convert.ToDateTime(value);
            if (timeToCheck < DateTime.Now)
            {
                return new ValidationResult(
                    "Projection date can not be before current date.");
            }
            else if (timeToCheck.Hour < this.openHour.Hour)
            {
                return new ValidationResult(
                    "Projection hour can not be before open hour.");
            }
            else if (timeToCheck.Hour > this.closeHour.Hour)
            {
                return new ValidationResult(
                    "Projection hour can not be after close hour.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
