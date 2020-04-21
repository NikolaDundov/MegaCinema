namespace MegaCinema.Web.Infrastructure.CustomAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class HoursValidationAttribute : ValidationAttribute
    {
        private static readonly string BeforeCurrentDateError = "Projection date can not be before current date.";
        private static readonly string BeforeOpenHourError = "Projection hour can not be before open hour.";
        private static readonly string AfterClosingHourError = "Projection hour can not be after close hour.";
        private readonly DateTime openHour = new DateTime(2020, 1, 1, 10, 30, 00);
        private readonly DateTime closeHour = new DateTime(2020, 1, 1, 22, 30, 00);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime timeToCheck = Convert.ToDateTime(value);
            if (timeToCheck < DateTime.Now)
            {
                return new ValidationResult(BeforeCurrentDateError);
            }
            else if (timeToCheck.Hour < this.openHour.Hour)
            {
                return new ValidationResult(BeforeOpenHourError);
            }
            else if (timeToCheck.Hour == this.openHour.Hour
                && timeToCheck.Minute < this.openHour.Minute)
            {
                return new ValidationResult(BeforeOpenHourError);
            }
            else if (timeToCheck.Hour > this.closeHour.Hour)
            {
                return new ValidationResult(AfterClosingHourError);
            }
            else if (timeToCheck.Hour == this.closeHour.Hour
                && timeToCheck.Minute > this.closeHour.Minute)
            {
                return new ValidationResult(AfterClosingHourError);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
