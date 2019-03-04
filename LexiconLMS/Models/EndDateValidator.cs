using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class EndDateLaterThanStartDate : ValidationAttribute, IClientModelValidator
    {
        public EndDateLaterThanStartDate()
        {
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            ModuleViewModel module = (ModuleViewModel)validationContext.ObjectInstance;

            if (module.EndDate.CompareTo(module.StartDate) > 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("End Date must be later than Start Date");
        }

        void IClientModelValidator.AddValidation(ClientModelValidationContext context)
        {
        }
    }
}

