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
    public sealed class IsFileListPDF : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<IFormFile> file = (List<IFormFile>)value;

            if (file == null)
            {
                return ValidationResult.Success;
            }

            for(var i = 0; i < file.Count; i++)
            {
                if (Path.GetExtension(file[i].FileName).ToLower() != ".pdf")
                {
                    return new ValidationResult("Todos os ficheiros têm de ser PDF.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
