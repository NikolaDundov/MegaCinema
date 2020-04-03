namespace MegaCinema.Web.ViewModels.CustomAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class MovieDurationAttribute : ValidationAttribute
    {
        private readonly TimeSpan minimumLength = new TimeSpan(0, 20, 0);
        private readonly TimeSpan maximumLength = new TimeSpan(4, 59, 59);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TimeSpan duration = TimeSpan.Parse(value.ToString());
            if (duration < this.minimumLength)
            {
                return new ValidationResult(
                    "Movie duration can not be less than 20 miutes.");
            }
            else if (duration > this.maximumLength)
            {
                return new ValidationResult(
                    "Movie duration can not be more than 5 hours.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
