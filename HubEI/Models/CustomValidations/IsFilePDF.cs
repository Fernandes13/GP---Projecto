using Microsoft.AspNetCore.Http;
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

            var model = (ViewModels.BOProjectViewModel)validationContext.ObjectInstance;

            if (file == null && model.Project.Report != null)
            {
                return ValidationResult.Success;
            }
            else if(file == null)
            {
                return new ValidationResult("É necessário introduzir o relatório do projecto.");
            }

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
