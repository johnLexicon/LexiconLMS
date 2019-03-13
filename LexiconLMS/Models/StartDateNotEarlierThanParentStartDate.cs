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
                    var dateTimeFormat = "yyyy-MM-dd";
                    return new ValidationResult(string.Format(ErrorMessage, child.StartDate.ToString(dateTimeFormat), parent.ParentStartDate.ToString(dateTimeFormat)));
                }                
            }
            return ValidationResult.Success;
        }

        void IClientModelValidator.AddValidation(ClientModelValidationContext context)
        {
        }
    }
}

