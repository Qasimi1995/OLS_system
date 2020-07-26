using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.CustomValidation
{
    public class ValidateRport : ValidationAttribute
{
    private readonly string[] _Extensions;
    public ValidateRport(string[] Extensions)
    {
        _Extensions = Extensions;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var ProcessStatusId = validationContext.ObjectInstance.GetType().GetProperty("ProcessStatusId").GetValue(validationContext.ObjectInstance);
        var file = value as IFormFile;

            if (Guid.Parse(ProcessStatusId.ToString()) == Guid.Parse( "D279A58A-1FC1-4A01-A9A3-38EC746ABE62"))
            {

                if (!(file == null))
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (_Extensions.Contains(extension.ToLower()))
                    {
                        return ValidationResult.Success;
                    }
                    else {
                        return new ValidationResult("Only PDF allowed");

                    }
                }
                else {

                    return new ValidationResult("Please select te report");
                }

            }


            return ValidationResult.Success;
        }

  
}
}
