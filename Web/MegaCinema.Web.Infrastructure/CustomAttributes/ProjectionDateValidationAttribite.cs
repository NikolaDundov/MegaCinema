namespace MegaCinema.Web.Infrastructure.CustomAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ProjectionDateValidationAttribite : ValidationAttribute
    {
        private const string ErrorMessage = "Projection time cannot be before today";
        private readonly DateTime projectioStartTime = DateTime.UtcNow;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime selectedTime = Convert.ToDateTime(value);
            if (selectedTime.Date < this.projectioStartTime.Date)
            {
                return new ValidationResult(ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
