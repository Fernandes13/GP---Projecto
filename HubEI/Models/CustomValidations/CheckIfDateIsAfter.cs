﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.CustomValidations
{
    /// <summary>
    /// Classe usada para validar se uma data é superior à data atual
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CheckIfDateIsAfter : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var model = (Project)validationContext.ObjectInstance;
                DateTime hours = Convert.ToDateTime(model.ProjectDate);

                DateTime date = Convert.ToDateTime(value);

                DateTime actualDate = new DateTime(date.Year, date.Month, date.Day, hours.Hour, hours.Minute, 0);

                if (actualDate < DateTime.Now)
                {
                    return new ValidationResult("A data não pode ser anterior a hoje");
                }
            }

            return ValidationResult.Success;
        }
    }
}
