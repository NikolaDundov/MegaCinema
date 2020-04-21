namespace MegaCinema.Web.Infrastructure.CustomAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.RegularExpressions;

    public class TitleValidationAttribite : ValidationAttribute
    {
        private const string ForbidenSymbol = "You cannot use these symbols in movie title";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueStr = value.ToString();
            if (!Regex.IsMatch(valueStr, @"^[A-Za-z0-9 &'!?.,:-]+$"))
            {
                return new ValidationResult(ForbidenSymbol);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
