﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class IsFilePDF : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = (IFormFile)value;

            if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
            {
                return new ValidationResult("O ficheiro tem de ser PDF.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
