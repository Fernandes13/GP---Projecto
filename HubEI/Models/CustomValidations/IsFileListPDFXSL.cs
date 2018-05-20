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
    public sealed class IsFileListPDFXSL : ValidationAttribute
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
                if (Path.GetExtension(file[i].FileName).ToLower() != ".pdf" 
                        || Path.GetExtension(file[i].FileName).ToLower() != ".xsl"
                        || Path.GetExtension(file[i].FileName).ToLower() != ".xslx")
                {
                    return new ValidationResult("Todos os ficheiros têm de ser PDF ou MS Excel.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
