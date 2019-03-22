using LexiconLMS.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LexiconLMS.Models
{
    public class StartDateNotEarlierThanParentStartDate : ValidationAttribute, IClientModelValidator
    {
        public StartDateNotEarlierThanParentStartDate()
        {
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if(validationContext.ObjectInstance is IDateInterval child && validationContext.ObjectInstance is IParentDateInterval parent)
            {
                if (child.StartDate.CompareTo(parent.ParentStartDate) < 0)
                {
                    return new ValidationResult(string.Format(ErrorMessage, child.StartDate.ToString(Common.DateFormat), parent.ParentStartDate.ToString(Common.DateFormat)));
                }                
            }
            return ValidationResult.Success;
        }

        void IClientModelValidator.AddValidation(ClientModelValidationContext context)
        {
        }
    }
}

